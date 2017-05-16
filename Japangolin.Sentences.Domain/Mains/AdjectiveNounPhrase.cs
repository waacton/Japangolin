namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class AdjectiveNounPhrase : INounPhrase
    {
        public ITranslation Noun { get; }
        public ITranslation Adjective { get; }

        public AdjectiveNounPhrase(IJapaneseEntry noun, IJapaneseEntry adjective)
        {
            this.Noun = new Translation(noun);
            this.Adjective = new Translation(adjective);
        }

        public List<ITranslation> GetEnglishOrder() => new List<ITranslation> { this.Adjective, this.Noun };
        public List<ITranslation> GetJapaneseOrder() => new List<ITranslation> { this.Adjective, this.Noun };
    }
}
