using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Hairibar.NaughtyExtensions
{
    [CustomPropertyDrawer(typeof(InlineAttribute))]
    internal class InlineDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (!property.hasChildren)
            {
                EditorGUILayout.PropertyField(property);
                EditorGUILayout.HelpBox("Can't use [Inline] on a property with no children.", MessageType.Warning);
                return;
            }
            
            InlineAttribute attribute = fieldInfo.GetCustomAttribute<InlineAttribute>();

            if (attribute.ShowHeaderAndBox)
            {
                rect.position += new Vector2(0, EditorGUIUtility.singleLineHeight * 0.2f);

                EditorGUI.LabelField(rect, label, EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical("box");
            }

            //Move to first child
            SerializedProperty child = property.Copy();
            child.Next(true);

            SerializedProperty endProperty = property.GetEndProperty(true);

            bool enterChildren;
            if (attribute.ShowHeaderAndBox)
            {
                enterChildren = EditorGUILayout.PropertyField(child);
            }
            else
            {
                enterChildren = EditorGUI.PropertyField(rect, child);
            }

            while (child.Next(enterChildren) && !SerializedProperty.EqualContents(child, endProperty))
            {
                enterChildren = EditorGUILayout.PropertyField(child);
            }

            if (attribute.ShowHeaderAndBox)
            {
                EditorGUILayout.EndVertical();
            }
        }
    }
}
