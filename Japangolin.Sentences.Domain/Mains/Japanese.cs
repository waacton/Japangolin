namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;

    public abstract class Japanese
    {
        public string KanaBase { get; }
        public string KanjiBase { get; }
        public Conjugation Conjugation { get; }

        public virtual string KanaConjugated => this.KanaBase;
        public virtual string KanjiConjugated => this.KanjiBase;

        protected Japanese(string kanaBase, string kanjiBase, Conjugation conjugation)
        {
            this.KanaBase = kanaBase;
            this.KanjiBase = kanjiBase;
            this.Conjugation = conjugation;
        }

        protected Japanese(string japaneseBase, Conjugation conjugation) : this(japaneseBase, japaneseBase, conjugation)
        {
        }
    }

    public class UnconjugatedJapanese : Japanese
    {
        public UnconjugatedJapanese(string kana, string kanji) : base(kana, kanji, Conjugation.None)
        {
        }

        public UnconjugatedJapanese(string japanese) : this(japanese, japanese)
        {
        }
    }

    public class TopicJapanese : Japanese
    {
        public TopicJapanese() : base("は", Conjugation.None)
        {
        }
    }

    public class NounJapanese : Japanese
    {
        private static readonly Dictionary<Conjugation, Func<string, string>> Conjugations =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
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

        public override string KanaConjugated => Conjugations[this.Conjugation].Invoke(this.KanaBase);
        public override string KanjiConjugated => Conjugations[this.Conjugation].Invoke(this.KanjiBase);

        public NounJapanese(string kana, string kanji, Conjugation conjugation) : base(kana, kanji, conjugation)
        {
        }
    }
}
