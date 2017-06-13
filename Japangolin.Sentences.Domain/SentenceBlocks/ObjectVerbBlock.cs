namespace Wacton.Japangolin.Sentences.Domain.SentenceBlocks
{
    using System.Collections.Generic;

    using Wacton.Japangolin.Sentences.Domain.Golins;
    using Wacton.Japangolin.Sentences.Domain.NounPhrases;

    public class ObjectVerbBlock : ObjectBlock
    {
        private readonly IGolin verb;

        public override bool HasVerb => true;

        public ObjectVerbBlock(NounPhrase nounPhrase, IGolin verb) : base(nounPhrase)
        {
            this.verb = verb;
        }

        public override List<IGolin> GolinEnglish()
        {
            var golins = new List<IGolin>();
            golins.Add(this.verb);
            golins.Add(this.ObjectPreposition);
            golins.AddRange(this.NounPhrase.GolinEnglish());
            return golins;
        }

        public override List<IGolin> GolinJapanese()
        {
            var golins = new List<IGolin>();
            golins.AddRange(this.NounPhrase.GolinJapanese());
            golins.Add(this.DirectObjectMarker);
            golins.Add(this.verb);
            return golins;
        }
    }
}