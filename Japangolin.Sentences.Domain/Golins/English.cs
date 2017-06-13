namespace Wacton.Japangolin.Sentences.Domain.Golins
{
    using System;
    using System.Collections.Generic;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;

    public class English
    {
        public string EnglishBase { get; }
        public Conjugation Conjugation { get; }

        private readonly Dictionary<Conjugation, Func<string, string>> conjugationFunctions;
        public string EnglishConjugated => this.conjugationFunctions[this.Conjugation].Invoke(this.EnglishBase);

        public English(string englishBase, Conjugation conjugation, Dictionary<Conjugation, Func<string, string>> conjugationFunctions)
        {
            this.EnglishBase = englishBase;
            this.Conjugation = conjugation;
            this.conjugationFunctions = conjugationFunctions;
        }

        public English(string englishBase) : this(englishBase, Conjugation.None, ConjugationFunctions.Defaults)
        {
        }
    }
}
