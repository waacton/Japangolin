namespace Wacton.Japangolin.Sentences.Domain.SentenceBlocks
{
    using Wacton.Japangolin.Sentences.Domain.Golins;
    using Wacton.Japangolin.Sentences.Domain.NounPhrases;

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