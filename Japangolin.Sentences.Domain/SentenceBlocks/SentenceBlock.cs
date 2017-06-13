namespace Wacton.Japangolin.Sentences.Domain.SentenceBlocks
{
    using System.Collections.Generic;

    using Wacton.Japangolin.Sentences.Domain.Golins;
    using Wacton.Japangolin.Sentences.Domain.NounPhrases;

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
