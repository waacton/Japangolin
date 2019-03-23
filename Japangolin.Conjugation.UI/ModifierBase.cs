namespace Wacton.Japangolin.Conjugation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Tovarisch.Enum;

    public abstract class ModifierBase : Enumeration, IModifier
    {
        private readonly string format;
        private readonly (Func<WordClass, Conjugator> conjugatorByWordClass, List<WordClass> wordClasses)[] conjugations;

        public abstract string Variation { get; }
        public abstract bool IsHighLevel { get; }

        public int RequiredWordDataCount { get; }

        public ModifierBase(string displayName, string format, params (Func<WordClass, Conjugator> conjugatorByWordClass, List<WordClass> wordClasses)[] conjugations)
            : base(displayName)
        {
            this.format = format;
            this.conjugations = conjugations;
            this.RequiredWordDataCount = this.format.Count(character => character.Equals('{')); // naive!

            if (this.conjugations.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }
        }

        public List<List<WordClass>> GetRequiredWordClasses()
        {
            return this.conjugations.Select(conjugation => conjugation.wordClasses).ToList();
        }

        public string Conjugate(params WordData[] wordDatas)
        {
            if (wordDatas.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }

            var conjugatedWords = wordDatas
                .Select((wordData, i) => this.conjugations[i].conjugatorByWordClass(wordData.Class).Conjugate(wordData.Kana))
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
                var conjugatorByWordClass = conjugations[i].conjugatorByWordClass;
                var wordClass = wordDatas[i].Class;
                var conjugator = conjugatorByWordClass(wordClass);
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

        // TODO: split enums into forms / conjugations / grammars (IModifier?)
        // TODO: rename word classes
        // TODO: allow grammars to take different tense / polarity / formality if the grammar allows (as it stands is enough for current practice)
        //       e.g. ～んです works with present, past, affirmative, negative
        protected static readonly List<WordClass> Nouns = new List<WordClass> { WordClass.Noun };
        protected static readonly List<WordClass> Adjectives = new List<WordClass> { WordClass.AdjectiveNa, WordClass.AdjectiveI };
        protected static readonly List<WordClass> Verbs = new List<WordClass> { WordClass.VerbRu, WordClass.VerbU };
        protected static readonly List<WordClass> AdjectivesAndVerbs = Adjectives.Concat(Verbs).ToList();
        protected static readonly List<WordClass> All = Nouns.Concat(Adjectives).Concat(Verbs).ToList();
    }
}
