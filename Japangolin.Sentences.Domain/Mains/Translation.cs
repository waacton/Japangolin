namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;

    public abstract class Translation : ITranslation
    {
        public IJapaneseEntry JapaneseEntry { get; }

        public string English { get; private set; }
        public string Kana { get; private set; }
        public string Kanji { get; private set; }

        public bool HasJapanese => !string.IsNullOrEmpty(this.Kana);

        public Translation(IJapaneseEntry japaneseEntry)
        {
            this.JapaneseEntry = japaneseEntry;
            this.ParseEntry();
        }

        public Translation(string english, string kanji, string kana)
        {
            this.English = english;
            this.Kanji = kanji;
            this.Kana = kana;
        }

        public abstract string Conjugate(Conjugation conjugation, bool isKana);

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
                { Conjugation.ShortPresentAffirmative, s => $"{s}だ" },
                { Conjugation.ShortPresentNegative, s => $"{s}じゃない" },
                { Conjugation.ShortPastAffirmative, s => $"{s}だった" },
                { Conjugation.ShortPastNegative, s => $"{s}じゃなかった" },
            };

        public NounTranslation(IJapaneseEntry japaneseEntry) : base(japaneseEntry)
        {
        }

        public override string Conjugate(Conjugation conjugation, bool isKana)
        {
            return Conjugations[conjugation].Invoke(isKana ? this.Kana : this.Kanji);
        }
    }

    // -----

    public class VerbTranslation : Translation
    {
        private static readonly Dictionary<Conjugation, Func<string, string>> Conjugations =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.LongPresentAffirmative, s => $"{s}" },
                { Conjugation.ShortPresentAffirmative, s => $"{s}" }
            };

        public VerbTranslation(IJapaneseEntry japaneseEntry) : base(japaneseEntry)
        {
        }

        public override string Conjugate(Conjugation conjugation, bool isKana)
        {
            throw new NotImplementedException();
        }
    }

    // -----

    public class AdjectiveTranslation : Translation
    {
        private static readonly Dictionary<Conjugation, Func<string, string>> Conjugations =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.LongPresentAffirmative, s => $"{s}" },
                { Conjugation.ShortPresentAffirmative, s => $"{s}" }
            };

        public AdjectiveTranslation(IJapaneseEntry japaneseEntry) : base(japaneseEntry)
        {
        }

        public override string Conjugate(Conjugation conjugation, bool isKana)
        {
            throw new NotImplementedException();
        }
    }

    // -----

    public class EnglishOnlyTranslation : Translation
    {
        public EnglishOnlyTranslation(string english) : base(english, null, null)
        {
        }

        public override string Conjugate(Conjugation conjugation, bool isKana)
        {
            throw new NotImplementedException();
        }
    }

    // -----

    public class JapaneseOnlyTranslation : Translation
    {
        public JapaneseOnlyTranslation(string kana) : base(null, kana, kana)
        {
        }

        public override string Conjugate(Conjugation conjugation, bool isKana)
        {
            throw new NotImplementedException();
        }
    }
}
