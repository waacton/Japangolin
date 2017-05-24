namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Linq;

    using Wacton.Desu.Japanese;

    public abstract class Golin : IGolin
    {
        private ConjugatedEnglish conjugatedEnglish;
        private ConjugatedJapanese conjugatedJapanese;

        public IJapaneseEntry JapaneseEntry { get; }
        public Conjugation Conjugation { get; }

        public string EnglishBase => this.conjugatedEnglish?.EnglishBase;
        public string EnglishConjugated => this.conjugatedEnglish?.EnglishConjugated;

        public string KanaBase => this.conjugatedJapanese?.KanaBase;
        public string KanaConjugated => this.conjugatedJapanese?.KanaConjugated;

        public string KanjiBase => this.conjugatedJapanese?.KanjiBase;
        public string KanjiConjugated => this.conjugatedJapanese?.KanjiConjugated;

        public bool IsTranslatable { get; }

        public Golin(IJapaneseEntry japaneseEntry, Conjugation conjugation)
        {
            this.JapaneseEntry = japaneseEntry;
            this.Conjugation = conjugation;
            this.ParseEntry(conjugation);
            this.IsTranslatable = true;
        }

        public Golin(ConjugatedEnglish conjugatedEnglish, ConjugatedJapanese conjugatedJapanese, bool isTranslatable = false)
        {
            this.conjugatedEnglish = conjugatedEnglish;
            this.conjugatedJapanese = conjugatedJapanese;
            this.IsTranslatable = isTranslatable;
        }

        public Golin(ConjugatedEnglish conjugatedEnglish, bool isTranslatable = false) : this(conjugatedEnglish, null, isTranslatable)
        {
        }

        public Golin(ConjugatedJapanese conjugatedJapanese, bool isTranslatable = false) : this(null, conjugatedJapanese, isTranslatable)
        {
        }

        private void ParseEntry(Conjugation conjugation)
        {
            var english = this.JapaneseEntry.Senses.First().Glosses.First().Term;
            var kana = this.JapaneseEntry.Readings.First().Text;
            var kanji = this.JapaneseEntry.Kanjis.Any() ? this.JapaneseEntry.Kanjis.First().Text : kana;

            this.conjugatedEnglish = new ConjugatedEnglish(english, conjugation);
            this.conjugatedJapanese = new ConjugatedJapanese(kana, kanji, conjugation);
        }

        public override string ToString() => $"{this.EnglishBase ?? "..."} | {this.KanaBase ?? "..."}";
    }

    // -----

    public class Noungolin : Golin
    {
        public Noungolin(IJapaneseEntry japaneseEntry, Conjugation conjugation) : base(japaneseEntry, conjugation)
        {
        }
    }

    public class Verbgolin : Golin
    {
        public Verbgolin(IJapaneseEntry japaneseEntry, Conjugation conjugation) : base(japaneseEntry, conjugation)
        {
        }
    }

    public class Adjectivegolin : Golin
    {
        public Adjectivegolin(IJapaneseEntry japaneseEntry, Conjugation conjugation) : base(japaneseEntry, conjugation)
        {
        }
    }

    // -----

    public class Topicgolin : Golin
    {
        public Topicgolin(Conjugation conjugation) : base(new TopicEnglish(conjugation), new TopicJapanese(conjugation))
        {
        }
    }

    public class Objectgolin : Golin
    {
        public Objectgolin(Conjugation conjugation) : base(new ConjugatedEnglish("a", conjugation))
        {
        }
    }

    public class ObjectNoungolin : Golin
    {
        public ObjectNoungolin(NounEnglish nounEnglish, ObjectNounJapanese objectNounJapanese) : base(nounEnglish, objectNounJapanese)
        {
        }
    }

    public class Possessiongolin : Golin
    {
        public Possessiongolin(Conjugation conjugation) : base(new ConjugatedJapanese("の", conjugation))
        {
        }
    }
}
