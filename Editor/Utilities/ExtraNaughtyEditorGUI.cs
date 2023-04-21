using UnityEditor;
using UnityEngine;

namespace Hairibar.NaughtyExtensions.Editor
{
    internal static class ExtraNaughtyEditorGUI
    {
        public static void Header(Rect rect, string text)
        {
            EditorGUI.LabelField(rect, text, EditorStyles.boldLabel);
        }
    }
}
