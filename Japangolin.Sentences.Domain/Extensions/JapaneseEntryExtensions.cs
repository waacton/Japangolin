namespace Wacton.Japangolin.Sentences.Domain.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;

    public static class JapaneseEntryExtensions
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

        private static List<IJapaneseEntry> nouns;
        public static List<IJapaneseEntry> GetNouns(this IEnumerable<IJapaneseEntry> japaneseEntries)
        {
            if (nouns == null)
            {
                nouns = japaneseEntries.Where(entry => entry.IsPartOfSpeech(PartOfSpeech.NounCommon)).ToList();
            }

            return nouns;
        }

        private static List<IJapaneseEntry> verbs;
        public static List<IJapaneseEntry> GetVerbs(this IEnumerable<IJapaneseEntry> japaneseEntries)
        {
            if (verbs == null)
            {
                verbs = japaneseEntries.Where(entry => entry.IsAnyPartOfSpeech(PartsOfSpeech.AllVerbs)).ToList();
            }

            return verbs;
        }

        private static List<IJapaneseEntry> adjectives;
        public static List<IJapaneseEntry> GetAdjectives(this IEnumerable<IJapaneseEntry> japaneseEntries)
        {
            if (adjectives == null)
            {
                adjectives = japaneseEntries.Where(entry => entry.IsAnyPartOfSpeech(PartsOfSpeech.AllAdjectives)).ToList();
            }

            return adjectives;
        }

        public static bool IsPartOfSpeech(this IJapaneseEntry entry, PartOfSpeech partOfSpeech)
        {
            return entry.Senses.Any(sense => sense.PartsOfSpeech.Contains(partOfSpeech));
        }

        public static bool IsAnyPartOfSpeech(this IJapaneseEntry entry, IEnumerable<PartOfSpeech> partsOfSpeech)
        {
            return entry.Senses.Any(sense => sense.PartsOfSpeech.Intersect(partsOfSpeech).Any());
        }
    }
}
