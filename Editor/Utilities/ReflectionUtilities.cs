namespace Hairibar.NaughtyExtensions.Editor
{
    internal static class ReflectionUtilities
    {
        public static string GetAutoBackingFieldName(string propertyName)
        {
            return $"<{propertyName}>k__BackingField";
        }

        public static T[] GetEnumValues<T>() where T : System.Enum
        {
            return (T[]) System.Enum.GetValues(typeof(T));
        }
    }
}