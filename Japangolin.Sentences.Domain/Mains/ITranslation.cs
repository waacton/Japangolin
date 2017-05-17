﻿namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    public interface ITranslation
    {
        string English { get; }
        string Kana { get; }
        string Kanji { get; }

        string Conjugate(Conjugation conjugation, bool isKana);
    }
}
