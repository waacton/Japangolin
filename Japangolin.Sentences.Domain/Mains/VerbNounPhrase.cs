namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class VerbNounPhrase : INounPhrase
    {
        public IGolin Noun { get; }
        public IGolin Verb { get; }

        public VerbNounPhrase(IJapaneseEntry noun, IJapaneseEntry verb, Conjugation conjugation)
        {
            this.Noun = GolinFactory.FromConjugatedNoun(noun, conjugation);
            this.Verb = GolinFactory.FromUnconjugated(verb);
        }

        public List<IGolin> GolinEnglish() => new List<IGolin> { this.Verb, this.Noun };
        public List<IGolin> GolinJapanese() => new List<IGolin> { this.Verb, this.Noun };

        public override string ToString() => this.GetNounPhraseToString();
    }
}
