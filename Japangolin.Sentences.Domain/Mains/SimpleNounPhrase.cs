namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class SimpleNounPhrase : NounPhrase
    {
        public SimpleNounPhrase(IJapaneseEntry noun, Conjugation conjugation) : base(noun, conjugation)
        {
        }

        public SimpleNounPhrase(IJapaneseEntry noun) : this(noun, Conjugation.None)
        {
        }

        public override List<IGolin> GolinEnglish() => new List<IGolin> { this.Noun };
        public override List<IGolin> GolinJapanese() => new List<IGolin> { this.Noun };
    }
}
