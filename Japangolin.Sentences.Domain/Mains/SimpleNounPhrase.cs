namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;

    public class SimpleNounPhrase : INounPhrase
    {
        public IGolin Noun { get; }
        public Conjugation Conjugation { get; }

        public SimpleNounPhrase(IJapaneseEntry noun, Conjugation conjugation)
        {
            this.Noun = new Noungolin(noun, conjugation);
            this.Conjugation = conjugation;
        }

        public List<IGolin> GolinEnglish() => new List<IGolin> { this.Noun };
        public List<IGolin> GolinJapanese() => new List<IGolin> { this.Noun };

        public override string ToString()
        {
            var english = string.Join(" ", this.GolinEnglish().Select(translation => translation.EnglishBase));
            var kana = string.Join(" ", this.GolinEnglish().Select(translation => translation.KanaBase));
            return $"{english} | {kana}";
        }
    }
}
