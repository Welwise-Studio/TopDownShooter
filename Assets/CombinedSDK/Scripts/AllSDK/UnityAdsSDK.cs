#if UNITY_ADS
using UnityEngine;
using UnityEngine.Advertisements;
#endif

public class UnityAdsSDK : SDK
#if UNITY_ADS
    , 
    IUnityAdsInitializationListener,
    IUnityAdsLoadListener,
    IUnityAdsShowListener
#endif
{
#if UNITY_ADS
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _iosGameId;
    [SerializeField] private bool _isTesting = true;
    [Space(10)]
    [SerializeField] private string _rewardedAdAndroidId = "Rewarded_Android";
    [SerializeField] private string _rewardedAdIosId = "Rewarded_iOS";
    [Space(10)]
    [SerializeField] private string _bannerAdAndroidId = "Banner_Android";
    [SerializeField] private string _bannerAdIosId = "Banner_iOS";
    [Space(10)]
    [SerializeField] private string _interstitialAdAndroidId = "Interstitial_Android";
    [SerializeField] private string _interstitialAdIosId = "Interstitial_iOS";

    [Header("——————— UnityAds: Баннер ———————")]
    [Space(5)]
    [SerializeField] private BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    private string _gameId;
    private string _rewardedId;
    private string _bannerId;
    private string _interstitialId;

    private string _idUnityAds;

    new private void Awake()
    {
        base.Awake();
#if UNITY_ANDROID
        _gameId = _androidGameId;
        _rewardedId = _rewardedAdAndroidId;
        _bannerId = _bannerAdAndroidId;
        _interstitialId = _interstitialAdAndroidId;
#elif UNITY_IOS
        _gameId = _iosGameId;
        _rewardedId = _rewardedAdIosId;
        _bannerId = _bannerAdIosId;
        _interstitialId = _interstitialAdIosId;
#elif UNITY_EDITOR
        _gameId = _androidGameId;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _isTesting, this);
        }

        Advertisement.Banner.SetPosition(_bannerPosition);

        LoadBannerAd();
        LoadInterstitalAd();
        LoadRewardAd();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("UnityAds: Ads Initialized...");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("UnityAds: Interstitial Ad Loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    private void BannerLoadedError(string message) { }

    private void BannerLoaded()
    {
        Debug.Log("UnityAds: Banner Ad Loaded");
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { ErrorAD(); }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == _gameId && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Debug.Log("UnityAds: Ads Fully Watched .....");
        }
        Debug.Log("UnityAds: Interstitial Ad Completed");
        CombinedSDK.OnCombinedSDKCloseRewardVideo?.Invoke(_idUnityAds);
    }

    private void BannerHidden() { }
    private void BannerClicked() { }
    private void BannerShown() { }
    private void LoadBannerAd()
    {
        BannerLoadOptions options = new BannerLoadOptions()
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadedError
        };

        Advertisement.Banner.Load(_bannerId, options);
    }
    private void LoadInterstitalAd()
    {
        Advertisement.Load(_interstitialId, this);
    }
    private void LoadRewardAd()
    {
        Advertisement.Load(_rewardedId, this);
    }
    public override void OpenRewardAd(string id)
    {
        _idUnityAds = id;
        Advertisement.Show(_rewardedId, this);
        LoadRewardAd();
    }
    public override void OpenFullscreenAd()
    {
        Advertisement.Show(_interstitialId, this);
        LoadInterstitalAd();
    }
    public override void OpenFastFullscreenAd()
    {
        DebugError(100);
    }
    public override void ShowAllBannersAd()
    {
        BannerOptions options = new BannerOptions()
        {
            showCallback = BannerShown,
            clickCallback = BannerClicked,
            hideCallback = BannerHidden
        };

        Advertisement.Banner.Show(_bannerId, options);
    }
    public override void HideAllBannersAd()
    {
        Advertisement.Banner.Hide();
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