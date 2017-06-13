namespace Wacton.Japangolin.Sentences.Domain.NounPhrases
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Sentences.Domain.Extensions;
    using Wacton.Tovarisch.Randomness;

    public static class NounPhraseFactory
    {
        public static NounPhrase Simple(IEnumerable<IJapaneseEntry> japaneseEntries)
        {
            var noun = RandomSelection.SelectOne(japaneseEntries.GetNouns());
            return new SimpleNounPhrase(noun);
        }

        public static NounPhrase Modified(IEnumerable<IJapaneseEntry> japaneseEntries)
        {
            var noun = RandomSelection.SelectOne(japaneseEntries.GetNouns());
            var modifyingNoun = RandomSelection.SelectOne(japaneseEntries.GetNouns());
            return new ModifiedNounPhrase(noun, modifyingNoun);
        }
    }
}
