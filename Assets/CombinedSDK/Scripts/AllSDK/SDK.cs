#if CRAZY_GAMES
using CrazyGames;
#endif

using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public abstract class SDK : MonoBehaviour
{
    [Tooltip("������ ������ ������������ ��� ���������� PlayerPrefs")]
    public SavesCombinedSDK DataSavesCombinedSDK;

    public void Awake()
    {
        CombinedSDK.Initilized(this);
    }


    //AD
    public abstract void OpenRewardAd(string id);

    public abstract void OpenFullscreenAd();

    public virtual void OpenFastFullscreenAd()
    {
        DebugError(100);
    }

    //Banners
    public virtual void ShowAllBannersAd()
    {
        DebugError(100);
    }
    public virtual void HideAllBannersAd()
    {
        DebugError(100);
    }
#if CRAZY_GAMES
    public virtual void ShowBannerAd(CrazyBanner banner) { }
    public virtual void HideBannerAd(CrazyBanner banner) { }
#endif

    //Purchases
    public virtual void BuyPayments(string id)
    {
        DebugError(100);
    }
    public virtual void AddPayments(GameObject go)
    {
        DebugError(100);
    }

    //Metrics
    public virtual void SendMetric(string metricId)
    {
        DebugError(100);
    }
    public virtual void SendDetailMetric(string metricId, string metricDetalName)
    {
        DebugError(100);
    }

    //SaveData
    public virtual void SaveProgressData()
    {
        //PlayerPrefs
        foreach (var saveData in CombinedSDK.AllSavesCombinedSDK.GetType().GetFields())
        {
            if (saveData.FieldType == typeof(string))
            {
                PlayerPrefs.SetString(saveData.Name, saveData.GetValue(CombinedSDK.AllSavesCombinedSDK).ToString());
            }
            else if (saveData.FieldType == typeof(int))
            {
                PlayerPrefs.SetInt(saveData.Name, Convert.ToInt32(saveData.GetValue(CombinedSDK.AllSavesCombinedSDK)));
            }
            else if (saveData.FieldType == typeof(float))
            {
                PlayerPrefs.SetFloat(saveData.Name, Convert.ToSingle(saveData.GetValue(CombinedSDK.AllSavesCombinedSDK)));
            }
            else
                DebugError(902, saveData.Name);
        }
    }
    public virtual void LoadProgressData()
    {
        //PlayerPrefs
        foreach (var saveData in CombinedSDK.AllSavesCombinedSDK.GetType().GetFields())
        {
            if (PlayerPrefs.HasKey(saveData.Name))
            {
                if (saveData.FieldType == typeof(string))
                {
                    try
                    {
                        saveData.SetValue(CombinedSDK.AllSavesCombinedSDK, PlayerPrefs.GetString(saveData.Name));
                    }
                    catch
                    {
                        DebugError(901, saveData.Name);
                    }
                }
                else if (saveData.FieldType == typeof(int))
                {
                    try
                    {
                        saveData.SetValue(CombinedSDK.AllSavesCombinedSDK, PlayerPrefs.GetInt(saveData.Name));
                    }
                    catch
                    {
                        DebugError(901, saveData.Name);
                    }
                }
                else if (saveData.FieldType == typeof(float))
                {
                    try
                    {
                        saveData.SetValue(CombinedSDK.AllSavesCombinedSDK, PlayerPrefs.GetFloat(saveData.Name));
                    }
                    catch
                    {
                        DebugError(901, saveData.Name);
                    }
                }
                else
                    DebugError(902, saveData.Name);
            }
        }
    }
    public virtual void ResetProgressData()
    {
        PlayerPrefs.DeleteAll();
        CombinedSDK.AllSavesCombinedSDK.DefaultValue();
    }

    // ���������� �� �������
    // ����� - ����������� ������
    // ������� - ��������� ���������� ������ ����
    // ������� - ���������� ����� ������ � ���� ���������
    public virtual void DebugError(int indexError, string textInfo = null)
    {
        switch (indexError)
        {
            case 100:
                Debug.Log("<color=yellow>������� ������ �� ���������� � ���� SDK </color>" + 
                    "<color=red>[" + indexError + "]</color>");
                break;
            case 400:
                Debug.Log("<color=yellow>��������� ����� �� ���� �������� </color>" +
                    "<color=red>[" + indexError + "]</color>");
                break;
            case 500:
                Debug.Log("<color=yellow>��������� ������ �� ����� ������� ������ (" + textInfo + ") </color>" +
                    "<color=red>[" + indexError + "]</color>");
                break;
            case 900:
                Debug.Log("<color=yellow>�� ������� (" + textInfo + ") �� ���������� PurchaseYG ���������� </color>" +
                    "<color=red>[" + indexError + "]</color>");
                break;
            case 901:
                Debug.Log("<color=yellow>���������� (" + textInfo + ") � SavesCombinedSDK � �� ������� ��������� � PlayerPrefs �� ��������� � ����� </color>" +
                    "<color=red>[" + indexError + "]</color>");
                break;
            case 902:
                Debug.Log("<color=yellow>���������� (" + textInfo + ") � SavesCombinedSDK ���������� �� ��������������� ��� ��� ���������� </color>" +
                    "<color=red>[" + indexError + "]</color>");
                break;
            case 903:
#if YANDEX_GAMES
                Debug.Log("<color=yellow>���������� (" + textInfo + ") �� ������� � YandexGame.savesData </color>" +
                    "<color=red>[" + indexError + "]</color>");
#else
                Debug.Log("<color=yellow>���������� (" + textInfo + ") �� ������� � SavesCombinedSDK </color>" +
                    "<color=red>[" + indexError + "]</color>");
#endif
                break;
            case 990:
                Debug.Log("<color=yellow>[YandexGamesSDK] �� ����������� ���������� ������, ���� ������ ������ </color>" +
                    "<color=red>[" + indexError + "]</color>");
                break;
            case 991:
                Debug.Log("<color=yellow>�� ����� ���������� �� ���������� ������ SDK </color>" +
                    "<color=red>[" + indexError + "]</color>");
                break;
            case 0:
                Debug.Log("<color=yellow>�������� ������ </color>" +
                    "<color=red>[" + indexError + "]</color>");
                break;
        }
    }
}
