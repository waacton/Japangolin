namespace Wacton.Japangolin.Sentences.Domain.Golins
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

        public string TranslationInformation { get; }

        public bool IsTranslatable => !string.IsNullOrWhiteSpace(this.TranslationInformation);

        public Golin(English english, Japanese japanese, string translationInformation = null)
        {
            this.english = english;
            this.japanese = japanese;
            this.TranslationInformation = translationInformation;
        }

        public override string ToString() => $"{this.EnglishBase ?? " - "} | {this.KanaBase ?? " - "}";
    }
}
