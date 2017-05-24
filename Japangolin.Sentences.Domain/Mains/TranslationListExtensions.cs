namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    public static class TranslationListExtensions
    {
        public static string ToEnglish(this IEnumerable<IGolin> translations, string delimiter)
        {
            return string.Join(delimiter, translations.Select(translation => translation.EnglishBase));
        }

        public static string ToKana(this IEnumerable<IGolin> translations, string delimiter)
        {
            return string.Join(delimiter, translations.Select(translation => translation.KanaBase));
        }

        public static string ToKanji(this IEnumerable<IGolin> translations, string delimiter)
        {
            return string.Join(delimiter, translations.Select(translation => translation.KanaBase));
        }
    }
}
