namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class ModifiedNounPhrase : INounPhrase
    {
        public ITranslation TargetNoun { get; }
        public ITranslation ModifyingNoun { get; }

        public ModifiedNounPhrase(IJapaneseEntry targetNoun, IJapaneseEntry modifyingNoun)
        {
            this.TargetNoun = new Translation(targetNoun);
            this.ModifyingNoun = new Translation(modifyingNoun);
        }

        public List<ITranslation> GetEnglishOrder() => new List<ITranslation> { this.TargetNoun, new EnglishOnlyTranslation("of"), this.ModifyingNoun };
        public List<ITranslation> GetJapaneseOrder() => new List<ITranslation> { this.ModifyingNoun, new JapaneseOnlyTranslation("の"), this.TargetNoun };
    }
}
