namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;

    public class VerbNounPhrase : INounPhrase
    {
        public IGolin Noun { get; }
        public IGolin Verb { get; }
        public Conjugation Conjugation { get; }

        public VerbNounPhrase(IJapaneseEntry noun, IJapaneseEntry verb, Conjugation conjugation)
        {
            this.Noun = new Noungolin(noun, conjugation);
            this.Verb = new Verbgolin(verb, conjugation);
            this.Conjugation = conjugation;
        }

        public List<IGolin> GolinEnglish() => new List<IGolin> { this.Verb, this.Noun };
        public List<IGolin> GolinJapanese() => new List<IGolin> { this.Verb, this.Noun };

        public override string ToString()
        {
            var english = string.Join(" ", this.GolinEnglish().Select(translation => translation.EnglishBase));
            var kana = string.Join(" ", this.GolinEnglish().Select(translation => translation.KanaBase));
            return $"{english} | {kana}";
        }
    }
}
