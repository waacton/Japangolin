namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    public interface IGolin
    {
        string EnglishBase { get; }
        string KanaBase { get; }
        string KanjiBase { get; }

        string EnglishConjugated { get; }
        string KanaConjugated { get; }
        string KanjiConjugated { get; }

        bool IsTranslatable { get; }
    }
}
