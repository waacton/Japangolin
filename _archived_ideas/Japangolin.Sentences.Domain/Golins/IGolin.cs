namespace Wacton.Japangolin.Sentences.Domain.Golins
{
    using System.Collections.Generic;

    using Wacton.Japangolin.Sentences.Domain.Conjugations;

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

        Conjugation Conjugation { get; }
        string ConjugationInformation { get; }
    }
}
