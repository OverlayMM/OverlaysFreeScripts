using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RandomMOTD : MonoBehaviour
{
    public string[] MOTDs;
    public TextMeshPro MOTDText;
    private const string LastChangeKey = "LastMOTDChange";
    private TimeSpan ChangeInterval = TimeSpan.FromHours(24);
    private int currentMOTDIndex = 0;

    void Pick()
    {
        MOTDText.text = MOTDs[currentMOTDIndex];
        currentMOTDIndex = (currentMOTDIndex + 1) % MOTDs.Length; // Move to the next MOTD
    }

    IEnumerator ChangeMOTD()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            DateTime lastChangeTime = DateTime.MinValue;
            if (PlayerPrefs.HasKey(LastChangeKey))
            {
                long ticks = Convert.ToInt64(PlayerPrefs.GetString(LastChangeKey));
                lastChangeTime = new DateTime(ticks);
            }
            DateTime nextChangeTime = lastChangeTime + ChangeInterval;

            // Calculate time elapsed since last change
            TimeSpan timeSinceLastChange = DateTime.Now - lastChangeTime;

            // If more than 24 hours have passed, change MOTD
            if (timeSinceLastChange >= ChangeInterval)
            {
                Pick();
                // Update last change time to now
                PlayerPrefs.SetString(LastChangeKey, DateTime.Now.Ticks.ToString());
                PlayerPrefs.Save();
            }
            else
            {
                // Otherwise, wait until the remaining time before changing MOTD
                yield return new WaitForSeconds((float)(ChangeInterval - timeSinceLastChange).TotalSeconds);
            }
        }
    }

    private void Start()
    {
        Pick(); // Set initial MOTD
        StartCoroutine(ChangeMOTD());
    }
}
