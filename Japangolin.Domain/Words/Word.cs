namespace Wacton.Japangolin.Domain.Words
{
    using Wacton.Japangolin.Domain.Enums;

    public class Word
    {
        public string Kana { get; set; }
        public string Kanji { get; set; }
        public string English { get; set; }
        public WordClass Class { get; set; }

        public override string ToString() => $"{Kana} · {Class} · {English}";
    }
}
