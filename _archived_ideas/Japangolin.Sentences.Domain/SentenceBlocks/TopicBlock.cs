namespace Wacton.Japangolin.Sentences.Domain.SentenceBlocks
{
    using System.Collections.Generic;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;
    using Wacton.Japangolin.Sentences.Domain.NounPhrases;

    public class TopicBlock : SentenceBlock
    {
        private readonly Conjugation conjugation;
        private readonly bool isVerbInSentence;

        private IGolin TopicPreposition => GolinFactory.TopicPreposition(this.conjugation, this.isVerbInSentence);
        private IGolin TopicMarker => GolinFactory.TopicMarker();

        public TopicBlock(NounPhrase nounPhrase, Conjugation conjugation, bool isVerbInSentence) : base(nounPhrase)
        {
            this.conjugation = conjugation;
            this.isVerbInSentence = isVerbInSentence;
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
}