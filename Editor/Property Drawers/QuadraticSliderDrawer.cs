using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Hairibar.NaughtyExtensions.Editor
{
    [CustomPropertyDrawer(typeof(QuadraticSliderAttribute))]
    internal class QuadraticSliderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            QuadraticSliderAttribute quadraticAttribute = fieldInfo.GetCustomAttribute<QuadraticSliderAttribute>();
            if (quadraticAttribute != null)
            {
                NonLinearSliderDrawer.Draw(rect, property, quadraticAttribute.Min, quadraticAttribute.Max, GetQuadraticFunction(quadraticAttribute.Power), label);
            }
        }

        public static NonLinearSliderDrawer.Function GetQuadraticFunction(float power)
        {
            return new NonLinearSliderDrawer.Function
            {
                function = (x) => Mathf.Pow(x, power),
                backwardsFunction = (x) => Mathf.Pow(x, 1f / power)
            };
        }
    }
}
