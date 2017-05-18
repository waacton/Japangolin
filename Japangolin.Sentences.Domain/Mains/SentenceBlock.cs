namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public abstract class SentenceBlock
    {
        public SentenceBlockType Type { get; }
        public INounPhrase NounPhrase { get; }

        public SentenceBlock(SentenceBlockType type, INounPhrase nounPhrase)
        {
            this.Type = type;
            this.NounPhrase = nounPhrase;
        }

        public List<ITranslation> GetEnglishOrder(Conjugation conjugation)
        {
            var englishPreposition = this.Type.EnglishPreposition(conjugation);
            var prepositionTranslation = new EnglishOnlyTranslation(englishPreposition);
            var englishOrder = this.NounPhrase.GetEnglishOrder();
            var prepositionIndex = this.Type.Equals(SentenceBlockType.Topic) ? englishOrder.Count : 0;
            englishOrder.Insert(prepositionIndex, prepositionTranslation);
            return englishOrder;
        }

        public List<ITranslation> GetJapaneseOrder()
        {
            var japaneseParticle = this.Type.JapaneseParticle;
            var particleTranslation = new JapaneseOnlyTranslation(japaneseParticle);
            var japaneseOrder = this.NounPhrase.GetJapaneseOrder();
            japaneseOrder.Add(particleTranslation);
            return japaneseOrder;
        }
    }

    public class TopicBlock : SentenceBlock
    {
        public TopicBlock(INounPhrase nounPhrase) : base(SentenceBlockType.Topic, nounPhrase)
        {
        }
    }

    public class ObjectBlock : SentenceBlock
    {
        public ObjectBlock(INounPhrase nounPhrase) : base(SentenceBlockType.Object, nounPhrase)
        {
        }
    }
}
