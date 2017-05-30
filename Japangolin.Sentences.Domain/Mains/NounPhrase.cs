﻿namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public abstract class NounPhrase
    {
        private readonly IJapaneseEntry noun;
        private Conjugation conjugation;

        public IGolin Noun => GolinFactory.Noun(this.noun, this.conjugation);

        protected NounPhrase(IJapaneseEntry noun, Conjugation conjugation)
        {
            this.noun = noun;
            this.conjugation = conjugation;
        }

        protected NounPhrase(IJapaneseEntry noun) : this(noun, Conjugation.None)
        {
        }

        public void SetConjugation(Conjugation newConjugation)
        {
            this.conjugation = newConjugation;
        }

        public abstract List<IGolin> GolinEnglish();
        public abstract List<IGolin> GolinJapanese();

        public override string ToString() => this.GetNounPhraseToString();
    }
}
