using System;
using UnityEditor;
using UnityEngine;

namespace Hairibar.NaughtyExtensions.Editor
{
    internal static class NonLinearSliderDrawer
    {
        const float SLIDER_NUMBER_FIELD_WIDTH = 50;
        const float SLIDER_MARGIN_SIZE = 5;

        #region Public Drawing API
        public static void Draw_Layout(SerializedProperty serializedProperty, float leftValue, float rightValue, Function function)
        {
            Draw_Layout(serializedProperty, leftValue, rightValue, function,
                new GUIContent(serializedProperty.displayName, serializedProperty.tooltip));
        }

        public static void Draw_Layout(SerializedProperty serializedProperty, float leftValue, float rightValue, Function function, GUIContent guiContent)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            EditorGUI.BeginProperty(rect, GUIContent.none, serializedProperty);

            serializedProperty.floatValue = Draw(rect, guiContent, serializedProperty.floatValue, leftValue, rightValue, function);

            EditorGUI.EndProperty();
        }

        public static float Draw_Layout(GUIContent guiContent, float value, float leftValue, float rightValue, Function function)
        {
            Rect rect = EditorGUILayout.GetControlRect();

            return Draw(rect, guiContent, value, leftValue, rightValue, function);
        }


        public static void Draw(Rect rect, SerializedProperty serializedProperty, float leftValue, float rightValue, Function function)
        {
            Draw(rect, serializedProperty, leftValue, rightValue, function, new GUIContent(serializedProperty.displayName, serializedProperty.tooltip));
        }

        public static void Draw(Rect rect, SerializedProperty serializedProperty, float leftValue, float rightValue, Function function, GUIContent guiContent)
        {
            EditorGUI.BeginProperty(rect, GUIContent.none, serializedProperty);

            serializedProperty.floatValue = Draw(rect, guiContent, serializedProperty.floatValue, leftValue, rightValue, function);

            EditorGUI.EndProperty();
        }

        public static float Draw(Rect rect, GUIContent guiContent, float value, float leftValue, float rightValue, Function function)
        {
            Rect controlRect;
            if (guiContent != null) controlRect = EditorGUI.PrefixLabel(rect, guiContent);
            else controlRect = rect;

            bool sliderChanged = DrawSlider(value, leftValue, rightValue, function, ref controlRect, out float sliderValue);
            bool numericInputFieldChanged = DrawNumericInputField(value, leftValue, rightValue, controlRect, out float numericInputFieldValue);

            if (sliderChanged)
            {
                return sliderValue;
            }
            else if (numericInputFieldChanged)
            {
                return numericInputFieldValue;
            }
            else
            {
                return value;
            }
        }


        static bool DrawSlider(float value, float leftValue, float rightValue, Function function, ref Rect controlRect, out float inputValue)
        {
            EditorGUI.BeginChangeCheck();

            Rect sliderRect = new Rect(controlRect.x, controlRect.y, controlRect.width - SLIDER_NUMBER_FIELD_WIDTH - SLIDER_MARGIN_SIZE, controlRect.height);

            float sliderValue = GUI.HorizontalSlider(sliderRect, function.backwardsFunction(value), leftValue, rightValue);
            float convertedValue = function.function(sliderValue);

            inputValue = convertedValue;
            return EditorGUI.EndChangeCheck();
        }

        static bool DrawNumericInputField(float currentValue, float min, float max, Rect controlRect, out float inputValue)
        {
            EditorGUI.BeginChangeCheck();

            Rect numberRect = new Rect(controlRect.xMax - SLIDER_NUMBER_FIELD_WIDTH, controlRect.y, SLIDER_NUMBER_FIELD_WIDTH, controlRect.height);
            float floatFieldValue = EditorGUI.FloatField(numberRect, currentValue);

            inputValue = Mathf.Clamp(floatFieldValue, min, max);
            return EditorGUI.EndChangeCheck();
        }
        #endregion


        public struct Function
        {
            public Func<float, float> function;
            public Func<float, float> backwardsFunction;
        }
    }
}
