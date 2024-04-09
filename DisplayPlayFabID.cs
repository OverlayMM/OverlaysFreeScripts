using UnityEngine;
using TMPro;

public class DisplayPlayFabID : MonoBehaviour
{
    public TextMeshPro IDDisplayText;

    private Playfablogin playfabloginscript;

    private void OnEnable()
    {
        GameObject[] playFabObjects = GameObject.FindGameObjectsWithTag("PlayFab");

        if (playFabObjects.Length > 0)
        {
            playfabloginscript = playFabObjects[0].GetComponent<Playfablogin>();

            if (playfabloginscript == null)
            {
                Debug.LogError("PlayFab login script not found on object tagged as PlayFab.");
            }
        }
        else
        {
            Debug.LogError("No object tagged as PlayFab found in the scene.");
        }
    }

    void Update()
    {
        if (playfabloginscript != null)
        {
            IDDisplayText.text = "Your ID is: " + playfabloginscript.MyPlayFabID;
        }
        else
        {
            IDDisplayText.text = "PlayFab login script not found.";
        }
    }
}
