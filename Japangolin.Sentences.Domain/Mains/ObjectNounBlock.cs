namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public class ObjectNounBlock : ObjectBlock
    {
        public override bool HasVerb => false;

        public ObjectNounBlock(NounPhrase nounPhrase) : base(nounPhrase)
        {
        }

        public override List<IGolin> GolinEnglish()
        {
            var golins = new List<IGolin>();
            golins.Add(this.ObjectPreposition);
            golins.AddRange(this.NounPhrase.GolinEnglish());
            return golins;
        }

        public override List<IGolin> GolinJapanese()
        {
            return this.NounPhrase.GolinJapanese();
        }
    }
}