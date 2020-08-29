using System;
using System.Collections.Generic;
using System.Linq;
using Wacton.Desu.Japanese;
using Wacton.Tovarisch.CSV;

namespace JLPT_Parser
{
    class Program
    {
        static readonly string inputFile = "../../../JLPTN5.csv";
        static readonly string outputFile = "../../../JLPTN5-output.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("Loading Japanese dictionary...");
            var japaneseDictionary = new JapaneseDictionary();
            var japaneseEntries = japaneseDictionary.GetEntries();

            Console.WriteLine("Looking up JLPT data...");
            var jlptCsv = CsvRead.ReadRows(inputFile);

            var matched = new List<IEnumerable<string>>();
            var unmatched = new List<IEnumerable<string>>();
            foreach(var row in jlptCsv)
            {
                var kanjiText = row[0].Split("/")[0].Trim();
                var readingText = row[1].Split("/")[0].Trim();

                var sequence = GetSequence(japaneseEntries, kanjiText, readingText);
                if (sequence.HasValue)
                {
                    var withSequence = row.ToList();
                    withSequence.Insert(0, sequence.Value.ToString());
                    matched.Add(withSequence);
                }
                else
                {
                    var withSequence = row.ToList();
                    withSequence.Insert(0, string.Empty);
                    unmatched.Add(withSequence);
                }
            }

            Console.WriteLine($"Succeeded: {matched.Count()}");
            Console.WriteLine($"Failed: {unmatched.Count()}");

            var output = matched.Concat(unmatched);
            CsvWrite.WriteRows(output, outputFile);
        }

        static int? GetSequence(IEnumerable<IJapaneseEntry> entries, string kanjiText, string readingText)
        {
            // ideally there will be a unique kanji match
            var matches = GetKanjiMatches(entries, kanjiText);
            if (matches.Count() == 1)
            {
                return matches.Single().Sequence;
            }

            // when multiple kanjis exist, check them to see if there is a unique kana match
            // otherwise resort to checking all entries for kana if there is no kanji match at all
            if (matches.Count() > 1)
            {
                return GetSequenceByReading(matches, readingText);
            }
            else
            {
                return GetSequenceByReading(entries, readingText);
            }

        }


        static int? GetSequenceByReading(IEnumerable<IJapaneseEntry> entries, string readingText)
        {
            // ideally there will be a unique kana match
            var matches = GetReadingMatches(entries, readingText);
            if (matches.Count() == 1)
            {
                return matches.Single().Sequence;
            }

            // if we've resorted to kana matches, but there is no unique match
            // it will need to be resolved by hand
            // - e.g. あなた can mean "you" or "beyond", but only "you" is desired for JLTP N5
            return null;
        }

        static IEnumerable<IJapaneseEntry> GetKanjiMatches(IEnumerable<IJapaneseEntry> entries, string kanjiText)
        {
            return entries.Where(entry => entry.Kanjis.Any(kanji => kanji.Text == kanjiText));
        }

        static IEnumerable<IJapaneseEntry> GetReadingMatches(IEnumerable<IJapaneseEntry> entries, string readingText)
        {
            return entries.Where(entry => entry.Readings.Any(reading => reading.Text == readingText));
        }
    }
}
