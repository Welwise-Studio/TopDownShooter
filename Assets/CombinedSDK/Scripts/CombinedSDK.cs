#if UNITY_EDITOR
// Обучение по скрипту:

// AD:
// CombinedSDK.OpenRewardAd(int id)   -  Вызвать видеоролик за вознаграждение
// CombinedSDK.OpenFullscreenAd()     -  Вызвать полноэкранную рекламу
// CombinedSDK.OpenFastFullscreenAd() -  Вызвать быструю полноэкранную рекламу [только с GameDistributionSDK]

// Banners:
// CombinedSDK.ShowAllBannersAd()               -  Показать все баннеры из массива [только с CrazyGames или UnityAds]
// CombinedSDK.HideAllBannersAd()               -  Спрятать все баннеры из массива [только с CrazyGames или UnityAds]
// CombinedSDK.ShowBannerAd(CrazyBanner banner) -  Показать баннер [только с CrazyGames]
// CombinedSDK.HideBannerAd(CrazyBanner banner) -  Спрятать баннер [только с CrazyGames]

// Purchases:
// CombinedSDK.BuyPayments(string id)     -  Купить предмет [только с YandexGames]
// CombinedSDK.AddPayments(GameObject go) -  Добавить новый предмет в список ВСЕХ покупок [только с YandexGames]

// Metrics:
// SendMetric(string metricId)                              -  Отправляет данные о достижении чего-либо [только с YandexGames]
// SendDetalMetric(string metricId, string metricDetalName) -  Отправляет данные в вложенные метрики [только с YandexGames]
// Для правильной работы метрик зайдите в документацию PluginYG

// SaveData:
// CombinedSDK.SaveProgressData()  -  Сохранить весь прогресс
// CombinedSDK.LoadProgressData()  -  Загрузить весь прогресс

// Для добавления новых переменных перейдите в скрипт SavesCombinedSDK
// Для изменения значений у переменных во время игры используйте:
// CombinedSDK.AllSavesCombinedSDK.Название
// или
// [SerializeField] private SavesCombinedSDK _saveDataSDK
// _saveDataSDK.Название

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
            UnityEngine.Debug.Log("<color=yellow>вы повторно хотите инициализировать CombinedSDK </color>" +
                "<color=red>[000]</color>");
        }
    }

    // Вознограждение за просмотр рекламы
    public static void Rewarded(int id)
    {
        if (id == 0)
        {
            // Пример:
            UnityEngine.Debug.Log("<color=yellow>Тестовая награда</color>");
        }
        else if (id == 2)
        {

        }
    }

    // AD
    /// <summary>
    /// Вызывает непропускаемый рекламный видеоролик за награду
    /// </summary>
    /// <param name="id">Награда которая определяется в CombinedSDK.Rewarded</param>
    public static void OpenRewardAd(string id)
    {
        CheckInitilized();
        SelectedSDK.OpenRewardAd(id);
    }
    /// <summary>
    /// Вызывает обычную пропускаемую рекламу
    /// </summary>
    public static void OpenFullscreenAd()
    {
        CheckInitilized();
        SelectedSDK.OpenFullscreenAd();
    }
    /// <summary>
    /// Вызывает быстровызывающую пропускаемую рекламу
    /// </summary>
    public static void OpenFastFullscreenAd()
    {
        CheckInitilized();
        SelectedSDK.OpenFastFullscreenAd();
    }

    //Banners
    /// <summary>
    /// Показать все баннеры из массива
    /// Если UnityADS - то показать единственный баннер
    /// </summary>
    public static void ShowAllBannersAd()
    {
        CheckInitilized();
        SelectedSDK.ShowAllBannersAd();
    }
    /// <summary>
    /// Скрыть все баннеры из массива
    /// Если UnityADS - то скрыть единственный баннер
    /// </summary>
    public static void HideAllBannersAd()
    {
        CheckInitilized();
        SelectedSDK.HideAllBannersAd();
    }
#if CRAZY_GAMES
    /// <summary>
    /// Показать переданный в метод баннер
    /// </summary>
    public static void ShowBannerAd(CrazyGames.CrazyBanner _crazyBanner)
    {
        CheckInitilized();
        SelectedSDK.ShowBannerAd(_crazyBanner);
    }
    /// <summary>
    /// Скрыть переданный в метод баннер
    /// </summary>
    public static void HideBannerAd(CrazyGames.CrazyBanner _crazyBanner)
    {
        CheckInitilized();
        SelectedSDK.HideBannerAd(_crazyBanner);
    }
#endif

    //Purchases
    /// <summary>
    /// Купить предмет за реальные деньги
    /// </summary>
    /// <param name="id">id покупаемого предмета</param>
    public static void BuyPayments(string id)
    {
        CheckInitilized();
        SelectedSDK.BuyPayments(id);
    }
    /// <summary>
    /// Добавить объект в список всех возможных покупок
    /// </summary>
    /// <param name="go">объект с скриптом покупки</param>
    public static void AddPayments(UnityEngine.GameObject go)
    {
        CheckInitilized();
        SelectedSDK.AddPayments(go);
    }

    //Metrics
    /// <summary>
    /// Отправляет данные о достижении чего-либо
    /// </summary>
    /// <param name="metricId">Название метрики</param>
    public static void SendMetric(string metricId)
    {
        CheckInitilized();
        SelectedSDK.SendMetric(metricId);
    }
    /// <summary>
    /// Отправляет данные о достижении чего-либо в вложенные метрики
    /// </summary>
    /// <param name="metricId">Название метрики</param>
    /// <param name="metricDetalName">Название вложенной метрики</param>
    public static void SendDetalMetric(string metricId, string metricDetalName)
    {
        CheckInitilized();
        SelectedSDK.SendDetailMetric(metricId, metricDetalName);
    }

    //SaveData
    /// <summary>
    /// Сохранить файл SavesCombinedSDK
    /// </summary>
    public static void SaveProgressData()
    {
        CheckInitilized();
        SelectedSDK.SaveProgressData();
    }
    /// <summary>
    /// Загрузить в файл SavesCombinedSDK
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
            throw new System.Exception("CombinedSDK не инициализирован" +
                "<color=red>[999]</color>");
    }
}
