namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;

    public class ModifiedNounPhrase : INounPhrase
    {
        public IGolin TargetNoun { get; }
        public IGolin ModifyingNoun { get; }
        public Conjugation Conjugation { get; }

        public ModifiedNounPhrase(IJapaneseEntry targetNoun, IJapaneseEntry modifyingNoun, Conjugation conjugation)
        {
            this.TargetNoun = new Noungolin(targetNoun, conjugation);
            this.ModifyingNoun = new Noungolin(modifyingNoun, conjugation);
            this.Conjugation = conjugation;
        }

        public List<IGolin> GolinEnglish() => new List<IGolin> { this.ModifyingNoun, this.TargetNoun };
        public List<IGolin> GolinJapanese() => new List<IGolin> { this.ModifyingNoun, new Possessiongolin(this.Conjugation), this.TargetNoun };

        public override string ToString()
        {
            var english = string.Join(" ", this.GolinEnglish().Select(translation => translation.EnglishBase));
            var kana = string.Join(" ", this.GolinEnglish().Select(translation => translation.KanaBase));
            return $"{english} | {kana}";
        }
    }
}
