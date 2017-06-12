namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;

    public static class GolinFactory
    {
        public static IGolin Noun(IJapaneseEntry japaneseEntry) => Noun(japaneseEntry, Conjugation.None);
        public static IGolin Noun(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var english = new English(japaneseEntry.GetEnglish());
            var japanese = new Japanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji(), conjugation, ConjugationFunctions.JapaneseNoun);
            return CreateGolin(english, japanese);
        }

        public static IGolin Verb(IJapaneseEntry japaneseEntry) => Verb(japaneseEntry, Conjugation.None);
        public static IGolin Verb(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var isVerbIchidan = japaneseEntry.IsAnyPartOfSpeech(PartsOfSpeech.VerbsIchidan);
            var conjugationFunction = isVerbIchidan ? ConjugationFunctions.JapaneseVerbIchidan : ConjugationFunctions.JapaneseVerbGodan;
            var english = new English(japaneseEntry.GetEnglish());
            var japanese = new Japanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji(), conjugation, conjugationFunction);
            return CreateGolin(english, japanese);
        }

        public static IGolin Adjective(IJapaneseEntry japaneseEntry) => Adjective(japaneseEntry, Conjugation.None);
        public static IGolin Adjective(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var isAdjectiveI = japaneseEntry.IsPartOfSpeech(PartOfSpeech.AdjectiveI);
            var conjugationFunction = isAdjectiveI ? ConjugationFunctions.JapaneseAdjectiveI : ConjugationFunctions.JapaneseAdjectiveNa;
            var english = new English(japaneseEntry.GetEnglish());
            var japanese = new Japanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji(), conjugation, conjugationFunction);
            return CreateGolin(english, japanese);
        }

        public static IGolin TopicPreposition(Conjugation conjugation, bool isVerbInSentence)
        {
            var conjugationFunction = isVerbInSentence ? ConjugationFunctions.EnglishTopicPrepositionsWithVerb : ConjugationFunctions.EnglishTopicPrepositionsWithoutVerb;
            var english = new English("is", conjugation, conjugationFunction);
            return CreateGolin(english, null, false);
        }

        public static IGolin TopicMarker()
        {
            var japanese = new Japanese("は");
            return CreateGolin(null, japanese, false);
        }

        public static IGolin ObjectPreposition()
        {
            var english = new English("a");
            return CreateGolin(english, null, false);
        }

        public static IGolin DirectObjectMarker()
        {
            var japanese = new Japanese("を");
            return CreateGolin(null, japanese, false);
        }

        public static IGolin PossessionMarker()
        {
            var japanese = new Japanese("の");
            return CreateGolin(null, japanese, false);
        }

        private static IGolin CreateGolin(English english, Japanese japanese, bool isTranslatable = true)
        {
            return new Golin(english, japanese, isTranslatable);
        }
    }
}
