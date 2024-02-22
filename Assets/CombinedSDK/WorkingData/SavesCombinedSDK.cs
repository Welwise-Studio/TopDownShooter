using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

[DefaultExecutionOrder(-100)]
[CreateAssetMenu(fileName = "CombinedSDKSaves", menuName = "DataCombinedSDK", order = 51)]
public class SavesCombinedSDK : ScriptableObject
{
    // "Технические сохранения" для работы плагина (Не удалять)
    public int idSave;
    //public bool isFirstSession = true;
    public string language = "ru";
    //public bool promptDone;

    public Dictionary<string, bool> openedWeapons 
    { 
        get => _openedWeapons.IsNullOrEmpty() ? new Dictionary<string, bool>() : JsonConvert.DeserializeObject<Dictionary<string, bool>>(_openedWeapons); 
        set => _openedWeapons = JsonConvert.SerializeObject(value); 
    }

    // Ваши сохранения
    public int Balance;
    public string _openedWeapons;
    public int ShelterLevel;
    public int LastWaveIndex;
    public int LastGunIndex;


    // Вы так же можете задать значения по умолчанию
    public void DefaultValue()
    {

        openedWeapons["pistol"] = true;
        Balance = 0;
        ShelterLevel = 1;
        LastWaveIndex = 0;
        LastGunIndex = 0;
        _openedWeapons = JsonConvert.SerializeObject(openedWeapons);
    }
}


#region EditorInspector
#if UNITY_EDITOR
[CustomEditor(typeof(SavesCombinedSDK))]
public class VisualSavesCombindeSDK : Editor
{
    private SavesCombinedSDK _savesScript;

    private void OnEnable()
    {
        _savesScript = (SavesCombinedSDK)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(20);

        if (GUILayout.Button("Clear data in PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            _savesScript.DefaultValue();
        }
    }
}
#endif
#endregion