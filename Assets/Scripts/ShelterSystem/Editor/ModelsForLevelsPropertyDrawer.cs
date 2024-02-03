using ShelterSystem;
using UnityEditor;
using UnityEngine;
using static ShelterSystem.ShelterUpgradeElement;


[CustomPropertyDrawer(typeof(ModelForLevel))]
public class ModelsForLevelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Calculate Rects
        float levelWidth = 60f;
        float spacing = 8f;
        float modelWidth = position.width - levelWidth - spacing;
        Rect levelRect = new Rect(position.x, position.y, levelWidth, position.height);
        Rect modelRect = new Rect(position.x + levelWidth + spacing, position.y, modelWidth, position.height);

        // Draw fields
        EditorGUI.PropertyField(levelRect, property.FindPropertyRelative("Level"), GUIContent.none);
        EditorGUI.PropertyField(modelRect, property.FindPropertyRelative("Model"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}