namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public abstract class SentenceBlock
    {
        public NounPhrase NounPhrase { get; }

        public SentenceBlock(NounPhrase nounPhrase)
        {
            this.NounPhrase = nounPhrase;
        }

        public abstract List<IGolin> GolinEnglish();

        public abstract List<IGolin> GolinJapanese();
    }

    public class TopicBlock : SentenceBlock
    {
        private readonly Conjugation conjugation;

        private IGolin TopicPreposition => GolinFactory.TopicPreposition(this.conjugation);
        private IGolin TopicMarker => GolinFactory.TopicMarker();

        public TopicBlock(NounPhrase nounPhrase, Conjugation conjugation) : base(nounPhrase)
        {
            this.conjugation = conjugation;
        }

        public override List<IGolin> GolinEnglish()
        {
            var golins = this.NounPhrase.GolinEnglish();
            golins.Add(this.TopicPreposition);
            return golins;
        }

        public override List<IGolin> GolinJapanese()
        {
            var golins = this.NounPhrase.GolinJapanese();
            golins.Add(this.TopicMarker);
            return golins;
        }
    }

    public class ObjectBlock : SentenceBlock
    {
        private IGolin ObjectPreposition = GolinFactory.ObjectPreposition();
        private IGolin DirectObjectMarker = GolinFactory.DirectObjectMarker();

        public IGolin Verb { get; }

        public ObjectBlock(NounPhrase nounPhrase, IGolin verb) : base(nounPhrase)
        {
            this.Verb = verb;
        }

        public ObjectBlock(NounPhrase nounPhrase) : this(nounPhrase, null)
        {
        }

        public override List<IGolin> GolinEnglish()
        {
            var golins = new List<IGolin>();
            if (this.Verb != null)
            {
                golins.Add(this.Verb);
            }

            golins.Add(this.ObjectPreposition);
            golins.AddRange(this.NounPhrase.GolinEnglish());
            return golins;
        }

        public override List<IGolin> GolinJapanese()
        {
            var golins = this.NounPhrase.GolinJapanese();
            if (this.Verb != null)
            {
                golins.Add(this.DirectObjectMarker);
                golins.Add(this.Verb);
            }
            
            return golins;
        }

    }
}
