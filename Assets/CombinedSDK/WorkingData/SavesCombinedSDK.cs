using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-100)]
[CreateAssetMenu(fileName = "CombinedSDKSaves", menuName = "DataCombinedSDK", order = 51)]
public class SavesCombinedSDK : ScriptableObject
{
    // "Технические сохранения" для работы плагина (Не удалять)
    public int idSave;
    public bool isFirstSession = true;
    public string language = "ru";
    public bool promptDone;

    // Тестовые сохранения для демо сцены
    // Можно удалить этот код, но тогда удалите и демо (папка Example)
    public int money = 1;                       // Можно задать полям значения по умолчанию
    public string newPlayerName = "Hello!";
    public bool[] openLevels = new bool[3];

    // Ваши сохранения
    public int Balance;
    public Dictionary<string, bool> openedWeapons = new Dictionary<string, bool>();
    public int ShelterLevel;
    public int LastWaveIndex;
    public int LastGunIndex;


    // Вы так же можете задать значения по умолчанию
    public void DefaultValue()
    {
        openLevels[1] = true;
        openedWeapons["pistol"] = true;
        Balance = 0;
        ShelterLevel = 1;
        LastWaveIndex = 0;
        LastGunIndex = 0;
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