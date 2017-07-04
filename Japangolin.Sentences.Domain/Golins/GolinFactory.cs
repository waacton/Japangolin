namespace Wacton.Japangolin.Sentences.Domain.Golins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Extensions;

    public static class GolinFactory
    {
        public static IGolin Noun(IJapaneseEntry japaneseEntry) => Noun(japaneseEntry, Conjugation.None);
        public static IGolin Noun(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            return CreateGolin(japaneseEntry, conjugation, ConjugationFunctions.JapaneseNoun, ConjugationInformations.JapaneseNoun);
        }

        public static IGolin Verb(IJapaneseEntry japaneseEntry) => Verb(japaneseEntry, Conjugation.None);
        public static IGolin Verb(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var isVerbIchidan = japaneseEntry.IsAnyPartOfSpeech(PartsOfSpeech.VerbsIchidan);
            var conjugationFunctions = isVerbIchidan ? ConjugationFunctions.JapaneseVerbIchidan : ConjugationFunctions.JapaneseVerbGodan;
            var conjugationInformations = isVerbIchidan ? ConjugationInformations.JapaneseVerbIchidan : ConjugationInformations.JapaneseVerbGodan;
            return CreateGolin(japaneseEntry, conjugation, conjugationFunctions, conjugationInformations);
        }

        public static IGolin Adjective(IJapaneseEntry japaneseEntry) => Adjective(japaneseEntry, Conjugation.None);
        public static IGolin Adjective(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var isAdjectiveI = japaneseEntry.IsPartOfSpeech(PartOfSpeech.AdjectiveI);
            var conjugationFunctions = isAdjectiveI ? ConjugationFunctions.JapaneseAdjectiveI : ConjugationFunctions.JapaneseAdjectiveNa;
            var conjugationInformations = isAdjectiveI ? ConjugationInformations.JapaneseAdjectiveI : ConjugationInformations.JapaneseAdjectiveNa;
            return CreateGolin(japaneseEntry, conjugation, conjugationFunctions, conjugationInformations);
        }

        public static IGolin TopicPreposition(Conjugation conjugation, bool isVerbInSentence)
        {
            var conjugationFunction = isVerbInSentence ? ConjugationFunctions.EnglishTopicPrepositionsWithVerb : ConjugationFunctions.EnglishTopicPrepositionsWithoutVerb;
            var english = new English("is", conjugation, conjugationFunction);
            return CreateGolin(english, null);
        }

        public static IGolin TopicMarker()
        {
            var japanese = new Japanese("は");
            return CreateGolin(null, japanese);
        }

        public static IGolin ObjectPreposition()
        {
            var english = new English("a");
            return CreateGolin(english, null);
        }

        public static IGolin DirectObjectMarker()
        {
            var japanese = new Japanese("を");
            return CreateGolin(null, japanese);
        }

        public static IGolin PossessionMarker()
        {
            var japanese = new Japanese("の");
            return CreateGolin(null, japanese);
        }

        private static IGolin CreateGolin(IJapaneseEntry japaneseEntry, Conjugation conjugation, 
                                          Dictionary<Conjugation, Func<string, string>> conjugationFunctions, 
                                          Dictionary<Conjugation, Func<string>> conjugationInformations)
        {
            var english = new English(japaneseEntry.GetEnglish());
            var japanese = new Japanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji(), conjugation, conjugationFunctions, conjugationInformations);
            var translationInformation = japaneseEntry.GetPartsOfSpeech().Select(partOfSpeech => partOfSpeech.ToString());
            return CreateGolin(english, japanese, translationInformation);
        }

        private static IGolin CreateGolin(English english, Japanese japanese, IEnumerable<string> translationInformation = null)
        {
            return new Golin(english, japanese, translationInformation);
        }
    }
}
