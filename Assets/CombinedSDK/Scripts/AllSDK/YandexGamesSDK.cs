#if YANDEX_GAMES
using System.Collections.Generic;
using UnityEngine;
using YG;
using YG.Utils.Pay;
#endif

public class YandexGamesSDK : SDK
{
#if YANDEX_GAMES
    [Header("Все покупки")]
    [SerializeField] private List<PurchaseYG> _allPurchasesGO = new List<PurchaseYG>();
    [Header("Id всех покупок с многоразовым использованием")]
    [SerializeField] private List<string> _allReusablePurchasesId = new List<string>();
    [Header("Использовать PlayerPrefs вместо SavesYG")]
    [SerializeField] private bool _usePlayerPrefs = false;
    [Header("Отправка метрик в яндекс")]
    [SerializeField] private bool _useMetrics = false;
    private static bool LastShoppingCompleted;

    public void Start()
    {
        if (YandexGame.SDKEnabled)
            ConsumePurchases();
        UpdatePurchases();
    }

    private void OnEnable()
    {
        // Подписываемся на событие открытия рекламы в OnEnable для Яндекса
        YandexGame.RewardVideoEvent += OnRewardedVideoSuccess;
        YandexGame.ErrorVideoEvent += ErrorAD;

        YandexGame.PurchaseSuccessEvent += PurchaseSuccess;
        YandexGame.PurchaseFailedEvent += FailedPurchased;

        YandexGame.GetDataEvent += ConsumePurchases;
        YandexGame.GetDataEvent += LoadProgressData;
    }

    private void OnDisable()
    {
        // Отписываемся от события открытия рекламы в OnDisable для Яндекса
        YandexGame.RewardVideoEvent -= OnRewardedVideoSuccess;
        YandexGame.ErrorVideoEvent -= ErrorAD;

        YandexGame.PurchaseSuccessEvent -= PurchaseSuccess;
        YandexGame.PurchaseFailedEvent -= FailedPurchased;

        YandexGame.GetDataEvent -= ConsumePurchases;
        YandexGame.GetDataEvent -= LoadProgressData;
    }

    public override void SaveProgressData()
    {
        if (_usePlayerPrefs)
            base.SaveProgressData();
        else
        {
            foreach (var saveData in CombinedSDK.AllSavesCombinedSDK.GetType().GetFields())
            {
                bool dontFindName = false;
                foreach (var ygSaveData in YandexGame.savesData.GetType().GetFields())
                {
                    if (saveData.Name == ygSaveData.Name)
                    {
                        if (saveData.FieldType == ygSaveData.FieldType)
                        {
                            ygSaveData.SetValue(YandexGame.savesData, saveData.GetValue(CombinedSDK.AllSavesCombinedSDK));
                        }
                        else
                            DebugError(902, "SavesCombined: " + saveData.Name + ", YG:" + ygSaveData.Name);

                        dontFindName = false;
                        break;
                    }

                    dontFindName = true;
                }

                if (dontFindName)
                    DebugError(903, saveData.Name);
            }

            YandexGame.SaveProgress();
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
                bool dontFindName = false;
                foreach (var ygSaveData in YandexGame.savesData.GetType().GetFields())
                {
                    if (saveData.Name == ygSaveData.Name)
                    {
                        if (saveData.FieldType == ygSaveData.FieldType)
                        {
                            saveData.SetValue(CombinedSDK.AllSavesCombinedSDK, ygSaveData.GetValue(YandexGame.savesData));
                        }
                        else
                            DebugError(902, "SavesCombined: " + saveData.Name + ", YG:" + ygSaveData.Name);

                        dontFindName = false;
                        break;
                    }

                    dontFindName = true;
                }

                if (dontFindName)
                    DebugError(903, saveData.Name);
            }
        }
    }
    private void DisablePurchasedItem(string idYG = null)
    {
        CombinedSDK.OnCombinedSDKPurchasedItem?.Invoke(idYG);

        // ---------------------------------------------------------------
        // Ваша реализация отключения объекта, если объект куплен

        // idYG - id предмета который купил игрок

        //Пример:

        //if (!ShopManager.PurchasedIndexItems.Contains(Convert.Int32(idYG)))
        //    ShopManager.PurchasedIndexItems.Add(Convert.Int32(idYG));

        // Если реализовали отключение объекта, то закомментируйте строчку ниже!
        DebugError(990);


        // ---------------------------------------------------------------
    }
    public override void BuyPayments(string id)
    {
        if (_allReusablePurchasesId.Contains(id))
        {
            YandexGame.BuyPayments(id);
        }
        else
        {
            if (!YandexGame.PurchaseByID(id).consumed)
            {
                YandexGame.BuyPayments(id);
            }
        }
    }
    public override void AddPayments(GameObject go)
    {
        if (go.GetComponent<PurchaseYG>())
        {
            if (!_allPurchasesGO.Contains(go.GetComponent<PurchaseYG>()))
            {
                _allPurchasesGO.Add(go.GetComponent<PurchaseYG>());
                UpdatePurchases();
            }
        }
        else
        {
            DebugError(900, go.name);
        }
    }
    private void ConsumePurchases()
    {
        if (!LastShoppingCompleted)
        {
            LastShoppingCompleted = true;
            YandexGame.ConsumePurchases();
        }
    }
    private void PurchaseSuccess(string idYG)
    {
        DisablePurchasedItem(idYG);
    }
    private void UpdatePurchases()
    {
        if (_allPurchasesGO.Count > 0)
        {
            for (int indexItem = 0; indexItem < _allPurchasesGO.Count; indexItem++)
            {
                Purchase purchase = YandexGame.PurchaseByID(_allPurchasesGO[indexItem].data.id);
                if (purchase != null)
                {
                    if (_allPurchasesGO[indexItem].data.consumed)
                        DisablePurchasedItem(_allPurchasesGO[indexItem].data.id);
                }
                else
                {
                    Debug.LogError($"<color=yellow>[YandexGames] Товара с таким id: {purchase.id} не существует!</color>");
                }
            }
        }
    }

    private void OnRewardedVideoSuccess(int id)
    {
        CombinedSDK.OnCombinedSDKCloseRewardVideo?.Invoke(id.ToString());
    }

    public override void OpenRewardAd(string id)
    {
        YandexGame.RewVideoShow(int.Parse(id));
    }
    public override void OpenFullscreenAd()
    {
        YandexGame.FullscreenShow();
    }
    public override void OpenFastFullscreenAd()
    {
        YandexGame.FullscreenShow();
    }
    public override void ShowAllBannersAd()
    {
        DebugError(100);
    }
    public override void HideAllBannersAd()
    {
        DebugError(100);
    }

    public override void SendMetric(string metricId)
    {
        YandexMetrica.Send(metricId);
    }

    public override void SendDetailMetric(string metricId, string metricDetalName)
    {
        var eventParams = new Dictionary<string, string>
        {
            { metricId, metricDetalName }
        };

        YandexMetrica.Send(metricId, eventParams);
    }

    public override void ResetProgressData()
    {
        if (_usePlayerPrefs)
            base.ResetProgressData();
        else
            YandexGame.ResetSaveProgress();
    }

    private void ErrorAD()
    {
        DebugError(400);
    }
    private void FailedPurchased(string purchasedId)
    {
        DebugError(500, purchasedId);
    }
#else
    public override void LoadProgressData()
    {
        DebugError(991);
    }

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
