namespace Wacton.Japangolin.Sentences.Domain.NounPhrases
{
    using System.Collections.Generic;

    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    public class ModifiedNounPhrase : NounPhrase
    {
        public IGolin ModifyingNoun { get; }
        private IGolin PossessionMarker = GolinFactory.PossessionMarker();

        public ModifiedNounPhrase(IJapaneseEntry noun, IJapaneseEntry modifyingNoun) : base(noun)
        {
            this.ModifyingNoun = GolinFactory.Noun(modifyingNoun);
        }

        public override List<IGolin> GolinEnglish() => new List<IGolin> { this.ModifyingNoun, this.Noun };
        public override List<IGolin> GolinJapanese() => new List<IGolin> { this.ModifyingNoun, this.PossessionMarker, this.Noun };
    }
}
