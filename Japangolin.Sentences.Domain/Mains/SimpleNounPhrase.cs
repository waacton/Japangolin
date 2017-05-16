namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class SimpleNounPhrase : INounPhrase
    {
        public ITranslation Noun { get; }

        public SimpleNounPhrase(IJapaneseEntry noun)
        {
            this.Noun = new Translation(noun);
        }

        public List<ITranslation> GetEnglishOrder() => new List<ITranslation> { this.Noun };
        public List<ITranslation> GetJapaneseOrder() => new List<ITranslation> { this.Noun };
    }
}
