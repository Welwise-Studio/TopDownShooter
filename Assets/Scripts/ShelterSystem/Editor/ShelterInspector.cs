using UnityEngine;
using UnityEditor;
using ShelterSystem;

[CustomEditor(typeof(Shelter))]
public class ShelterInspector : Editor
{
    private SerializedProperty maxLevelProp;
    private SerializedProperty statsPerLevelProp;
    private SerializedProperty regenerationProp;
    private SerializedProperty elementsProp;
    private SerializedProperty dieModalProp;

    private bool showLevelStatsFull = true;
    private bool showLevelStats = false;
    private float healthFactor = 0.5f;
    private float regenFactor = 0.5f;
    private float startHealth = 100f;
    private float startRegen = 1f;
    private float endHealth = 500f;
    private float endRegen = 5f;

    private void OnEnable()
    {
        maxLevelProp = serializedObject.FindProperty("_maxLevel");
        statsPerLevelProp = serializedObject.FindProperty("_statsPerLevel");
        regenerationProp = serializedObject.FindProperty("_regeneration");
        elementsProp = serializedObject.FindProperty("_elements");
        dieModalProp = serializedObject.FindProperty("_dieModal");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Shelter shelter = (Shelter)target;

        EditorGUILayout.PropertyField(dieModalProp);
        EditorGUILayout.Space(10);

        GUI.enabled = false;
        EditorGUILayout.IntField("Level", shelter.Level);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(maxLevelProp);

        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(regenerationProp, true);


        showLevelStatsFull = EditorGUILayout.Foldout(showLevelStatsFull, "Levels Statistic");
        if (showLevelStatsFull)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            healthFactor = EditorGUILayout.Slider("Health Factor", healthFactor, .0001f, .9999f);
            regenFactor = EditorGUILayout.Slider("Regen Factor", regenFactor, .0001f, .9999f);
            startHealth = EditorGUILayout.FloatField("Start Health", startHealth);
            startRegen = EditorGUILayout.FloatField("Start Regen", startRegen);
            endHealth = EditorGUILayout.FloatField("End Health", endHealth);
            endRegen = EditorGUILayout.FloatField("End Regen", endRegen);

            if (GUILayout.Button("Generate"))
            {
                System.Reflection.MethodInfo generateMethod = typeof(Shelter).GetMethod("GenerateLevelStatsExp", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                Shelter.Stats[] generatedStats = (Shelter.Stats[])generateMethod.Invoke(shelter, new object[] { healthFactor, regenFactor, startHealth, startRegen, endHealth, endRegen });
                statsPerLevelProp.arraySize = generatedStats.Length;
                for (int i = 0; i < generatedStats.Length; i++)
                {
                    statsPerLevelProp.GetArrayElementAtIndex(i).FindPropertyRelative("MaxHealth").floatValue = generatedStats[i].MaxHealth;
                    statsPerLevelProp.GetArrayElementAtIndex(i).FindPropertyRelative("RegenerationPerSecond").floatValue = generatedStats[i].RegenerationPerSecond;
                }
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(10);

            EditorGUILayout.BeginVertical();

            showLevelStats = EditorGUILayout.Foldout(showLevelStats, "Levels");

            if (showLevelStats)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                for (int i = 0; i < shelter.MaxLevel; i++)
                {
                    EditorGUILayout.PropertyField(statsPerLevelProp.GetArrayElementAtIndex(i), new GUIContent($"Level {i}"));
                }
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.Space(15);
            EditorGUILayout.PropertyField(elementsProp, true);

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}