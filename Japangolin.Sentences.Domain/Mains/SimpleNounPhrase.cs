namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;

    public class SimpleNounPhrase : INounPhrase
    {
        public ITranslation Noun { get; }

        public SimpleNounPhrase(IJapaneseEntry noun, Conjugation conjugation)
        {
            this.Noun = new NounTranslation(noun, conjugation);
        }

        public List<ITranslation> GetEnglishOrder() => new List<ITranslation> { this.Noun };
        public List<ITranslation> GetJapaneseOrder() => new List<ITranslation> { this.Noun };

        public override string ToString()
        {
            var english = string.Join(" ", this.GetEnglishOrder().Select(translation => translation.English));
            var kana = string.Join(" ", this.GetEnglishOrder().Select(translation => translation.Kana));
            return $"{english} | {kana}";
        }
    }
}
