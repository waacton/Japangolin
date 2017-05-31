namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class SimpleNounPhrase : NounPhrase
    {
        public SimpleNounPhrase(IJapaneseEntry noun) : base(noun)
        {
        }

        public override List<IGolin> GolinEnglish() => new List<IGolin> { this.Noun };
        public override List<IGolin> GolinJapanese() => new List<IGolin> { this.Noun };
    }
}
