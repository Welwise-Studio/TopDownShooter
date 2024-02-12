#if GAME_PUSH
using GamePush;
#endif

public class GamePushSDK : SDK
{
#if GAME_PUSH
    private void OnEnable()
    {
        GP_Ads.OnRewardedReward += stringRewardedId => CombinedSDK.OnCombinedSDKCloseRewardVideo?.Invoke(stringRewardedId);
    }

    private void OnDisable()
    {
        GP_Ads.OnRewardedReward -= stringRewardedId => CombinedSDK.OnCombinedSDKCloseRewardVideo?.Invoke(stringRewardedId);
    }

    public override void OpenFullscreenAd()
    {
        GP_Ads.ShowFullscreen();
    }

    public override void OpenRewardAd(string id)
    {
        GP_Ads.ShowRewarded(id);
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
