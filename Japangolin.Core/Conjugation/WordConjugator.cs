namespace Wacton.Japangolin.Core.Conjugation;

using System;
using Wacton.Japangolin.Core.Words;

public class WordConjugator
{
    private readonly Func<Word, Conjugator> getConjugator;

    public WordConjugator(Func<Word, Conjugator> getConjugator)
    {
        this.getConjugator = getConjugator;
    }

    public (string kana, string kanji) Conjugate(Word word)
    {
        var conjugator = getConjugator(word);
        var kana = conjugator.Conjugate(word.Kana);
        var kanji = conjugator.Conjugate(word.Kanji);
        return (kana, kanji);
    }

    public Hint GetHint(Word word)
    {
        var conjugator = getConjugator(word);
        return conjugator.Hint;
    }
}