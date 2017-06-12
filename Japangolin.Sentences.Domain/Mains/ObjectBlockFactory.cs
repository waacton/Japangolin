namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Randomness;

    public static class ObjectBlockFactory
    {
        public static ObjectNounBlock Noun(IEnumerable<IJapaneseEntry> japaneseEntries, Conjugation conjugation, NounPhraseType nounPhraseType)
        {
            var nounPhrase = CreateNounPhrase(nounPhraseType, japaneseEntries);
            nounPhrase.SetConjugation(conjugation);
            return new ObjectNounBlock(nounPhrase);
        }

        public static ObjectVerbBlock Verb(IEnumerable<IJapaneseEntry> japaneseEntries, Conjugation conjugation, NounPhraseType nounPhraseType)
        {
            var nounPhrase = CreateNounPhrase(nounPhraseType, japaneseEntries);
            var verb = RandomSelection.SelectOne(japaneseEntries.GetVerbs());
            var verbGolin = GolinFactory.Verb(verb, conjugation);
            return new ObjectVerbBlock(nounPhrase, verbGolin);
        }

        public static ObjectAdjectiveBlock Adjective(IEnumerable<IJapaneseEntry> japaneseEntries, Conjugation conjugation)
        {
            var adjective = RandomSelection.SelectOne(japaneseEntries.GetAdjectives());
            var adjectiveGolin = GolinFactory.Adjective(adjective, conjugation);
            return new ObjectAdjectiveBlock(adjectiveGolin);
        }

        private static NounPhrase CreateNounPhrase(NounPhraseType nounPhraseType, IEnumerable<IJapaneseEntry> japaneseEntries)
        {
            switch (nounPhraseType)
            {
                case NounPhraseType.Simple:
                    return NounPhraseFactory.Simple(japaneseEntries);
                case NounPhraseType.Modified:
                    return NounPhraseFactory.Modified(japaneseEntries);
                default:
                    throw new ArgumentOutOfRangeException($"Noun phrase type {nounPhraseType} is not handled");
            }
        }
    }
}
