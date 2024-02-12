using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-100)]
[CreateAssetMenu(fileName = "CombinedSDKSaves", menuName = "DataCombinedSDK", order = 51)]
public class SavesCombinedSDK : ScriptableObject
{
    // ��� ������������� YandexGamesSDKPlugin, �� �� ������� ������������ �������� �����������
    // ����� ���� �����! (�.�. ������ ������ ����� ���� ����� ���������� [SavesYG])

    // ���� ����������

    // ...

    // ������:
    public int money;


    // �� ��� �� ������ ������ �������� �� ���������
    public void DefaultValue()
    {
        // ������:
        // money = 5;
    }
}


#region EditorInspector
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
#endregion