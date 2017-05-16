namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Linq;

    using Wacton.Desu.Japanese;

    public class Translation : ITranslation
    {
        public IJapaneseEntry JapaneseEntry { get; }

        public string English { get; private set; }
        public string Kanji { get; private set; }
        public string Kana { get; private set; }

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

        private void ParseEntry()
        {
            this.English = this.JapaneseEntry.Senses.First().Glosses.First().Term;
            this.Kana = this.JapaneseEntry.Readings.First().Text;
            this.Kanji = this.JapaneseEntry.Kanjis.Any() ? this.JapaneseEntry.Kanjis.First().Text : this.Kana;
        }

        public Translation(string english)
        {
            this.English = english;
        }

        public override string ToString()
        {
            return this.English;
        }
    }

    // ---

    public class EnglishOnlyTranslation : Translation
    {
        public EnglishOnlyTranslation(string english) : base(english, null, null)
        {
        }
    }

    // ---

    public class JapaneseOnlyTranslation : Translation
    {
        public JapaneseOnlyTranslation(string kana) : base(null, kana, kana)
        {
        }
    }
}
