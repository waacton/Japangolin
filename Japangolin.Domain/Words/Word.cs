using Wacton.Japangolin.Domain.Enums;

namespace Wacton.Japangolin.Domain.Words
{
    public class Word
    {
        public string Kana { get; set; }
        public string Kanji { get; set; }
        public string English { get; set; }
        public WordClass Class { get; set; }

        public override string ToString() => $"{this.Kana} · {this.Class} · {this.English}";
    }
}
