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
    }
}
