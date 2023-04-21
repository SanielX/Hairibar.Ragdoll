using System;

namespace Hairibar.NaughtyExtensions.Editor
{
    internal sealed class InvalidConversionException : Exception
    {
        public InvalidConversionException(string message) : base(message)
        {
        }

        public InvalidConversionException()
        {
        }
    }
}
