#if GAME_PUSH
using GamePush;
using UnityEngine;
#endif

public class GamePushSDK : SDK
{
#if GAME_PUSH
    [Header("Использовать PlayerPrefs вместо Облачного сохранения")]
    [SerializeField] private bool _usePlayerPrefs = false;

    private void OnEnable()
    {
        GP_Ads.OnRewardedReward += stringRewardedId => CombinedSDK.OnCombinedSDKCloseRewardVideo?.Invoke(stringRewardedId);
    }

    private void OnDisable()
    {
        GP_Ads.OnRewardedReward -= stringRewardedId => CombinedSDK.OnCombinedSDKCloseRewardVideo?.Invoke(stringRewardedId);
    }

    public override void OpenFastFullscreenAd()
    {
        GP_Ads.ShowPreloader();
    }

    public override void OpenFullscreenAd()
    {
        GP_Ads.ShowFullscreen();
    }

    public override void OpenRewardAd(string id)
    {
        GP_Ads.ShowRewarded(id);
    }

    public override void SaveProgressData()
    {
        if (_usePlayerPrefs)
            base.SaveProgressData();
        else
        {
            Debug.Log("----------------------------------");
            foreach (var saveData in CombinedSDK.AllSavesCombinedSDK.GetType().GetFields())
            {
                Debug.Log(saveData.Name);
                if (saveData.FieldType == typeof(int))
                    GP_Player.Add(saveData.Name, (int)saveData.GetValue(CombinedSDK.AllSavesCombinedSDK));
                else if (saveData.FieldType == typeof(float))
                    GP_Player.Add(saveData.Name, (float)saveData.GetValue(CombinedSDK.AllSavesCombinedSDK));
                else
                    GP_Player.Add(saveData.Name, (string)saveData.GetValue(CombinedSDK.AllSavesCombinedSDK));
            }
        }
    }
    public override void LoadProgressData()
    {
        if (_usePlayerPrefs)
            base.LoadProgressData();
        else
        {
            foreach (var saveData in CombinedSDK.AllSavesCombinedSDK.GetType().GetFields())
            {
                if (saveData.FieldType == typeof(string))
                {
                    saveData.SetValue(CombinedSDK.AllSavesCombinedSDK, GP_Player.GetString(saveData.Name));
                }
                else if (saveData.FieldType == typeof(int))
                {
                    saveData.SetValue(CombinedSDK.AllSavesCombinedSDK, GP_Player.GetInt(saveData.Name));
                }
                else if (saveData.FieldType == typeof(float))
                {
                    saveData.SetValue(CombinedSDK.AllSavesCombinedSDK, GP_Player.GetFloat(saveData.Name));
                }
            }
        }
    }
#else
    public override void OpenFullscreenAd()
    {
        DebugError(991);
    }

    public override void OpenRewardAd(string id)
    {
        DebugError(991);
    }
#endif
}
