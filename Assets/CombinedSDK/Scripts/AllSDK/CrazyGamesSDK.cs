#if CRAZY_GAMES
using CrazyGames;
using UnityEngine;
#endif
public class CrazyGamesSDK : SDK
{
#if CRAZY_GAMES
    [Tooltip("Все банеры которые будут взаимодейсвовать с CombinedSDK")]
    [SerializeField] private CrazyBanner[] _crazyBanners;
    [Tooltip("Показать все банеры из CrazyBanners в старте сцены")]
    [SerializeField] private bool _showAllBannersInStart;
    private string _idCrazyGames;


    private void Start()
    {
        if (_showAllBannersInStart)
        {
            ShowAllBannersAd();
        }
    }
    private void OnRewardedVideoSuccess()
    {
        CombinedSDK.OnCombinedSDKCloseRewardVideo?.Invoke(_idCrazyGames);
    }
    public override void OpenRewardAd(string id)
    {
        _idCrazyGames = id;
        CrazyAds.Instance.beginAdBreakRewarded(OnRewardedVideoSuccess, ErrorAD);
    }
    public override void OpenFullscreenAd()
    {
        CrazyAds.Instance.beginAdBreak();
    }
    public override void OpenFastFullscreenAd()
    {
        GameDistribution.Instance.PreloadRewardedAd();
    }
    public override void ShowAllBannersAd()
    {
        foreach (var banner in _crazyBanners)
        {
            if (banner != null)
            {
                banner.gameObject.SetActive(true);
                banner.MarkVisible(true);
                CrazyAds.Instance.updateBannersDisplay();
            }
        }
    }
    public override void HideAllBannersAd()
    {
        foreach (var banner in _crazyBanners)
        {
            if (banner != null)
            {
                banner.MarkVisible(false);
                CrazyAds.Instance.updateBannersDisplay();
            }
        }
    }
    public override void ShowBannerAd(CrazyBanner banner)
    {
        banner.gameObject.SetActive(true);
        banner.MarkVisible(true);
        CrazyAds.Instance.updateBannersDisplay();
    }
    public override void HideBannerAd(CrazyBanner banner)
    {
        banner.MarkVisible(false);
        CrazyAds.Instance.updateBannersDisplay();
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