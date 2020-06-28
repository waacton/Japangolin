namespace Wacton.Japangolin.Sentences.Domain.NounPhrases
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    public class VerbNounPhrase : NounPhrase
    {
        public IGolin Verb { get; }

        public VerbNounPhrase(IJapaneseEntry noun, IJapaneseEntry verb) : base(noun)
        {
            this.Verb = GolinFactory.Verb(verb);
        }

        public override List<IGolin> GolinEnglish() => new List<IGolin> { this.Verb, this.Noun };
        public override List<IGolin> GolinJapanese() => new List<IGolin> { this.Verb, this.Noun };
    }
}
