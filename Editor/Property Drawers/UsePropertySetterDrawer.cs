using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Hairibar.NaughtyExtensions.Editor
{
    [CustomPropertyDrawer(typeof(UsePropertySetterAttribute))]
    internal class UsePropertySetterDrawer : PropertyDrawer
    {
        #region Public Drawing API
        public static void Draw_Layout(SerializedProperty serializedProperty)
        {
            Draw_Layout(serializedProperty, serializedProperty.GetLabelContent());
        }

        public static void Draw_Layout(SerializedProperty serializedProperty, GUIContent label)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            Draw(rect, serializedProperty, label);
        }

        public static void Draw(Rect rect, SerializedProperty serializedProperty)
        {
            Draw(rect, serializedProperty, serializedProperty.GetLabelContent());
        }

        public static void Draw(Rect rect, SerializedProperty serializedProperty, GUIContent label)
        {
            string warningMessage = "";

            //Find the property member
            PropertyInfo nativePropertyInfo = FindProperty(serializedProperty);
            if (nativePropertyInfo == null || nativePropertyInfo.SetMethod == null)
            {
                EditorGUI.PropertyField(rect, serializedProperty, label);
                return;
            }

            //Draw the appropiate field and get the value entered by the user in the inspector.
            bool valueChanged = DrawControl(serializedProperty, rect, label, out object valueSetInInspector, ref warningMessage);

            if (valueChanged)
            {
                //Support undo.
                //TODO: As of right now, the SerializedProperty's value change is correctly undone, but the setter doesn't get called with the old value.
                //I'm not sure if that's possible, or even if it would be better that way.
                Undo.RecordObject(serializedProperty.serializedObject.targetObject, $"Changed {serializedProperty.displayName} in inspector.");

                //Call the setter
                nativePropertyInfo.SetValue(serializedProperty.serializedObject.targetObject, valueSetInInspector);

                //Read the value after the setter
                object processedValue = FindField(serializedProperty).GetValue(serializedProperty.serializedObject.targetObject);

                //Set the SerializedProperty value to the read value
                ModifySerializedValue(serializedProperty, processedValue, ref warningMessage);
            }

            //If we have a warning message, show it.
            if (!string.IsNullOrEmpty(warningMessage))
            {
                warningMessage = warningMessage.Trim();
                EditorGUILayout.HelpBox(warningMessage, MessageType.Warning);
            }
        }
        #endregion

        public override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
        {
            Draw(rect, serializedProperty, label);
        }

        /// <summary>
        /// Draws the appropiate control for this serialized property, returns wether the value changed and gives out the new value.
        /// </summary>
        static bool DrawControl(SerializedProperty serializedProperty, Rect rect, GUIContent guiContent, out object newValue, ref string warningMessage)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(rect, serializedProperty, guiContent, true);
            newValue = ReadSerializedValue(serializedProperty, ref warningMessage);

            return EditorGUI.EndChangeCheck();
        }

        static object ReadSerializedValue(SerializedProperty serializedProperty, ref string warningMessage)
        {
            object oldValue = null;
            switch (serializedProperty.propertyType)
            {
                case SerializedPropertyType.Integer:
                    oldValue = serializedProperty.intValue;
                    break;
                case SerializedPropertyType.Boolean:
                    oldValue = serializedProperty.boolValue;
                    break;
                case SerializedPropertyType.Float:
                    oldValue = serializedProperty.floatValue;
                    break;
                case SerializedPropertyType.String:
                    oldValue = serializedProperty.stringValue;
                    break;
                case SerializedPropertyType.Color:
                    oldValue = serializedProperty.colorValue;
                    break;
                case SerializedPropertyType.ObjectReference:
                    oldValue = serializedProperty.objectReferenceValue;
                    break;
                case SerializedPropertyType.LayerMask:
                    oldValue = serializedProperty.intValue;
                    break;
                case SerializedPropertyType.Enum:
                    oldValue = serializedProperty.enumValueIndex;
                    break;
                case SerializedPropertyType.Vector2:
                    oldValue = serializedProperty.vector2Value;
                    break;
                case SerializedPropertyType.Vector3:
                    oldValue = serializedProperty.vector3Value;
                    break;
                case SerializedPropertyType.Vector4:
                    oldValue = serializedProperty.vector4Value;
                    break;
                case SerializedPropertyType.Rect:
                    oldValue = serializedProperty.rectValue;
                    break;
                case SerializedPropertyType.ArraySize:
                    oldValue = serializedProperty.arraySize;
                    break;
                case SerializedPropertyType.Character:
                    oldValue = serializedProperty.stringValue;
                    break;
                case SerializedPropertyType.AnimationCurve:
                    oldValue = serializedProperty.animationCurveValue;
                    break;
                case SerializedPropertyType.Bounds:
                    oldValue = serializedProperty.boundsValue;
                    break;
                case SerializedPropertyType.Quaternion:
                    oldValue = serializedProperty.quaternionValue;
                    break;
                case SerializedPropertyType.ExposedReference:
                    oldValue = serializedProperty.exposedReferenceValue;
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    oldValue = serializedProperty.fixedBufferSize;
                    break;
                case SerializedPropertyType.Vector2Int:
                    oldValue = serializedProperty.vector2IntValue;
                    break;
                case SerializedPropertyType.Vector3Int:
                    oldValue = serializedProperty.vector3IntValue;
                    break;
                case SerializedPropertyType.RectInt:
                    oldValue = serializedProperty.rectIntValue;
                    break;
                case SerializedPropertyType.BoundsInt:
                    oldValue = serializedProperty.boundsIntValue;
                    break;
                default:
                    if (string.IsNullOrEmpty(warningMessage))
                        warningMessage += $"{serializedProperty.propertyType} serialized types aren't supported by UsePropertySetterAttribute.";
                    break;
            }

            return oldValue;
        }

        static void ModifySerializedValue(SerializedProperty serializedProperty, object value, ref string warningMessage)
        {
            switch (serializedProperty.propertyType)
            {
                case SerializedPropertyType.Integer:
                    serializedProperty.intValue = (int) value;
                    break;
                case SerializedPropertyType.Boolean:
                    serializedProperty.boolValue = (bool) value;
                    break;
                case SerializedPropertyType.Float:
                    serializedProperty.floatValue = (float) value;
                    break;
                case SerializedPropertyType.String:
                    serializedProperty.stringValue = (string) value;
                    break;
                case SerializedPropertyType.Color:
                    serializedProperty.colorValue = (Color) value;
                    break;
                case SerializedPropertyType.ObjectReference:
                    serializedProperty.objectReferenceValue = value as Object;
                    break;
                case SerializedPropertyType.LayerMask:
                    serializedProperty.intValue = (LayerMask) value;
                    break;
                case SerializedPropertyType.Enum:
                    serializedProperty.enumValueIndex = (int) value;
                    break;
                case SerializedPropertyType.Vector2:
                    serializedProperty.vector2Value = (Vector2) value;
                    break;
                case SerializedPropertyType.Vector3:
                    serializedProperty.vector3Value = (Vector3) value;
                    break;
                case SerializedPropertyType.Vector4:
                    serializedProperty.vector4Value = (Vector4) value;
                    break;
                case SerializedPropertyType.Rect:
                    serializedProperty.rectValue = (Rect) value;
                    break;
                case SerializedPropertyType.AnimationCurve:
                    serializedProperty.animationCurveValue = (AnimationCurve) value;
                    break;
                case SerializedPropertyType.Bounds:
                    serializedProperty.boundsValue = (Bounds) value;
                    break;
                case SerializedPropertyType.Vector2Int:
                    serializedProperty.vector2IntValue = (Vector2Int) value;
                    break;
                case SerializedPropertyType.Vector3Int:
                    serializedProperty.vector3IntValue = (Vector3Int) value;
                    break;
                case SerializedPropertyType.RectInt:
                    serializedProperty.rectIntValue = (RectInt) value;
                    break;
                case SerializedPropertyType.BoundsInt:
                    serializedProperty.boundsIntValue = (BoundsInt) value;
                    break;
                default:
                    if (string.IsNullOrEmpty(warningMessage))
                        warningMessage += $"{serializedProperty.propertyType} serialized types aren't supported by UsePropertySetterAttribute.";
                    break;
            }
        }

        /// <summary>
        /// Tries to find the member property. Not guaranteed to find one, as the given name (or auto name) could be wrong.
        /// </summary>
        static PropertyInfo FindProperty(SerializedProperty serializedProperty)
        {
            return null;
            // UsePropertySetterAttribute attribute = serializedProperty.GetFieldInfo().GetCustomAttribute<UsePropertySetterAttribute>();
            // 
            // string propertyName;
            // if (attribute.autoFindProperty) propertyName = serializedProperty.displayName.Replace(" ", string.Empty);
            // else propertyName = attribute.propertyName;
            // Object targetObject = serializedProperty.serializedObject.targetObject;
            // 
            // return ReflectionUtility.GetProperty(targetObject, propertyName);
        }

        static FieldInfo FindField(SerializedProperty serializedProperty)
        {
            return null;
            // Object targetObject = serializedProperty.serializedObject.targetObject;
            // return ReflectionUtility.GetField(targetObject, serializedProperty.name);
        }
    }
}