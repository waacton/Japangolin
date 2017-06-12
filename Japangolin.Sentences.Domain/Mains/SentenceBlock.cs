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
}
