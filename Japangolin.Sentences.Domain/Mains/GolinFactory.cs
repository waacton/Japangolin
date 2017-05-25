namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using Wacton.Desu.Japanese;

    public static class GolinFactory
    {
        public static IGolin FromConjugatedNoun(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var english = new UnconjugatedEnglish(japaneseEntry.GetEnglish());
            var japanese = new NounJapanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji(), conjugation);
            return CreateGolin(english, japanese);
        }

        // TODO: conjugation will be needed here (JP + ENG)
        public static IGolin FromConjugatedVerb(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var english = new UnconjugatedEnglish(japaneseEntry.GetEnglish());
            var japanese = new UnconjugatedJapanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji());
            return CreateGolin(english, japanese);
        }

        // TODO: conjugation will be needed here (JP)
        public static IGolin FromConjugatedAdjective(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            var english = new UnconjugatedEnglish(japaneseEntry.GetEnglish());
            var japanese = new UnconjugatedJapanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji());
            return CreateGolin(english, japanese);
        }

        public static IGolin FromUnconjugated(IJapaneseEntry japaneseEntry)
        {
            var english = new UnconjugatedEnglish(japaneseEntry.GetEnglish());
            var japanese = new UnconjugatedJapanese(japaneseEntry.GetKana(), japaneseEntry.GetKanji());
            return CreateGolin(english, japanese);
        }

        public static IGolin CreateTopic(Conjugation conjugation)
        {
            var english = new TopicEnglish(conjugation);
            var japanese = new TopicJapanese();
            return CreateGolin(english, japanese, false);
        }

        public static IGolin CreateObject()
        {
            var english = new UnconjugatedEnglish("a");
            return CreateGolin(english, null, false);
        }

        public static IGolin CreatePossession()
        {
            var japanese = new UnconjugatedJapanese("の");
            return CreateGolin(null, japanese, false);
        }

        private static IGolin CreateGolin(English english, Japanese japanese, bool isTranslatable = true)
        {
            return new Golin(english, japanese, isTranslatable);
        }
    }
}
