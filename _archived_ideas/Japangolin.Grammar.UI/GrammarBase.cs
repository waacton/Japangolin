namespace Wacton.Japangolin.Grammar
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

        public List<string> Conjugate(params WordData[] wordDatas)
        {
            if (wordDatas.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }

            var conjugations = new List<List<string>>();
            for (var i = 0; i < wordDatas.Length; i++)
            {
                var wordData = wordDatas[i];
                var conjugator = this.conjugators[i];
                var kanaConjugation = wordData.ConjugateKana(conjugator);
                var kanjiConjugation = wordData.ConjugateKanji(conjugator);
                conjugations.Add(new List<string> { kanaConjugation, kanjiConjugation });
            }

            var textVariations = this.GetTextVariations(conjugations);
            var result = textVariations.Select(words => string.Format(this.format, words.ToArray()));
            return result.ToList();
        }

        public string ConjugateAllKana(params WordData[] wordDatas)
        {
            if (wordDatas.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }

            var conjugations = wordDatas
                .Select((wordData, i) => wordData.ConjugateKana(this.conjugators[i]))
                .ToArray();

            return string.Format(this.format, conjugations);
        }

        public string ConjugateAllKanji(params WordData[] wordDatas)
        {
            if (wordDatas.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }

            var conjugations = wordDatas
                .Select((wordData, i) => wordData.ConjugateKanji(this.conjugators[i]))
                .ToArray();

            return string.Format(this.format, conjugations);
        }

        private List<List<string>> GetTextVariations(List<List<string>> conjugations)
        {
            var current = conjugations[0];
            var next = conjugations.Skip(1).ToList();

            var results = new List<List<string>>();
            foreach(var item in current)
            {
                if (!next.Any())
                {
                    results.Add(new List<string> { item });
                    continue;
                }

                var itemLists = this.GetTextVariations(next);
                foreach(var itemList in itemLists)
                {
                    var result = new List<string>() { item };
                    result.AddRange(itemList);
                    results.Add(result);
                }
            }

            return results;
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
