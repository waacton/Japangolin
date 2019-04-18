namespace Wacton.Japangolin.Conjugation
{
    using System;

    public static class ConjugatorFuncs
    {
        public static readonly Func<WordClass, Conjugator> Dictionary = wordClass => Conjugators.GetDict(wordClass);
        public static readonly Func<WordClass, Conjugator> Stem = wordClass => Conjugators.GetStem(wordClass);
        public static readonly Func<WordClass, Conjugator> Te = wordClass => Conjugators.GetTe(wordClass);

        public static readonly Func<WordClass, Conjugator> PresentAffirmativeLong
            = wordClass => Conjugators.Get(wordClass, Tense.Present, Polarity.Affirmative, Formality.Long);
        public static readonly Func<WordClass, Conjugator> PresentAffirmativeShort
            = wordClass => Conjugators.Get(wordClass, Tense.Present, Polarity.Affirmative, Formality.Short);
        public static readonly Func<WordClass, Conjugator> PresentNegativeLong
            = wordClass => Conjugators.Get(wordClass, Tense.Present, Polarity.Negative, Formality.Long);
        public static readonly Func<WordClass, Conjugator> PresentNegativeShort
            = wordClass => Conjugators.Get(wordClass, Tense.Present, Polarity.Negative, Formality.Short);
        public static readonly Func<WordClass, Conjugator> PastAffirmativeLong
            = wordClass => Conjugators.Get(wordClass, Tense.Past, Polarity.Affirmative, Formality.Long);
        public static readonly Func<WordClass, Conjugator> PastAffirmativeShort
            = wordClass => Conjugators.Get(wordClass, Tense.Past, Polarity.Affirmative, Formality.Short);
        public static readonly Func<WordClass, Conjugator> PastNegativeLong
            = wordClass => Conjugators.Get(wordClass, Tense.Past, Polarity.Negative, Formality.Long);
        public static readonly Func<WordClass, Conjugator> PastNegativeShort
            = wordClass => Conjugators.Get(wordClass, Tense.Past, Polarity.Negative, Formality.Short);

        // standard short form applies except: noun & adj-na in present (replace ～だ ⇒ ～な)
        // https://www.wasabi-jpn.com/japanese-grammar/explanatory-noda/
        public static readonly Func<WordClass, Conjugator> NaPresentAffirmative = wordClass => GetNa(wordClass, Tense.Present, Polarity.Affirmative);
        public static readonly Func<WordClass, Conjugator> NaPastAffirmative = wordClass => GetNa(wordClass, Tense.Past, Polarity.Affirmative);
        public static readonly Func<WordClass, Conjugator> NaPresentNegative = wordClass => GetNa(wordClass, Tense.Present, Polarity.Negative);
        public static readonly Func<WordClass, Conjugator> NaPastNegative = wordClass => GetNa(wordClass, Tense.Past, Polarity.Negative);
        private static Conjugator GetNa(WordClass wordClass, Tense tense, Polarity polarity)
        {
            var conjugator = Conjugators.Get(wordClass, tense, polarity, Formality.Short);

            var function = conjugator.Function;
            var detailedInfo = $"{conjugator.DetailedInfo}（~だ ⇒ ~な）";
            var abstractInfo = $"{conjugator.AbstractInfo}（~だ ⇒ ~な）";

            // replace ～だ ⇒ ～な in noun & adj-na (only exists in present affirmative short)
            if (polarity == Polarity.Affirmative && (wordClass == WordClass.Noun || wordClass == WordClass.AdjectiveNa))
            {
                function = text =>
                {
                    var conjugation = conjugator.Conjugate(text);
                    return conjugation.Remove(conjugation.Length - 1) + 'な';
                };
            }

            return new Conjugator(function, detailedInfo, abstractInfo);
        }

        // standard short form applies except: noun & adj-na in present (remove ～だ)
        public static readonly Func<WordClass, Conjugator> RemoveDaAffirmative = wordClass => GetRemoveDa(wordClass, Polarity.Affirmative);
        public static readonly Func<WordClass, Conjugator> RemoveDaNegative = wordClass => GetRemoveDa(wordClass, Polarity.Negative);
        private static Conjugator GetRemoveDa(WordClass wordClass, Polarity polarity)
        {
            var conjugator = Conjugators.Get(wordClass, Tense.Present, polarity, Formality.Short);

            var function = conjugator.Function;
            var detailedInfo = $"{conjugator.DetailedInfo}（ーだ）";
            var abstractInfo = $"{conjugator.AbstractInfo}（ーだ）";

            // remove ～だ from noun & adj-na (only exists in present affirmative short)
            if (polarity == Polarity.Affirmative && (wordClass == WordClass.Noun || wordClass == WordClass.AdjectiveNa))
            {
                function = text =>
                {
                    var conjugation = conjugator.Conjugate(text);
                    return conjugation.Remove(conjugation.Length - 1);
                };
            }

            return new Conjugator(function, detailedInfo, abstractInfo);
        }

        // verb in present short negative (remove ～い)
        public static readonly Func<WordClass, Conjugator> RemoveI = wordClass => GetRemoveI(wordClass);
        private static Conjugator GetRemoveI(WordClass wordClass)
        {
            if (!WordClasses.Verbs.Contains(wordClass))
            {
                throw new InvalidOperationException();
            }

            var conjugator = Conjugators.Get(wordClass, Tense.Present, Polarity.Negative, Formality.Short);

            var detailedInfo = $"{conjugator.DetailedInfo}（ーい）";
            var abstractInfo = $"{conjugator.AbstractInfo}（ーい）";
            string function(string text)
            {
                var conjugation = conjugator.Conjugate(text);
                return conjugation.Remove(conjugation.Length - 1);
            }

            return new Conjugator(function, detailedInfo, abstractInfo);
        }

        // noun & adj in dictionary form (adj-i add く, adj-na & noun add に)
        public static readonly Func<WordClass, Conjugator> Adverbial = wordClass => GetAdverbial(wordClass);
        private static Conjugator GetAdverbial(WordClass wordClass)
        {
            if (!WordClasses.NounsOrAdjectives.Contains(wordClass))
            {
                throw new InvalidOperationException();
            }

            var conjugator = Conjugators.GetStem(wordClass);

            var detailedInfo = $"{conjugator.DetailedInfo}（＋く／に）";
            var abstractInfo = $"{conjugator.AbstractInfo}（＋く／に）";
            string function(string text)
            {
                var conjugation = conjugator.Conjugate(text);
                return wordClass == WordClass.AdjectiveI
                    ? conjugation + "く"
                    : conjugation + 'に';
            }

            return new Conjugator(function, detailedInfo, abstractInfo);
        }
    }
}
