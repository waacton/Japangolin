namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class AdjectiveNounPhrase : INounPhrase
    {
        public IGolin Noun { get; }
        public IGolin Adjective { get; }

        public AdjectiveNounPhrase(IJapaneseEntry noun, IJapaneseEntry adjective, Conjugation conjugation)
        {
            this.Noun = GolinFactory.FromConjugatedNoun(noun, conjugation);
            this.Adjective = GolinFactory.FromUnconjugated(adjective);
        }

        public List<IGolin> GolinEnglish() => new List<IGolin> { this.Adjective, this.Noun };
        public List<IGolin> GolinJapanese() => new List<IGolin> { this.Adjective, this.Noun };

        public override string ToString() => this.GetNounPhraseToString();
    }
}
