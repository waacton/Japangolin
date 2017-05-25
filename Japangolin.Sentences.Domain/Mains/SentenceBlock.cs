namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public abstract class SentenceBlock
    {
        public INounPhrase NounPhrase { get; }

        public SentenceBlock(INounPhrase nounPhrase)
        {
            this.NounPhrase = nounPhrase;
        }

        public abstract List<IGolin> GolinEnglish();

        public abstract List<IGolin> GolinJapanese();

    }

    public class TopicBlock : SentenceBlock
    {
        private readonly Conjugation conjugation;
        private IGolin TopicGolin => GolinFactory.CreateTopic(this.conjugation);

        public TopicBlock(INounPhrase nounPhrase, Conjugation conjugation) : base(nounPhrase)
        {
            this.conjugation = conjugation;
        }

        public override List<IGolin> GolinEnglish()
        {
            var golins = this.NounPhrase.GolinEnglish();
            golins.Add(this.TopicGolin);
            return golins;
        }

        public override List<IGolin> GolinJapanese()
        {
            var golins = this.NounPhrase.GolinJapanese();
            golins.Add(this.TopicGolin);
            return golins;
        }
    }

    public class ObjectBlock : SentenceBlock
    {
        private IGolin ObjectGolin = GolinFactory.CreateObject();

        public ObjectBlock(INounPhrase nounPhrase) : base(nounPhrase)
        {
        }
        
        public override List<IGolin> GolinEnglish()
        {
            var golins = this.NounPhrase.GolinEnglish();
            golins.Insert(0, this.ObjectGolin);
            return golins;
        }

        public override List<IGolin> GolinJapanese()
        {
            var golins = this.NounPhrase.GolinJapanese();
            return golins;
        }

    }
}
