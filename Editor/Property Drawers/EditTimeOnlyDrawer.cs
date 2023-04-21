using UnityEditor;
using UnityEngine;

namespace Hairibar.NaughtyExtensions.Editor
{
    [CustomPropertyDrawer(typeof(EditTimeOnlyAttribute))]
    internal class EditTimeOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.DisabledGroupScope(disabled: Application.isPlaying))
            {
                EditorGUI.PropertyField(rect, property, label, true);
            }
        }
    }
}
