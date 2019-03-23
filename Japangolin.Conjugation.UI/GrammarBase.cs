namespace Wacton.Japangolin.Conjugation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Tovarisch.Enum;

    public abstract class GrammarBase : Enumeration
    {
        private readonly string format;
        private readonly WordClassConjugator[] conjugators;

        public abstract string Variation { get; }
        public abstract bool IsHighLevel { get; }

        public int RequiredWordDataCount { get; }

        public GrammarBase(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName)
        {
            this.format = format;
            this.conjugators = conjugators;
            this.RequiredWordDataCount = this.format.Count(character => character.Equals('{')); // naive!

            if (this.conjugators.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }
        }

        public List<WordClass> GetRequiredWordClasses(int wordIndex)
        {
            return this.conjugators[wordIndex].WordClasses;
        }

        public string Conjugate(params WordData[] wordDatas)
        {
            if (wordDatas.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }

            var conjugatedWords = wordDatas
                .Select((wordData, i) => this.conjugators[i].Conjugate(wordData.Kana, wordData.Class))
                .ToArray();

            return string.Format(this.format, conjugatedWords);
        }

        public string Information(params WordData[] wordDatas)
        {
            if (wordDatas.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }

            var information = this.format;
            for (var i = 0; i < this.RequiredWordDataCount; i++)
            {
                var wordClass = wordDatas[i].Class;
                var conjugator = conjugators[i].GetConjugator(wordClass);
                var wordInfo = this.IsHighLevel ? conjugator.AbstractInfo : conjugator.DetailedInfo;
                information = information.Replace("{" + i + "}", "｛" + wordInfo + "｝");
            }

            return information;
        }

        protected static readonly Func<WordClass, Conjugator> Dictionary = wordClass => Conjugators.GetDict(wordClass);
        protected static readonly Func<WordClass, Conjugator> Stem = wordClass => Conjugators.GetStem(wordClass);
        protected static readonly Func<WordClass, Conjugator> Te = wordClass => Conjugators.GetTe(wordClass);

        protected static readonly Func<WordClass, Conjugator> PresentAffirmativeLong
            = wordClass => Conjugators.Get(wordClass, Tense.Present, Polarity.Affirmative, Formality.Long);
        protected static readonly Func<WordClass, Conjugator> PresentAffirmativeShort
            = wordClass => Conjugators.Get(wordClass, Tense.Present, Polarity.Affirmative, Formality.Short);
        protected static readonly Func<WordClass, Conjugator> PresentNegativeLong
            = wordClass => Conjugators.Get(wordClass, Tense.Present, Polarity.Negative, Formality.Long);
        protected static readonly Func<WordClass, Conjugator> PresentNegativeShort
            = wordClass => Conjugators.Get(wordClass, Tense.Present, Polarity.Negative, Formality.Short);
        protected static readonly Func<WordClass, Conjugator> PastAffirmativeLong
            = wordClass => Conjugators.Get(wordClass, Tense.Past, Polarity.Affirmative, Formality.Long);
        protected static readonly Func<WordClass, Conjugator> PastAffirmativeShort
            = wordClass => Conjugators.Get(wordClass, Tense.Past, Polarity.Affirmative, Formality.Short);
        protected static readonly Func<WordClass, Conjugator> PastNegativeLong
            = wordClass => Conjugators.Get(wordClass, Tense.Past, Polarity.Negative, Formality.Long);
        protected static readonly Func<WordClass, Conjugator> PastNegativeShort
            = wordClass => Conjugators.Get(wordClass, Tense.Past, Polarity.Negative, Formality.Short);

        // TODO: this is getting out of hand now...
        // TODO: allow grammars to take different tense / polarity / formality if the grammar allows (as it stands is enough for current practice)
        //       e.g. ～んです works with present, past, affirmative, negative
        protected static readonly Func<WordClass, Conjugator> Na = wordClass => GetNa(wordClass);
        private static Conjugator GetNa(WordClass wordClass)
        {
            var shortForm = PresentAffirmativeShort(wordClass);

            Func<string, string> function = shortForm.Function;
            string information = shortForm.DetailedInfo;

            // note: this change only happens with present affirmative short (～だ　⇒　～な)
            if (wordClass == WordClass.Noun || wordClass == WordClass.AdjectiveNa)
            {
                function = text =>
                {
                    var conjugation = shortForm.Conjugate(text);
                    return conjugation.Remove(conjugation.Length - 1) + 'な';
                };

                information = "＋な";
            }

            return new Conjugator(function, information, "short（な）");
        }

        protected static WordClassConjugator GetConj(List<WordClass> wordClasses, Func<WordClass, Conjugator> conjugator)
        {
            return new WordClassConjugator(wordClasses, conjugator);
        }
    }
}
