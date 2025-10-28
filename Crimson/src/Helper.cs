namespace Crimson;

internal static class Helper
{
    public static bool TryParseString(string source, out string result)
    {
        if (source[0] != '"' || source[^1] != '"')
        {
            result = null;
            return false;
        }
        result = source.Trim('"');
        return true;
    }
}