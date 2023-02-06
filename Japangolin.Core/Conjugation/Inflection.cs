﻿namespace Wacton.Japangolin.Core.Conjugation;

using Wacton.Desu.Enums;
using Wacton.Japangolin.Core.Utils;
using Wacton.Japangolin.Core.Words;

public class Inflection : Enumeration
{
    public static readonly Inflection Dictionary = new("Dictionary", Conjugators.Dictionary);
    public static readonly Inflection Stem = new("Stem", Conjugators.Stem);
    public static readonly Inflection Te = new("Te", Conjugators.Te);
    public static readonly Inflection PresentAffirmativeLong = new("PresentAffirmativeLong", Conjugators.PresentAffirmativeLong);
    public static readonly Inflection PresentAffirmativeShort = new("PresentAffirmativeShort", Conjugators.PresentAffirmativeShort);
    public static readonly Inflection PresentNegativeLong = new("PresentNegativeLong", Conjugators.PresentNegativeLong);
    public static readonly Inflection PresentNegativeShort = new("PresentNegativeShort", Conjugators.PresentNegativeShort);
    public static readonly Inflection PastAffirmativeLong = new("PastAffirmativeLong", Conjugators.PastAffirmativeLong);
    public static readonly Inflection PastAffirmativeShort = new("PastAffirmativeShort", Conjugators.PastAffirmativeShort);
    public static readonly Inflection PastNegativeLong = new("PastNegativeLong", Conjugators.PastNegativeLong);
    public static readonly Inflection PastNegativeShort = new("PastNegativeShort", Conjugators.PastNegativeShort);

    // ---

    private readonly WordConjugator wordConjugator;

    private Inflection(string displayName, WordConjugator wordConjugator)
        : base(displayName)
    {
        this.wordConjugator = wordConjugator;
    }

    public (string kana, string kanji) Conjugate(Word word)
    {
        return wordConjugator.Conjugate(word);
    }

    public Hint GetHint(Word word)
    {
        return wordConjugator.GetHint(word);
    }

    public string PrettyDisplay()
    {
        return StringUtils.PascalCase(DisplayName, " · ").ToLower();
    }

    public override string ToString() => PrettyDisplay();
}