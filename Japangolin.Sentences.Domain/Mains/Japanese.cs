namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;

    public class Japanese
    {
        public string KanaBase { get; }
        public string KanjiBase { get; }
        public Conjugation Conjugation { get; }

        private readonly Dictionary<Conjugation, Func<string, string>> conjugationFunctions;
        public string KanaConjugated => this.conjugationFunctions[this.Conjugation].Invoke(this.KanaBase);
        public string KanjiConjugated => this.conjugationFunctions[this.Conjugation].Invoke(this.KanjiBase);

        public Japanese(string kanaBase, string kanjiBase, Conjugation conjugation, Dictionary<Conjugation, Func<string, string>> conjugationFunctions)
        {
            this.KanaBase = kanaBase;
            this.KanjiBase = kanjiBase;
            this.Conjugation = conjugation;
            this.conjugationFunctions = conjugationFunctions;
        }

        public Japanese(string kanaBase, string kanjiBase) : this(kanaBase, kanjiBase, Conjugation.None, ConjugationFunctions.Defaults)
        {
        }

        public Japanese(string japaneseBase) : this(japaneseBase, japaneseBase)
        {
        }
    }
}
