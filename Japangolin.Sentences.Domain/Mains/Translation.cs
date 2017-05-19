namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;

    public abstract class Translation : ITranslation
    {
        public IJapaneseEntry JapaneseEntry { get; }

        public string English { get; private set; }
        public string Kana { get; private set; }
        public string Kanji { get; private set; }
        public Conjugation Conjugation { get; private set; }

        public abstract string EnglishConjugated { get; }
        public abstract string KanaConjugated { get; }
        public abstract string KanjiConjugated { get; }

        public bool HasJapanese => !string.IsNullOrEmpty(this.Kana);

        public Translation(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            this.JapaneseEntry = japaneseEntry;
            this.ParseEntry();
            this.Conjugation = conjugation;
        }

        public Translation(string english, string kanji, string kana, Conjugation conjugation)
        {
            this.English = english;
            this.Kanji = kanji;
            this.Kana = kana;
            this.Conjugation = conjugation;
        }

        private void ParseEntry()
        {
            this.English = this.JapaneseEntry.Senses.First().Glosses.First().Term;
            this.Kana = this.JapaneseEntry.Readings.First().Text;
            this.Kanji = this.JapaneseEntry.Kanjis.Any() ? this.JapaneseEntry.Kanjis.First().Text : this.Kana;
        }

        public override string ToString()
        {
            return this.English;
        }
    }

    /* TODO: this dual-conjugation does not make sense, needs thought  */

    // -----

    public class NounTranslation : Translation
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

        public override string EnglishConjugated => this.English;
        public override string KanaConjugated => Conjugations[this.Conjugation].Invoke(this.Kana);
        public override string KanjiConjugated => Conjugations[this.Conjugation].Invoke(this.Kanji);

        public NounTranslation(IJapaneseEntry japaneseEntry, Conjugation conjugation) : base(japaneseEntry, conjugation)
        {
        }
    }

    // -----

    public class VerbTranslation : Translation
    {
        public override string EnglishConjugated => this.English;
        public override string KanaConjugated => this.Kana;
        public override string KanjiConjugated => this.Kanji;

        public VerbTranslation(IJapaneseEntry japaneseEntry, Conjugation conjugation) : base(japaneseEntry, conjugation)
        {
        }
    }

    // -----

    public class AdjectiveTranslation : Translation
    {
        public override string EnglishConjugated => this.English;
        public override string KanaConjugated => this.Kana;
        public override string KanjiConjugated => this.Kanji;

        public AdjectiveTranslation(IJapaneseEntry japaneseEntry, Conjugation conjugation) : base(japaneseEntry, conjugation)
        {
        }
    }

    // -----

    public class ToBeTranslation : Translation
    {
        private static readonly Dictionary<Conjugation, string> Prepositions =
            new Dictionary<Conjugation, string>
            {
                { Conjugation.LongPresentAffirmative, "is" },
                { Conjugation.LongPresentNegative, "is not" },
                { Conjugation.LongPastAffirmative, "was" },
                { Conjugation.LongPastNegative, "was not" },
                { Conjugation.LongFutureAffirmative, "will be" },
                { Conjugation.LongFutureNegative, "will not be" },
                { Conjugation.ShortPresentAffirmative, "is" },
                { Conjugation.ShortPresentNegative, "is not" },
                { Conjugation.ShortPastAffirmative, "was" },
                { Conjugation.ShortPastNegative, "was not" },
                { Conjugation.ShortFutureAffirmative, "will be" },
                { Conjugation.ShortFutureNegative, "will not be" }
            };

        public ToBeTranslation(Conjugation conjugation) : base("is", null, null, conjugation)
        {
        }

        public override string EnglishConjugated => Prepositions[this.Conjugation];
        public override string KanaConjugated => this.Kana;
        public override string KanjiConjugated => this.Kanji;
    }

    // -----

    public class EnglishOnlyTranslation : Translation
    {
        public override string EnglishConjugated => this.English;
        public override string KanaConjugated => this.Kana;
        public override string KanjiConjugated => this.Kanji;

        public EnglishOnlyTranslation(string english, Conjugation conjugation) : base(english, null, null, conjugation)
        {
        }
    }

    // -----

    public class JapaneseOnlyTranslation : Translation
    {
        public override string EnglishConjugated => this.English;
        public override string KanaConjugated => this.Kana;
        public override string KanjiConjugated => this.Kanji;

        public JapaneseOnlyTranslation(string kana, Conjugation conjugation) : base(null, kana, kana, conjugation)
        {
        }
    }
}
