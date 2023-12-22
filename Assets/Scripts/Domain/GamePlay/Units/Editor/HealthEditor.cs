using UnityEngine;
using UnityEditor;
using Utils.Editor;

namespace Domain.GamePlay.Units.Editor
{
    [CustomEditor(typeof(Health))]
    public class HealthEditor : UnityEditor.Editor
    {
        private SerializedProperty _maxHealthProperty;
        private SerializedProperty _currentHealthProperty;
        private SerializedProperty _printTraceProperty;

        private void OnEnable()
        {
            _maxHealthProperty = serializedObject.FindProperty("_maxHealth");
            _currentHealthProperty = serializedObject.FindProperty("_value");
            _printTraceProperty = serializedObject.FindProperty("_printTrace");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawValuesControlls();
            EditorGUILayout.Space(10);
            DrawPrintControlls();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPrintControlls()
        {
            EditorGUILayout.PropertyField(_printTraceProperty);
        }

        private void DrawValuesControlls()
        {
            float maxHealth = _maxHealthProperty.floatValue;
            float currentHealth = _currentHealthProperty.floatValue;

            // Ensure that CurrentHealth is within the valid range.
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUIUtility.labelWidth = 75.0f;
            EditorGUILayout.PropertyField(_currentHealthProperty);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_maxHealthProperty);

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.FloatField(0, new GUILayoutOption[] { GUILayout.MaxWidth(EditorGUIUtility.singleLineHeight), GUILayout.MinWidth(EditorGUIUtility.singleLineHeight) });
            EditorGUI.EndDisabledGroup();

            Rect sliderPosition = EditorGUILayout.GetControlRect();

            EditorGUI.BeginChangeCheck();
            currentHealth = GUI.HorizontalSlider(sliderPosition, currentHealth, 0f, maxHealth);
            if (EditorGUI.EndChangeCheck())
                _currentHealthProperty.floatValue = currentHealth;
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_maxHealthProperty, GUIContent.none, new GUILayoutOption[] { GUILayout.MaxWidth(50.0f), GUILayout.MinWidth(40.0f) });
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            DrawButtons();
        }

        private void DrawButtons() => new ButtonsDrawer(serializedObject.targetObject).DrawButtons();
    }
}