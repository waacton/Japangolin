namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class VerbNounPhrase : NounPhrase
    {
        public IGolin Verb { get; }

        public VerbNounPhrase(IJapaneseEntry noun, IJapaneseEntry verb, Conjugation conjugation) : base(noun, conjugation)
        {
            this.Verb = GolinFactory.Verb(verb);
        }

        public override List<IGolin> GolinEnglish() => new List<IGolin> { this.Verb, this.Noun };
        public override List<IGolin> GolinJapanese() => new List<IGolin> { this.Verb, this.Noun };
    }
}
