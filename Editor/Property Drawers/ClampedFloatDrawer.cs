using UnityEditor;
using UnityEngine;

namespace Hairibar.NaughtyExtensions.Editor
{
    internal static class ClampedFloatDrawer
    {
        public static void Draw_Layout(SerializedProperty property, float min, float max)
        {
            Draw_Layout(property, property.GetLabelContent(), min, max);
        }

        public static void Draw_Layout(SerializedProperty property, GUIContent guiContent, float min, float max)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            GUIContent label = EditorGUI.BeginProperty(rect, guiContent, property);

            property.floatValue = Draw(rect, property.floatValue, label, min, max);
            EditorGUI.EndProperty();
        }

        public static float Draw_Layout(float currentValue, GUIContent guiContent, float min, float max)
        {
            return Draw(EditorGUILayout.GetControlRect(), currentValue, guiContent, min, max);
        }


        public static void Draw(Rect rect, SerializedProperty property, float min, float max)
        {
            Draw(rect, property, property.GetLabelContent(), min, max);
        }

        public static void Draw(Rect rect, SerializedProperty property, GUIContent guiContent, float min, float max)
        {
            GUIContent label = EditorGUI.BeginProperty(rect, guiContent, property);
            property.floatValue = Draw(rect, property.floatValue, label, min, max);
            EditorGUI.EndProperty();
        }

        public static float Draw(Rect rect, float currentValue, GUIContent guiContent, float min, float max)
        {
            float newValue = EditorGUI.FloatField(rect, guiContent, currentValue);
            return Mathf.Clamp(newValue, min, max);
        }
    }
}
