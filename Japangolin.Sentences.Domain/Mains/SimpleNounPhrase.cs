namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class SimpleNounPhrase : INounPhrase
    {
        public IGolin Noun { get; }

        public SimpleNounPhrase(IJapaneseEntry noun, Conjugation conjugation)
        {
            this.Noun = GolinFactory.FromConjugatedNoun(noun, conjugation);
        }

        public SimpleNounPhrase(IJapaneseEntry noun) : this(noun, Conjugation.None)
        {
        }

        public List<IGolin> GolinEnglish() => new List<IGolin> { this.Noun };
        public List<IGolin> GolinJapanese() => new List<IGolin> { this.Noun };

        public override string ToString() => this.GetNounPhraseToString();
    }
}
