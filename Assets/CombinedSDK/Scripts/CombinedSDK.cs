#if UNITY_EDITOR
// �������� �� �������:

// AD:
// CombinedSDK.OpenRewardAd(int id)   -  ������� ���������� �� ��������������
// CombinedSDK.OpenFullscreenAd()     -  ������� ������������� �������
// CombinedSDK.OpenFastFullscreenAd() -  ������� ������� ������������� ������� [������ � GameDistributionSDK]

// Banners:
// CombinedSDK.ShowAllBannersAd()               -  �������� ��� ������� �� ������� [������ � CrazyGames ��� UnityAds]
// CombinedSDK.HideAllBannersAd()               -  �������� ��� ������� �� ������� [������ � CrazyGames ��� UnityAds]
// CombinedSDK.ShowBannerAd(CrazyBanner banner) -  �������� ������ [������ � CrazyGames]
// CombinedSDK.HideBannerAd(CrazyBanner banner) -  �������� ������ [������ � CrazyGames]

// Purchases:
// CombinedSDK.BuyPayments(string id)     -  ������ ������� [������ � YandexGames]
// CombinedSDK.AddPayments(GameObject go) -  �������� ����� ������� � ������ ���� ������� [������ � YandexGames]

// Metrics:
// SendMetric(string metricId)                              -  ���������� ������ � ���������� ����-���� [������ � YandexGames]
// SendDetalMetric(string metricId, string metricDetalName) -  ���������� ������ � ��������� ������� [������ � YandexGames]
// ��� ���������� ������ ������ ������� � ������������ PluginYG

// SaveData:
// CombinedSDK.SaveProgressData()  -  ��������� ���� ��������
// CombinedSDK.LoadProgressData()  -  ��������� ���� ��������

// ��� ���������� ����� ���������� ��������� � ������ SavesCombinedSDK
// ��� ��������� �������� � ���������� �� ����� ���� �����������:
// CombinedSDK.AllSavesCombinedSDK.��������
// ���
// [SerializeField] private SavesCombinedSDK _saveDataSDK
// _saveDataSDK.��������

#endif

public static class CombinedSDK
{
    public static System.Action OnCombinedSDKInitilizedEvent;
    public static System.Action<string> OnCombinedSDKCloseRewardVideo;
    public static System.Action<string> OnCombinedSDKPurchasedItem;

    private static SDK SelectedSDK;

    public static SavesCombinedSDK AllSavesCombinedSDK;

    public static bool IsInitilized { get; private set; }



    public static void Initilized(SDK executor)
    {
        if (!IsInitilized)
        {
            SelectedSDK = executor;
            AllSavesCombinedSDK = executor.DataSavesCombinedSDK;
            IsInitilized = true;

            OnCombinedSDKInitilizedEvent?.Invoke();
            SelectedSDK.LoadProgressData();
        }
        else
        {
            UnityEngine.Debug.Log("<color=yellow>�� �������� ������ ���������������� CombinedSDK </color>" +
                "<color=red>[000]</color>");
        }
    }

    // �������������� �� �������� �������
    public static void Rewarded(int id)
    {
        if (id == 0)
        {
            // ������:
            UnityEngine.Debug.Log("<color=yellow>�������� �������</color>");
        }
        else if (id == 2)
        {

        }
    }

    // AD
    /// <summary>
    /// �������� �������������� ��������� ���������� �� �������
    /// </summary>
    /// <param name="id">������� ������� ������������ � CombinedSDK.Rewarded</param>
    public static void OpenRewardAd(string id)
    {
        CheckInitilized();
        SelectedSDK.OpenRewardAd(id);
    }
    /// <summary>
    /// �������� ������� ������������ �������
    /// </summary>
    public static void OpenFullscreenAd()
    {
        CheckInitilized();
        SelectedSDK.OpenFullscreenAd();
    }
    /// <summary>
    /// �������� ���������������� ������������ �������
    /// </summary>
    public static void OpenFastFullscreenAd()
    {
        CheckInitilized();
        SelectedSDK.OpenFastFullscreenAd();
    }

    //Banners
    /// <summary>
    /// �������� ��� ������� �� �������
    /// ���� UnityADS - �� �������� ������������ ������
    /// </summary>
    public static void ShowAllBannersAd()
    {
        CheckInitilized();
        SelectedSDK.ShowAllBannersAd();
    }
    /// <summary>
    /// ������ ��� ������� �� �������
    /// ���� UnityADS - �� ������ ������������ ������
    /// </summary>
    public static void HideAllBannersAd()
    {
        CheckInitilized();
        SelectedSDK.HideAllBannersAd();
    }
#if CRAZY_GAMES
    /// <summary>
    /// �������� ���������� � ����� ������
    /// </summary>
    public static void ShowBannerAd(CrazyGames.CrazyBanner _crazyBanner)
    {
        CheckInitilized();
        SelectedSDK.ShowBannerAd(_crazyBanner);
    }
    /// <summary>
    /// ������ ���������� � ����� ������
    /// </summary>
    public static void HideBannerAd(CrazyGames.CrazyBanner _crazyBanner)
    {
        CheckInitilized();
        SelectedSDK.HideBannerAd(_crazyBanner);
    }
#endif

    //Purchases
    /// <summary>
    /// ������ ������� �� �������� ������
    /// </summary>
    /// <param name="id">id ����������� ��������</param>
    public static void BuyPayments(string id)
    {
        CheckInitilized();
        SelectedSDK.BuyPayments(id);
    }
    /// <summary>
    /// �������� ������ � ������ ���� ��������� �������
    /// </summary>
    /// <param name="go">������ � �������� �������</param>
    public static void AddPayments(UnityEngine.GameObject go)
    {
        CheckInitilized();
        SelectedSDK.AddPayments(go);
    }

    //Metrics
    /// <summary>
    /// ���������� ������ � ���������� ����-����
    /// </summary>
    /// <param name="metricId">�������� �������</param>
    public static void SendMetric(string metricId)
    {
        CheckInitilized();
        SelectedSDK.SendMetric(metricId);
    }
    /// <summary>
    /// ���������� ������ � ���������� ����-���� � ��������� �������
    /// </summary>
    /// <param name="metricId">�������� �������</param>
    /// <param name="metricDetalName">�������� ��������� �������</param>
    public static void SendDetalMetric(string metricId, string metricDetalName)
    {
        CheckInitilized();
        SelectedSDK.SendDetailMetric(metricId, metricDetalName);
    }

    //SaveData
    /// <summary>
    /// ��������� ���� SavesCombinedSDK
    /// </summary>
    public static void SaveProgressData()
    {
        CheckInitilized();
        SelectedSDK.SaveProgressData();
    }
    /// <summary>
    /// ��������� � ���� SavesCombinedSDK
    /// </summary>
    public static void LoadProgressData()
    {
        CheckInitilized();
        SelectedSDK.LoadProgressData();
    }
    public static void ResetProgressData()
    {
        CheckInitilized();
        SelectedSDK.ResetProgressData();
    }
    private static void CheckInitilized()
    {
        if (!IsInitilized)
            throw new System.Exception("CombinedSDK �� ���������������" +
                "<color=red>[999]</color>");
    }
}
