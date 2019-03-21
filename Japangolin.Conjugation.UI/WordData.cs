namespace Wacton.Japangolin.Conjugation
{
    using Wacton.Japangolin.Conjugation;

    public class WordData
    {
        public string Kana { get; set; }
        public string Kanji { get; set; }
        public string English { get; set; }
        public WordClass Class { get; set; }

        public override string ToString()
        {
            return $"{this.Kana} / {this.Class} / {this.English}";
        }
    }
}
