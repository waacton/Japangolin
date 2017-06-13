namespace Wacton.Japangolin.Sentences.Domain.NounPhrases
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    public class SimpleNounPhrase : NounPhrase
    {
        public SimpleNounPhrase(IJapaneseEntry noun) : base(noun)
        {
        }

        public override List<IGolin> GolinEnglish() => new List<IGolin> { this.Noun };
        public override List<IGolin> GolinJapanese() => new List<IGolin> { this.Noun };
    }
}
