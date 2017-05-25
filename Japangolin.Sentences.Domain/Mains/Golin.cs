namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    public class Golin : IGolin
    {
        private readonly English english;
        private readonly Japanese japanese;

        public string EnglishBase => this.english?.EnglishBase;
        public string EnglishConjugated => this.english?.EnglishConjugated;

        public string KanaBase => this.japanese?.KanaBase;
        public string KanaConjugated => this.japanese?.KanaConjugated;

        public string KanjiBase => this.japanese?.KanjiBase;
        public string KanjiConjugated => this.japanese?.KanjiConjugated;

        public bool IsTranslatable { get; }

        public Golin(English english, Japanese japanese, bool isTranslatable)
        {
            this.english = english;
            this.japanese = japanese;
            this.IsTranslatable = isTranslatable;
        }

        public override string ToString() => $"{this.EnglishBase ?? " - "} | {this.KanaBase ?? " - "}";
    }
}
