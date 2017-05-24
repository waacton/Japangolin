namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;

    public class AdjectiveNounPhrase : INounPhrase
    {
        public IGolin Noun { get; }
        public IGolin Adjective { get; }
        public Conjugation Conjugation { get; }

        public AdjectiveNounPhrase(IJapaneseEntry noun, IJapaneseEntry adjective, Conjugation conjugation)
        {
            this.Noun = new Noungolin(noun, conjugation);
            this.Adjective = new Adjectivegolin(adjective, conjugation);
            this.Conjugation = conjugation;
        }

        public List<IGolin> GolinEnglish() => new List<IGolin> { this.Adjective, this.Noun };
        public List<IGolin> GolinJapanese() => new List<IGolin> { this.Adjective, this.Noun };

        public override string ToString()
        {
            var english = string.Join(" ", this.GolinEnglish().Select(translation => translation.EnglishBase));
            var kana = string.Join(" ", this.GolinEnglish().Select(translation => translation.KanaBase));
            return $"{english} | {kana}";
        }
    }
}
