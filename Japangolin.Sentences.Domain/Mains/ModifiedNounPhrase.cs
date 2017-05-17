namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;

    public class ModifiedNounPhrase : INounPhrase
    {
        public ITranslation TargetNoun { get; }
        public ITranslation ModifyingNoun { get; }

        public ModifiedNounPhrase(IJapaneseEntry targetNoun, IJapaneseEntry modifyingNoun)
        {
            this.TargetNoun = new NounTranslation(targetNoun);
            this.ModifyingNoun = new NounTranslation(modifyingNoun);
        }

        public List<ITranslation> GetEnglishOrder() => new List<ITranslation> { this.ModifyingNoun, this.TargetNoun };
        public List<ITranslation> GetJapaneseOrder() => new List<ITranslation> { this.ModifyingNoun, new JapaneseOnlyTranslation("の"), this.TargetNoun };

        public override string ToString()
        {
            var english = string.Join(" ", this.GetEnglishOrder().Select(translation => translation.English));
            var kana = string.Join(" ", this.GetEnglishOrder().Select(translation => translation.Kana));
            return $"{english} | {kana}";
        }
    }
}
