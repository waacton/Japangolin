namespace Wacton.Japangolin.Conjugation.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;

    public static class JapaneseEntryExtensions
    {
        /* 
         * NOTE: if using senses other than the main, the parts-of-speech are inherited from the main unless otherwise specified
         * the JMdict DTD contains this description:
         * 
         *  "In general where there are multiple senses in an entry, 
         *   the part-of-speech of an earlier sense will apply to later senses 
         *   unless there is a new part-of-speech indicated."
         *   
         */
        private static ISense GetMainSense(IJapaneseEntry japaneseEntry) => japaneseEntry.Senses.First();
        private static IReading GetMainReading(IJapaneseEntry japaneseEntry) => japaneseEntry.Readings.First();
        private static IKanji GetMainKanji(IJapaneseEntry japaneseEntry) => japaneseEntry.Kanjis.FirstOrDefault();

        public static string GetKana(this IJapaneseEntry japaneseEntry)
        {
            return GetMainReading(japaneseEntry).Text;
        }

        public static string GetKanji(this IJapaneseEntry japaneseEntry)
        {
            var mainKanji = GetMainKanji(japaneseEntry);
            return mainKanji != null ? mainKanji.Text : japaneseEntry.GetKana();
        }

        public static string GetEnglish(this IJapaneseEntry japaneseEntry)
        {
            return GetMainSense(japaneseEntry).Glosses.First().Term;
        }

        public static IEnumerable<PartOfSpeech> GetPartsOfSpeech(this IJapaneseEntry japaneseEntry)
        {
            return GetMainSense(japaneseEntry).PartsOfSpeech;
        }

        public static bool IsPartOfSpeech(this IJapaneseEntry japaneseEntry, PartOfSpeech partOfSpeech)
        {
            return GetMainSense(japaneseEntry).PartsOfSpeech.Contains(partOfSpeech);
        }

        public static bool IsAnyPartOfSpeech(this IJapaneseEntry japaneseEntry, IEnumerable<PartOfSpeech> partsOfSpeech)
        {
            return GetMainSense(japaneseEntry).PartsOfSpeech.Intersect(partsOfSpeech).Any();
        }

        private static List<IJapaneseEntry> nouns;
        public static List<IJapaneseEntry> GetNouns(this IEnumerable<IJapaneseEntry> japaneseEntries)
        {
            if (nouns == null)
            {
                nouns = japaneseEntries.Where(entry => entry.IsAnyPartOfSpeech(PartsOfSpeech.AllNouns)).ToList();
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
    }
}
