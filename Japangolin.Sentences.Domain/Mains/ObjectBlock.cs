namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    public abstract class ObjectBlock : SentenceBlock
    {
        protected IGolin ObjectPreposition = GolinFactory.ObjectPreposition();

        protected IGolin DirectObjectMarker = GolinFactory.DirectObjectMarker();

        public abstract bool HasVerb { get; }

        public ObjectBlock(NounPhrase nounPhrase) : base(nounPhrase)
        {
        }
    }
}