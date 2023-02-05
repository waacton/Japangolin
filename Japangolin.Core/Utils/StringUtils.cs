namespace Wacton.Japangolin.Core.Utils;

using System.Text.RegularExpressions;

public static class StringUtils
{
    private static readonly Regex PascalCaseRegex = new(@"(?!^)(?=[A-Z])");

    // C# regex can't handle > 4-digit codepoints, or their surrogate forms
    // e.g. \u2E80-\u3134F is invalid and \u2E80-\uD884\uDF4F is misinterpreted
    private static readonly Regex NonLatinRegex = new(@"[^\u0000-\u2E80]"); 

    public static string PascalCase(string text, string separator)
    {
        return PascalCaseRegex.Replace(text, separator);
    }

    public static bool ContainsNonLatinCharacters(string text)
    {
        return text != null && NonLatinRegex.IsMatch(text);
    }
}