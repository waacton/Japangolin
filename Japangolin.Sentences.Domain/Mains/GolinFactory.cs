namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using Wacton.Desu.Japanese;

    public static class GolinFactory
    {
        public static IGolin FromConjugatedNoun(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var english = new English(japaneseEntry.GetEnglish());
            var japanese = new Japanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji(), conjugation, ConjugationFunctions.JapaneseNoun);
            return CreateGolin(english, japanese);
        }

        public static IGolin FromConjugatedVerb(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var english = new English(japaneseEntry.GetEnglish(), conjugation, ConjugationFunctions.EnglishVerb); // TODO: not quite this
            var japanese = new Japanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji(), conjugation, ConjugationFunctions.JapaneseVerb);
            return CreateGolin(english, japanese);
        }

        // TODO: conjugation will be needed here (JP)
        public static IGolin FromConjugatedAdjective(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var english = new English(japaneseEntry.GetEnglish());
            var japanese = new Japanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji());
            return CreateGolin(english, japanese);
        }

        public static IGolin FromUnconjugated(IJapaneseEntry japaneseEntry)
        {
            var english = new English(japaneseEntry.GetEnglish());
            var japanese = new Japanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji());
            return CreateGolin(english, japanese);
        }

        public static IGolin TopicPreposition(Conjugation conjugation)
        {
            var english = new English("is", conjugation, ConjugationFunctions.EnglishTopicPrepositions);
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
