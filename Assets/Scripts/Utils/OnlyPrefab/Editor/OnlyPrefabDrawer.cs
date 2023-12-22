using UnityEditor;
using UnityEngine;

namespace Utils.Editor
{
    [CustomPropertyDrawer(typeof(OnlyPrefabAttribute))]
    public class OnlyPrefabDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            Rect propertyRect = new Rect(position.x, position.y, position.width, position.height);
            var tcolor = GUI.color;
            if (property.objectReferenceValue != null && PrefabUtility.GetPrefabAssetType(property.objectReferenceValue) != PrefabAssetType.Regular)
            {
                GUI.color = Color.red;
                label.text = "⚠️ Only prefabs are allowed";
            }

            EditorGUI.PropertyField(propertyRect, property, label);
            GUI.color = tcolor;
            EditorGUI.EndProperty();
        }
    }
}
