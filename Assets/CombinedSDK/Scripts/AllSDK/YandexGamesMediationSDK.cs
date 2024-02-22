#if YANDEX_GAMES_MEDIATION
using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;
#endif

public class YandexGamesMediationSDK : SDK
{
#if YANDEX_GAMES_MEDIATION
    [SerializeField] private string _adInterstitialUnitId = "demo-interstitial-yandex";
    [SerializeField] private string _adRewardedUnitId = "demo-rewarded-yandex";

    private String message;

    private InterstitialAdLoader interstitialAdLoader;
    private Interstitial interstitial;

    private RewardedAdLoader rewardedAdLoader;
    private RewardedAd rewardedAd;

    private void OnEnable()
    {
        this.interstitialAdLoader = new InterstitialAdLoader();
        this.interstitialAdLoader.OnAdLoaded += this.HandleAdLoaded;
        this.interstitialAdLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;

        this.rewardedAdLoader = new RewardedAdLoader();
        this.rewardedAdLoader.OnAdLoaded += this.HandleAdLoaded;
        this.rewardedAdLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;

#if UNITY_EDITOR
        Debug.Log("Mobile ads SDK is not available in editor. Only Android and iOS environments are supported");
#endif
    }

    private void OnDisable()
    {
        this.interstitialAdLoader.OnAdLoaded -= this.HandleAdLoaded;
        this.interstitialAdLoader.OnAdFailedToLoad -= this.HandleAdFailedToLoad;

        this.rewardedAdLoader.OnAdLoaded -= this.HandleAdLoaded;
        this.rewardedAdLoader.OnAdFailedToLoad -= this.HandleAdFailedToLoad;
    }

    public override void OpenFullscreenAd()
    {
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitialAdLoader.LoadAd(this.CreateAdRequest(_adInterstitialUnitId));
        this.DisplayMessage("Interstitial is requested");

        ShowInterstitial();
    }

    private void ShowInterstitial()
    {
        if (this.interstitial == null)
        {
            this.DisplayMessage("Interstitial is not ready yet");
            return;
        }

        this.interstitial.OnAdClicked += this.HandleAdClicked;
        this.interstitial.OnAdShown += this.HandleAdShown;
        this.interstitial.OnAdFailedToShow += this.HandleAdFailedToShow;
        this.interstitial.OnAdImpression += this.HandleImpression;
        this.interstitial.OnAdDismissed += this.HandleAdDismissed;

        this.interstitial.Show();
    }

    public override void OpenRewardAd(string id)
    {
        this.DisplayMessage("RewardedAd is not ready yet");
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        if (this.rewardedAd != null)
        {
            this.rewardedAd.Destroy();
        }

        this.rewardedAdLoader.LoadAd(this.CreateAdRequest(_adRewardedUnitId));
        this.DisplayMessage("Rewarded Ad is requested");
    }

    private void ShowRewardedAd()
    {
        if (this.rewardedAd == null)
        {
            this.DisplayMessage("RewardedAd is not ready yet");
            return;
        }

        this.rewardedAd.OnAdClicked += this.HandleAdClicked;
        this.rewardedAd.OnAdShown += this.HandleAdShown;
        this.rewardedAd.OnAdFailedToShow += this.HandleAdFailedToShow;
        this.rewardedAd.OnAdImpression += this.HandleImpression;
        this.rewardedAd.OnAdDismissed += this.HandleAdDismissed;
        this.rewardedAd.OnRewarded += this.HandleRewarded;

        this.rewardedAd.Show();
    }

    private AdRequestConfiguration CreateAdRequest(string adUnitId)
    {
        return new AdRequestConfiguration.Builder(adUnitId).Build();
    }

    private void DisplayMessage(String message)
    {
        Debug.Log(message);
        //this.message = message + (this.message.Length == 0 ? "" : "\n--------\n" + this.message);
        //MonoBehaviour.print(message);
    }

    #region Interstitial callback handlers

    public void HandleAdLoaded(object sender, InterstitialAdLoadedEventArgs args)
    {
        this.DisplayMessage("HandleAdLoaded event received");

        this.interstitial = args.Interstitial;
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        this.DisplayMessage($"HandleAdFailedToLoad event received with message: {args.Message}");
    }
    public void HandleAdClicked(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdClicked event received");
    }

    public void HandleAdShown(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdShown event received");
    }

    public void HandleAdDismissed(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdDismissed event received");

        this.interstitial.Destroy();
        this.interstitial = null;
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        this.DisplayMessage($"HandleImpression event received with data: {data}");
    }

    public void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage($"HandleAdFailedToShow event received with message: {args.Message}");
    }

    #endregion

    #region Rewarded Ad callback handlers

    public void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs args)
    {
        this.DisplayMessage("HandleAdLoaded event received");
        this.rewardedAd = args.RewardedAd;
    }

    public void HandleAdFailedToLoadRewarded(object sender, AdFailedToLoadEventArgs args)
    {
        this.DisplayMessage(
            $"HandleAdFailedToLoad event received with message: {args.Message}");
    }

    public void HandleAdClickedRewarded(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdClicked event received");
    }

    public void HandleAdShownRewarded(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdShown event received");
    }

    public void HandleAdDismissedRewarded(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdDismissed event received");

        this.rewardedAd.Destroy();
        this.rewardedAd = null;
    }

    public void HandleImpressionRewarded(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        this.DisplayMessage($"HandleImpression event received with data: {data}");
    }

    public void HandleRewarded(object sender, Reward args)
    {
        this.DisplayMessage($"HandleRewarded event received: amout = {args.amount}, type = {args.type}");
    }

    public void HandleAdFailedToShowRewarded(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage(
            $"HandleAdFailedToShow event received with message: {args.Message}");
    }

    #endregion
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
