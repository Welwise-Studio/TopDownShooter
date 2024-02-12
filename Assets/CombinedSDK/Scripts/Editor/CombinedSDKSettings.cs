using UnityEngine;

[CreateAssetMenu(fileName = "CombinedSDKSettings", menuName = "CombinedSDK", order = 51)]
public class CombinedSDKSettings : ScriptableObject
{
    public static System.Action SwitchSelectedSDKEvent;

    private void OnValidate()
    {
        SwitchSelectedSDKEvent = CombinedSelectSDK.SwitchSelectedSDK;
        SwitchSelectedSDKEvent?.Invoke();
    }

    public CombinedSDKSettingsSelectSDK selectSDK = CombinedSDKSettingsSelectSDK.None;
    public enum CombinedSDKSettingsSelectSDK
    {
        None,
        YandexGames,
        CrazyGames,
        GameDistribution,
        UnityADS,
        GamePush
    }
}