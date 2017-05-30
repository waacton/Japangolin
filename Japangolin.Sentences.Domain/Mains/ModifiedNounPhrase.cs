namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;

    public class ModifiedNounPhrase : NounPhrase
    {
        public IGolin ModifyingNoun { get; }
        private IGolin PossessionMarker = GolinFactory.PossessionMarker();

        public ModifiedNounPhrase(IJapaneseEntry noun, IJapaneseEntry modifyingNoun, Conjugation conjugation) : base(noun, conjugation)
        {
            this.ModifyingNoun = GolinFactory.Noun(modifyingNoun);
        }

        public ModifiedNounPhrase(IJapaneseEntry targetNoun, IJapaneseEntry modifyingNoun) : this(targetNoun, modifyingNoun, Conjugation.None)
        {
        }

        public override List<IGolin> GolinEnglish() => new List<IGolin> { this.ModifyingNoun, this.Noun };
        public override List<IGolin> GolinJapanese() => new List<IGolin> { this.ModifyingNoun, this.PossessionMarker, this.Noun };
    }
}
