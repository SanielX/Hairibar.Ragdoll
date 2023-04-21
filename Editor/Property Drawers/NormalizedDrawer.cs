using UnityEditor;
using UnityEngine;

namespace Hairibar.NaughtyExtensions.Editor
{
    [CustomPropertyDrawer(typeof(NormalizedAttribute))]
    internal class NormalizedValidator : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, GUIContent.none, property);

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    property.vector2Value = EditorGUI.Vector2Field(rect, label, property.vector2Value).normalized;
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = EditorGUI.Vector3Field(rect, label, property.vector3Value).normalized;
                    break;
                default:
                    EditorGUI.HelpBox(rect, "[Normalized] attribute only supports Vector2 and Vector3 types.", MessageType.Warning);
                    break;
            }

            EditorGUI.EndProperty();
        }
    }
}
