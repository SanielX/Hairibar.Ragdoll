using UnityEngine;

namespace Hairibar.NaughtyExtensions
{
    internal class QuadraticSliderAttribute : PropertyAttribute
    {
        public float Power { get; }
        public float Min   { get; }
        public float Max   { get; }

        public QuadraticSliderAttribute(float min, float max, float power)
        {
            Min = min;
            Max = max;
            Power = power;
        }
    }
}
