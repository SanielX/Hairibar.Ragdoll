// Based on https://github.com/twsiyuan/unity-ReorderableListUtility

using System;
using UnityEditor;
using UnityEditorInternal;

namespace Hairibar.NaughtyExtensions.Editor
{
    internal static class ReorderableListUtility
    {
        const float VERTICAL_ELEMENT_SPACING = 10;
        const float DRAGGABLE_AREA_WIDTH = 15;

        public static ReorderableList Create(SerializedProperty property, bool draggable, bool displayHeader, bool displayAddButton, bool displayRemoveButton, string header)
        {
            ReorderableList list = new ReorderableList(property.serializedObject, property, draggable, displayHeader, displayAddButton, displayRemoveButton);

            list.drawElementCallback = MakeDrawElementHandler(list);
            list.drawHeaderCallback = MakeDrawHeaderHandler(list, header);
            list.elementHeightCallback = MakeElementHeightCallback(list);

            return list;
        }

        public static void AddDefaultValueSetter(this ReorderableList list, Action<SerializedProperty> defaultValueSetter)
        {
            list.onAddCallback += (ReorderableList l) =>
            {
                int index = l.serializedProperty.arraySize;
                l.serializedProperty.InsertArrayElementAtIndex(index);

                SerializedProperty newEntry = l.serializedProperty.GetArrayElementAtIndex(index);
                defaultValueSetter(newEntry);
            };
        }

        static ReorderableList.ElementCallbackDelegate MakeDrawElementHandler(ReorderableList list)
        {
            return (rect, index, isActive, isFocused) =>
            {
                rect.y += VERTICAL_ELEMENT_SPACING / 2;
                rect.height -= VERTICAL_ELEMENT_SPACING;
                SerializedProperty serializedProperty = list.serializedProperty;
                EditorGUI.PropertyField(rect, serializedProperty.GetArrayElementAtIndex(index), true);
            };
        }

        static ReorderableList.HeaderCallbackDelegate MakeDrawHeaderHandler(ReorderableList list, string header)
        {
            return (rect) =>
            {
                if (list.draggable)
                {
                    rect.width -= DRAGGABLE_AREA_WIDTH;
                    rect.x += DRAGGABLE_AREA_WIDTH;
                }

                EditorGUI.LabelField(rect, header, EditorStyles.boldLabel);
            };
        }

        static ReorderableList.ElementHeightCallbackDelegate MakeElementHeightCallback(ReorderableList list)
        {
            return (index) =>
            {
                if(index < 0 || index >= list.serializedProperty.arraySize)
                    return 0.0f;
                
                return EditorGUI.GetPropertyHeight(list.serializedProperty.GetArrayElementAtIndex(index), true) + VERTICAL_ELEMENT_SPACING;
            };
        }
    }
}