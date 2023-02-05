namespace Wacton.Japangolin.Core.Words;

using System;
using System.Collections.Generic;
using System.Linq;
using Wacton.Desu.Enums;
using Wacton.Desu.Japanese;
using Wacton.Japangolin.Core.Enums;

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

    public static Word ParseToWord(this IJapaneseEntry japaneseEntry)
    {
        var kana = japaneseEntry.GetKana();
        var kanji = japaneseEntry.GetKanji();
        var english = japaneseEntry.GetEnglish();
        var partOfSpeech = japaneseEntry.GetPartsOfSpeech().First();
        var wordClass = GetWordClass(partOfSpeech);
        return new Word { Kana = kana, Kanji = kanji, English = english, Class = wordClass };
    }

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

    private static WordClass GetWordClass(PartOfSpeech partOfSpeech)
    {
        if (partOfSpeech == null) { throw new InvalidOperationException(); }
        if (POS.Nouns.Contains(partOfSpeech)) { return WordClass.Noun; }
        if (POS.AdjectivesNa.Contains(partOfSpeech)) { return WordClass.AdjectiveNa; }
        if (POS.AdjectivesI.Contains(partOfSpeech)) { return WordClass.AdjectiveI; }
        if (POS.VerbsRu.Contains(partOfSpeech)) { return WordClass.VerbRu; }
        if (POS.VerbsU.Contains(partOfSpeech)) { return WordClass.VerbU; }
        return WordClass.Unknown; // TODO: handle this better?
    }
}