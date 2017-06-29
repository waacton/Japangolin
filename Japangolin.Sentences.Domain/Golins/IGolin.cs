namespace Wacton.Japangolin.Sentences.Domain.Golins
{
    using System.Collections.Generic;

    public interface IGolin
    {
        string EnglishBase { get; }
        string KanaBase { get; }
        string KanjiBase { get; }

        string EnglishConjugated { get; }
        string KanaConjugated { get; }
        string KanjiConjugated { get; }

        IEnumerable<string> TranslationInformation { get; }

        bool IsTranslatable { get; }
    }
}
