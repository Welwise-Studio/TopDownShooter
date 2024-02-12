using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-100)]
[CreateAssetMenu(fileName = "CombinedSDKSaves", menuName = "DataCombinedSDK", order = 51)]
public class SavesCombinedSDK : ScriptableObject
{
    // ѕри использовании YandexGamesSDKPlugin, вы не сможете пользоватьс€ облачным сохранением
    // через этот класс! (т.к. яндекс плагин имеет свой класс сохранений [SavesYG])

    // ¬аши сохранени€

    // ...

    // ѕример:
    public int money;


    // ¬ы так же можете задать значени€ по умолчанию
    public void DefaultValue()
    {
        // ѕример:
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