namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;

    public abstract class SentenceBlock
    {
        public INounPhrase NounPhrase { get; }
        public Conjugation Conjugation => this.NounPhrase.Conjugation;

        public SentenceBlock(INounPhrase nounPhrase)
        {
            this.NounPhrase = nounPhrase;
        }

        public abstract List<ITranslation> GetEnglishOrder();

        public abstract List<ITranslation> GetJapaneseOrder();

    }

    public class TopicBlock : SentenceBlock
    {
        public TopicBlock(INounPhrase nounPhrase) : base(nounPhrase)
        {
        }

        public override List<ITranslation> GetEnglishOrder()
        {
            var translation = new ToBeTranslation(this.NounPhrase.Conjugation);
            var englishOrder = this.NounPhrase.GetEnglishOrder();
            englishOrder.Add(translation);
            return englishOrder;
        }

        public override List<ITranslation> GetJapaneseOrder()
        {
            var translation = new JapaneseOnlyTranslation("は", this.NounPhrase.Conjugation);
            var japaneseOrder = this.NounPhrase.GetEnglishOrder();
            japaneseOrder.Add(translation);
            return japaneseOrder;
        }
    }

    public class ObjectBlock : SentenceBlock
    {
        public ObjectBlock(INounPhrase nounPhrase) : base(nounPhrase)
        {
        }

        public override List<ITranslation> GetEnglishOrder()
        {
            var translation = new EnglishOnlyTranslation("a", this.Conjugation);
            var englishOrder = this.NounPhrase.GetEnglishOrder();
            englishOrder.Insert(0, translation);
            return englishOrder;
        }

        public override List<ITranslation> GetJapaneseOrder()
        {
            return this.NounPhrase.GetJapaneseOrder();
        }
    }
}
