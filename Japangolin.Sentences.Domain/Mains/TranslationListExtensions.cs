namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    public static class TranslationListExtensions
    {
        public static string ToEnglish(this IEnumerable<ITranslation> translations, string delimiter)
        {
            return string.Join(delimiter, translations.Select(translation => translation.English));
        }

        public static string ToKana(this IEnumerable<ITranslation> translations, string delimiter)
        {
            return string.Join(delimiter, translations.Select(translation => translation.Kana));
        }

        public static string ToKanji(this IEnumerable<ITranslation> translations, string delimiter)
        {
            return string.Join(delimiter, translations.Select(translation => translation.Kana));
        }
    }
}
