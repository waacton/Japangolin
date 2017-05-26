namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class ModifiedNounPhrase : INounPhrase
    {
        private IGolin PossessionMarker = GolinFactory.PossessionMarker();
        public IGolin TargetNoun { get; }
        public IGolin ModifyingNoun { get; }


        public ModifiedNounPhrase(IJapaneseEntry targetNoun, IJapaneseEntry modifyingNoun, Conjugation conjugation)
        {
            this.TargetNoun = GolinFactory.FromConjugatedNoun(targetNoun, conjugation);
            this.ModifyingNoun = GolinFactory.FromUnconjugated(modifyingNoun);
        }

        public ModifiedNounPhrase(IJapaneseEntry targetNoun, IJapaneseEntry modifyingNoun) : this(targetNoun, modifyingNoun, Conjugation.None)
        {
        }

        public List<IGolin> GolinEnglish() => new List<IGolin> { this.ModifyingNoun, this.TargetNoun };
        public List<IGolin> GolinJapanese() => new List<IGolin> { this.ModifyingNoun, this.PossessionMarker, this.TargetNoun };

        public override string ToString() => this.GetNounPhraseToString();
    }
}
