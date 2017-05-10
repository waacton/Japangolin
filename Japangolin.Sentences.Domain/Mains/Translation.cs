namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    public class Translation
    {
        public string English { get; private set; }
        public string Kanji { get; private set; }
        public string Kana { get; private set; }

        public bool HasJapanese => !string.IsNullOrEmpty(this.Kana);

        public Translation(string english, string kanji, string kana)
        {
            this.English = english;
            this.Kanji = kanji;
            this.Kana = kana;
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
}
