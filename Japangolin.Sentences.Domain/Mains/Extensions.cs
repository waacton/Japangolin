namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Linq;

    using Wacton.Desu.Japanese;

    public static class Extensions
    {
        public static string GetEnglish(this IJapaneseEntry japaneseEntry)
        {
            return japaneseEntry.Senses.First().Glosses.First().Term;
        }

        public static string GetKana(this IJapaneseEntry japaneseEntry)
        {
            return japaneseEntry.Readings.First().Text;
        }

        public static string GetKanji(this IJapaneseEntry japaneseEntry)
        {
            return japaneseEntry.Kanjis.Any() ? japaneseEntry.Kanjis.First().Text : japaneseEntry.GetKana();
        }

        private static readonly string EnglishSpace = " ";
        private static readonly string JapaneseSpace = "　";
        public static string GetNounPhraseToString(this NounPhrase nounPhrase)
        {
            var english = string.Join(EnglishSpace, nounPhrase.GolinEnglish().Select(translation => translation.EnglishBase));
            var kana = string.Join(JapaneseSpace, nounPhrase.GolinJapanese().Select(translation => translation.KanaBase));
            return $"{english} | {kana}";
        }
    }
}
