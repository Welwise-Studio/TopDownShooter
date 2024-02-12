#if GAME_DISTRIBUTION
using UnityEngine;
#endif

public class GameDistributionSDK : SDK
{
#if GAME_DISTRIBUTION
    private string _idGameDistribution;
    private void OnEnable()
    {
        GameDistribution.OnResumeGame += OnResumeGame;
        GameDistribution.OnPauseGame += OnPauseGame;
        GameDistribution.OnRewardedVideoSuccess += OnRewardedVideoSuccess;
        GameDistribution.OnRewardedVideoFailure += ErrorAD;
    }
    private void OnDisable()
    {
        GameDistribution.OnResumeGame -= OnResumeGame;
        GameDistribution.OnPauseGame -= OnPauseGame;
        GameDistribution.OnRewardedVideoSuccess -= OnRewardedVideoSuccess;
        GameDistribution.OnRewardedVideoFailure -= ErrorAD;
    }
    private void OnResumeGame()
    {
        AudioListener.pause = false;
    }
    private void OnPauseGame()
    {
        AudioListener.pause = true;
    }
    private void OnRewardedVideoSuccess()
    {
        CombinedSDK.OnCombinedSDKCloseRewardVideo?.Invoke(_idGameDistribution);
    }
    public override void OpenRewardAd(string id)
    {
        _idGameDistribution = id;
        GameDistribution.Instance.ShowRewardedAd();
    }
    public override void OpenFullscreenAd()
    {
        GameDistribution.Instance.ShowAd();
    }
    public override void OpenFastFullscreenAd()
    {
        GameDistribution.Instance.PreloadRewardedAd();
    }
    public override void ShowAllBannersAd()
    {
        DebugError(100);
    }
    public override void HideAllBannersAd()
    {
        DebugError(100);
    }
    public override void BuyPayments(string id)
    {
        DebugError(100);
    }
    public override void AddPayments(GameObject go)
    {
        DebugError(100);
    }
    public override void SaveProgressData()
    {
        base.SaveProgressData();
    }
    public override void LoadProgressData()
    {
        base.LoadProgressData();
        // ---------------------------------------------------------------
        // Ваша реализация присваивания сохраненных данных



        // ---------------------------------------------------------------
    }
    private void ErrorAD()
    {
        DebugError(400);
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
