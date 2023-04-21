namespace Hairibar.NaughtyExtensions.Editor
{
    internal static class StringUtilities
    {
        public static string EnsureNotStartWith(this string value, string start)
        {
            if (value.StartsWith(start)) return value.Substring(start.Length);
            else return value;
        }

        public static string EnsureNotEndsWith(this string value, string end)
        {
            if (value.EndsWith(end)) return value.Substring(0, value.Length - end.Length);
            else return value;
        }
    }
}
