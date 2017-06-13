namespace Wacton.Japangolin.Sentences.Domain.Golins
{
    public interface IGolin
    {
        string EnglishBase { get; }
        string KanaBase { get; }
        string KanjiBase { get; }

        string EnglishConjugated { get; }
        string KanaConjugated { get; }
        string KanjiConjugated { get; }

        string TranslationInformation { get; }

        bool IsTranslatable { get; }
    }
}
