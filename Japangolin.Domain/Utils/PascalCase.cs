using System.Text.RegularExpressions;

namespace Wacton.Japangolin.Domain.Utils
{
    public static class PascalCase
    {
        private static readonly Regex pascalCaseRegex = new Regex(@"(?!^)(?=[A-Z])");

        public static string InsertSeparator(string text, string separator)
        {
            return pascalCaseRegex.Replace(text, separator);
        }
    }
}
