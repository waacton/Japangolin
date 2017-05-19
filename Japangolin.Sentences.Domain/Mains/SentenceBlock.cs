namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;

    public abstract class SentenceBlock
    {
        public INounPhrase NounPhrase { get; }
        public Conjugation Conjugation { get; }

        public SentenceBlock(INounPhrase nounPhrase, Conjugation conjugation)
        {
            this.NounPhrase = nounPhrase;
            this.Conjugation = conjugation;
        }

        public abstract List<ITranslation> GetEnglishOrder();

        public abstract List<ITranslation> GetJapaneseOrder();

    }

    public class TopicBlock : SentenceBlock
    {
        public TopicBlock(INounPhrase nounPhrase, Conjugation conjugation) : base(nounPhrase, conjugation)
        {
        }

        public override List<ITranslation> GetEnglishOrder()
        {
            var translation = new ToBeTranslation(this.Conjugation);
            var englishOrder = this.NounPhrase.GetEnglishOrder();
            englishOrder.Add(translation);
            return englishOrder;
        }

        public override List<ITranslation> GetJapaneseOrder()
        {
            return this.NounPhrase.GetJapaneseOrder();
        }
    }

    public class ObjectBlock : SentenceBlock
    {
        private static readonly Dictionary<Conjugation, Func<string, string>> Conjugations =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.LongPresentAffirmative, s => $"{s}です" },
                { Conjugation.LongPresentNegative, s => $"{s}じゃないです" },
                { Conjugation.LongPastAffirmative, s => $"{s}でした" },
                { Conjugation.LongPastNegative, s => $"{s}じゃなかったです" },
                { Conjugation.LongFutureAffirmative, s => $"{s}です" },
                { Conjugation.LongFutureNegative, s => $"{s}じゃないです" },
                { Conjugation.ShortPresentAffirmative, s => $"{s}だ" },
                { Conjugation.ShortPresentNegative, s => $"{s}じゃない" },
                { Conjugation.ShortPastAffirmative, s => $"{s}だった" },
                { Conjugation.ShortPastNegative, s => $"{s}じゃなかった" },
                { Conjugation.ShortFutureAffirmative, s => $"{s}だ" },
                { Conjugation.ShortFutureNegative, s => $"{s}じゃない" }
            };

        public ObjectBlock(INounPhrase nounPhrase, Conjugation conjugation) : base(nounPhrase, conjugation)
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
