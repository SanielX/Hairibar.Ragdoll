
using UnityEngine;

namespace Hairibar.NaughtyExtensions
{
    internal class InlineAttribute : PropertyAttribute
    {
        public bool ShowHeaderAndBox { get; private set; } = true;

        public InlineAttribute() { }

        public InlineAttribute(bool showHeaderAndBox)
        {
            ShowHeaderAndBox = showHeaderAndBox;
        }
    }
}
