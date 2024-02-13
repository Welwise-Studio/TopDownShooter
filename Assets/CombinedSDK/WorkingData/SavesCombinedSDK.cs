using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-100)]
[CreateAssetMenu(fileName = "CombinedSDKSaves", menuName = "DataCombinedSDK", order = 51)]
public class SavesCombinedSDK : ScriptableObject
{
    // "����������� ����������" ��� ������ ������� (�� �������)
    public int idSave;
    public bool isFirstSession = true;
    public string language = "ru";
    public bool promptDone;

    // �������� ���������� ��� ���� �����
    // ����� ������� ���� ���, �� ����� ������� � ���� (����� Example)
    public int money = 1;                       // ����� ������ ����� �������� �� ���������
    public string newPlayerName = "Hello!";
    public bool[] openLevels = new bool[3];

    // ���� ����������
    public int Balance;
    public Dictionary<string, bool> openedWeapons = new Dictionary<string, bool>();
    public int ShelterLevel;
    public int LastWaveIndex;
    public int LastGunIndex;


    // �� ��� �� ������ ������ �������� �� ���������
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