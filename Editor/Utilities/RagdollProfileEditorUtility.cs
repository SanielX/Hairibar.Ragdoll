using UnityEditor;
using UnityEngine;

namespace Hairibar.Ragdoll.Editor
{
    internal static class RagdollProfileEditorUtility
    {
        public static void DrawDefinitionField_Layout(SerializedProperty definition, bool isRequired)
        {
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            EditorGUILayout.PropertyField(definition);
            EditorGUI.EndDisabledGroup();

            if (isRequired && !HasDefinition(definition))
            {
                EditorGUILayout.HelpBox("A RagdollDefinition must be assigned.", MessageType.Error);
            }
            else if (HasDefinition(definition) && !IsValid(definition))
            {
                EditorGUILayout.HelpBox("The Ragdoll Definition is invalid.", MessageType.Error);
            }
        }

        public static bool HasDefinition(SerializedProperty definition)
        {
            return definition.objectReferenceValue;
        }

        public static bool IsValid(SerializedProperty definition)
        {
            return HasDefinition(definition) ?
                new SerializedObject(definition.objectReferenceValue).FindProperty("_isValid").boolValue :
                false;
        }


        public static void RefreshBoneDictionaryKeys(SerializedProperty dictionary, SerializedProperty definition, System.Action<SerializedProperty> defaultValueInitializer = null)
        {
            SerializedProperty bonesArray = new SerializedObject(definition.objectReferenceValue).FindProperty("bones");
            SerializedProperty keys = dictionary.FindPropertyRelative("keys");

            keys.arraySize = bonesArray.arraySize;
            for (int i = 0; i < keys.arraySize; i++)
            {
                keys.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue = bonesArray.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue;
            }

            ResizeDictionaryValues(dictionary.FindPropertyRelative("values"), keys.arraySize, defaultValueInitializer);
        }

        static void ResizeDictionaryValues(SerializedProperty values, int newSize, System.Action<SerializedProperty> defaultValueInitializer)
        {
            int oldSize = values.arraySize;
            values.arraySize = newSize;

            if (defaultValueInitializer != null)
            {
                for (int i = oldSize; i < newSize; i++)
                {
                    defaultValueInitializer(values.GetArrayElementAtIndex(i));
                }
            }
        }


        public static bool ValidateProfileField_Layout(SerializedProperty property, RagdollDefinition definition, bool isRequired)
        {
            string type = ReflectionUtility.GetField(property.serializedObject.targetObject, property.name).FieldType.Name;

            if (!property.objectReferenceValue)
            {
                if (isRequired)
                {
                    EditorGUILayout.HelpBox($"A {type} is required.", MessageType.Error);
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else if (!(property.objectReferenceValue as RagdollProfile).IsValid)
            {
                EditorGUILayout.HelpBox($"The assigned {type} is not valid.", MessageType.Error);
                return false;
            }
            else if (definition != null && (property.objectReferenceValue as RagdollProfile).IsCompatibleWith(definition) != definition)
            {
                EditorGUILayout.HelpBox($"The bound RagdollDefinition and the {type}'s RagdollDefinition are incompatible.", MessageType.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
