namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public class ObjectAdjectiveBlock : ObjectBlock
    {
        private readonly IGolin adjective;

        public override bool HasVerb => false;

        public ObjectAdjectiveBlock(IGolin adjective) : base(null)
        {
            this.adjective = adjective;
        }

        public override List<IGolin> GolinEnglish()
        {
            return new List<IGolin> { this.adjective };
        }

        public override List<IGolin> GolinJapanese()
        {
            return new List<IGolin> { this.adjective };
        }
    }
}