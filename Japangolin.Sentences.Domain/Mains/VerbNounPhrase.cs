namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;

    public class VerbNounPhrase : INounPhrase
    {
        public ITranslation Noun { get; }
        public ITranslation Verb { get; }

        public VerbNounPhrase(IJapaneseEntry noun, IJapaneseEntry verb, Conjugation conjugation)
        {
            this.Noun = new NounTranslation(noun, conjugation);
            this.Verb = new VerbTranslation(verb, conjugation);
        }

        public List<ITranslation> GetEnglishOrder() => new List<ITranslation> { this.Verb, this.Noun };
        public List<ITranslation> GetJapaneseOrder() => new List<ITranslation> { this.Verb, this.Noun };

        public override string ToString()
        {
            var english = string.Join(" ", this.GetEnglishOrder().Select(translation => translation.English));
            var kana = string.Join(" ", this.GetEnglishOrder().Select(translation => translation.Kana));
            return $"{english} | {kana}";
        }
    }
}
