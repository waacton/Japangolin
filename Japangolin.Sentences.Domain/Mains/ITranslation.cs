namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    public interface ITranslation
    {
        string English { get; }
        string Kana { get; }
        string Kanji { get; }

        Conjugation Conjugation { get; }

        string EnglishConjugated { get; }
        string KanaConjugated { get; }
        string KanjiConjugated { get; }
    }
}
