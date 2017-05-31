namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class AdjectiveNounPhrase : NounPhrase
    {
        public IGolin Adjective { get; }

        public AdjectiveNounPhrase(IJapaneseEntry noun, IJapaneseEntry adjective) : base(noun)
        {
            this.Adjective = GolinFactory.Adjective(adjective);
        }

        public override List<IGolin> GolinEnglish() => new List<IGolin> { this.Adjective, this.Noun };
        public override List<IGolin> GolinJapanese() => new List<IGolin> { this.Adjective, this.Noun };
    }
}
