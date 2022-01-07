namespace Wacton.Japangolin.Domain.Utils
{
    using System.Text.RegularExpressions;

    public static class PascalCase
    {
        private static readonly Regex pascalCaseRegex = new Regex(@"(?!^)(?=[A-Z])");

        public static string InsertSeparator(string text, string separator)
        {
            return pascalCaseRegex.Replace(text, separator);
        }
    }
}
