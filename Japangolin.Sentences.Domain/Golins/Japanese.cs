﻿namespace Wacton.Japangolin.Sentences.Domain.Golins
{
    using System;
    using System.Collections.Generic;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;

    public class Japanese
    {
        public string KanaBase { get; }
        public string KanjiBase { get; }
        public Conjugation Conjugation { get; }

        private readonly Dictionary<Conjugation, Func<string, string>> conjugationFunctions;
        public string KanaConjugated => this.conjugationFunctions[this.Conjugation].Invoke(this.KanaBase);
        public string KanjiConjugated => this.conjugationFunctions[this.Conjugation].Invoke(this.KanjiBase);

        private readonly Dictionary<Conjugation, Func<string>> conjugationInformations;
        public string ConjugationInformation => this.conjugationInformations[this.Conjugation].Invoke();

        public Japanese(
            string kanaBase,
            string kanjiBase,
            Conjugation conjugation,
            Dictionary<Conjugation, Func<string, string>> conjugationFunctions,
            Dictionary<Conjugation, Func<string>> conjugationInformations)
        {
            this.KanaBase = kanaBase;
            this.KanjiBase = kanjiBase;
            this.Conjugation = conjugation;
            this.conjugationFunctions = conjugationFunctions;
            this.conjugationInformations = conjugationInformations;
        }

        public Japanese(string kanaBase, string kanjiBase) : this(kanaBase, kanjiBase, Conjugation.None, ConjugationFunctions.Defaults, ConjugationInformations.Defaults)
        {
        }

        public Japanese(string japaneseBase) : this(japaneseBase, japaneseBase)
        {
        }
    }
}
