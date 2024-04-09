using UnityEngine;
using TMPro;

public class OculusBatteryLevel : MonoBehaviour
{
    public TextMeshProUGUI batteryLevelText;

    void Update()
    {
        float headsetBatteryLevel = GetHeadsetBatteryLevel();

        if (headsetBatteryLevel >= 0)
        {
            batteryLevelText.text = "Headset Battery Level: " + headsetBatteryLevel.ToString("F0") + "%";
        }
        else
        {
            batteryLevelText.text = "Headset Battery Level: N/A";
        }
    }

    float GetHeadsetBatteryLevel()
    {
#if UNITY_ANDROID
        // Check if the application is running on an Android device
        if (Application.platform == RuntimePlatform.Android)
        {
            // Use Android API to get the battery level
            AndroidJavaObject context = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject intentFilter = new AndroidJavaObject("android.content.IntentFilter", "android.intent.action.BATTERY_CHANGED");
            AndroidJavaObject batteryStatus = context.Call<AndroidJavaObject>("registerReceiver", null, intentFilter);

            int level = batteryStatus.Call<int>("getIntExtra", "level", -1);
            int scale = batteryStatus.Call<int>("getIntExtra", "scale", -1);

            if (level >= 0 && scale > 0)
            {
                return (float)level / scale * 100f;
            }
        }
#endif
        return -1f; // Return -1 if unable to retrieve the battery level
    }
}
