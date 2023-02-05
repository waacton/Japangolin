namespace Wacton.Japangolin.Core.Conjugation;

using System;
using System.Collections.Generic;
using Wacton.Japangolin.Core.Enums;
using Wacton.Japangolin.Core.Words;

// TODO: explicitly handle irregularities (e.g. する、来る、 adjective-i いい conjugates as よく)
// TODO: expand te-form to allow negative (which will also allow things like potential & passive forms)
/* some useful stuff can be found at https://en.wikipedia.org/wiki/Japanese_verb_conjugation */
    
/*
 * a note on placeholder characters:
 * 〇〇 (U+3007) seems to be the primary placeholder referenced in JMdict (https://jisho.org/word/%E3%80%87%E3%80%87)
 * ○○ (U+25CB) is the symbol used by wikipedia placeholder examples (https://en.wiktionary.org/wiki/%E2%97%8B)
 * using ○○ (U+25CB) as it has less visual weight, less distracting
 */
public static class Conjugators
{
    public static readonly WordConjugator Dictionary = new(word => Conjugator1D(word, DictionaryLookup));
    public static readonly WordConjugator Stem = new(word => Conjugator1D(word, StemLookup));
    public static readonly WordConjugator Te = new(word => Conjugator1D(word, TeLookup));
    public static readonly WordConjugator PresentAffirmativeLong = new(word => Conjugator4D(word, ContextualLookup, Tense.Present, Polarity.Affirmative, Formality.Long));
    public static readonly WordConjugator PresentAffirmativeShort = new(word => Conjugator4D(word, ContextualLookup, Tense.Present, Polarity.Affirmative, Formality.Short));
    public static readonly WordConjugator PresentNegativeLong = new(word => Conjugator4D(word, ContextualLookup, Tense.Present, Polarity.Negative, Formality.Long));
    public static readonly WordConjugator PresentNegativeShort = new(word => Conjugator4D(word, ContextualLookup, Tense.Present, Polarity.Negative, Formality.Short));
    public static readonly WordConjugator PastAffirmativeLong = new(word => Conjugator4D(word, ContextualLookup, Tense.Past, Polarity.Affirmative, Formality.Long));
    public static readonly WordConjugator PastAffirmativeShort = new(word => Conjugator4D(word, ContextualLookup, Tense.Past, Polarity.Affirmative, Formality.Short));
    public static readonly WordConjugator PastNegativeLong = new(word => Conjugator4D(word, ContextualLookup, Tense.Past, Polarity.Negative, Formality.Long));
    public static readonly WordConjugator PastNegativeShort = new(word => Conjugator4D(word, ContextualLookup, Tense.Past, Polarity.Negative, Formality.Short));

    private static readonly Dictionary<WordClass, Conjugator> DictionaryLookup = new();
    private static readonly Dictionary<WordClass, Conjugator> StemLookup = new();
    private static readonly Dictionary<WordClass, Conjugator> TeLookup = new();
    private static readonly Dictionary<WordClass, Conjugator[,,]> ContextualLookup = new();

    private const string DictionaryHint = "dictionary";
    private const string StemHint = "stem";
    private const string TeHint = "te";
    
    static Conjugators()
    {
        InitialiseNoun();
        InitialiseAdjectiveNa();
        InitialiseAdjectiveI();
        InitialiseVerbRu();
        InitialiseVerbU();
    }
    
    private static readonly Conjugator IdentityConjugator = new(x => x, new Hint("the word itself"));

    private static void InitialiseNoun()
    {
        DictionaryLookup.Add(WordClass.Noun, IdentityConjugator);
        StemLookup.Add(WordClass.Noun, new Conjugator(x => $"{Forms.NounStem(x)}", new Hint(DictionaryHint)));
        TeLookup.Add(WordClass.Noun, new Conjugator(x => $"{Forms.NounFormTe(x)}", new Hint(StemHint, "＋で")));
        
        var matrix = new Conjugator[2, 2, 2];
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.NounStem(x)}です", new Hint(StemHint, "＋です"));
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.NounStem(x)}だ", new Hint(StemHint, "＋だ"));
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.NounStem(x)}じゃないです", new Hint(StemHint, "＋じゃないです")); // TODO: allow ではありません ?
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.NounStem(x)}じゃない", new Hint(StemHint, "＋じゃない"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.NounStem(x)}でした", new Hint(StemHint, "＋でした"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.NounStem(x)}だった", new Hint(StemHint, "＋だった"));
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.NounStem(x)}じゃなかったです", new Hint(StemHint, "＋じゃなかったです")); // TODO: allow ではありませんでした ?
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.NounStem(x)}じゃなかった", new Hint(StemHint, "＋じゃなかった"));
        ContextualLookup.Add(WordClass.Noun, matrix);
    }
    
    private static void InitialiseAdjectiveNa()
    {
        DictionaryLookup.Add(WordClass.AdjectiveNa, IdentityConjugator);
        StemLookup.Add(WordClass.AdjectiveNa, new Conjugator(x => $"{Forms.AdjNaStem(x)}", new Hint(DictionaryHint, "ーな")));
        TeLookup.Add(WordClass.AdjectiveNa, new Conjugator(x => $"{Forms.AdjNaFormTe(x)}", new Hint(StemHint, "＋で")));
        
        var matrix = new Conjugator[2, 2, 2];
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.AdjNaStem(x)}です", new Hint(StemHint, "＋です"));
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.AdjNaStem(x)}だ", new Hint(StemHint, "＋だ"));
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.AdjNaStem(x)}じゃないです", new Hint(StemHint, "＋じゃないです"));　// TODO: allow ではありません ?
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.AdjNaStem(x)}じゃない", new Hint(StemHint, "＋じゃない"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.AdjNaStem(x)}でした", new Hint(StemHint, "＋でした"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.AdjNaStem(x)}だった", new Hint(StemHint, "＋だった"));
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.AdjNaStem(x)}じゃなかったです", new Hint(StemHint, "＋じゃなかったです"));　// TODO: allow ではありませんでした ?
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.AdjNaStem(x)}じゃなかった", new Hint(StemHint, "＋じゃなかった"));
        ContextualLookup.Add(WordClass.AdjectiveNa, matrix);
    }
    
    private static void InitialiseAdjectiveI()
    {
        DictionaryLookup.Add(WordClass.AdjectiveI, IdentityConjugator);
        StemLookup.Add(WordClass.AdjectiveI, new Conjugator(x => $"{Forms.AdjIStem(x)}", new Hint(DictionaryHint, "ーい")));
        TeLookup.Add(WordClass.AdjectiveI, new Conjugator(x => $"{Forms.AdjIFormTe(x)}", new Hint(StemHint, "＋くて")));
        
        var matrix = new Conjugator[2, 2, 2];
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.Dict(x)}です", new Hint(DictionaryHint, "＋です"));
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.Dict(x)}", new Hint(DictionaryHint));
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.AdjIStem(x)}くないです", new Hint(StemHint, "＋くないです"));　// TODO: allow くありませんでした ?
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.AdjIStem(x)}くない", new Hint(StemHint, "＋くない"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.AdjIStem(x)}かったです", new Hint(StemHint, "＋かったです"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.AdjIStem(x)}かった", new Hint(StemHint, "＋かった"));
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.AdjIStem(x)}くなかったです", new Hint(StemHint, "＋くなかったです"));　// TODO: allow くありませんでした ?
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.AdjIStem(x)}くなかった", new Hint(StemHint, "＋くなかった"));
        ContextualLookup.Add(WordClass.AdjectiveI, matrix);
    }
    
    private static void InitialiseVerbRu()
    {
        DictionaryLookup.Add(WordClass.VerbRu, IdentityConjugator);
        StemLookup.Add(WordClass.VerbRu, new Conjugator(x => $"{Forms.VerbRuStem(x)}", new Hint(DictionaryHint, "ーる")));
        TeLookup.Add(WordClass.VerbRu, new Conjugator(x => $"{Forms.VerbRuFormTe(x)}", new Hint(StemHint, "＋て")));
        
        var matrix = new Conjugator[2, 2, 2];
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ます", new Hint(StemHint, "＋ます"));
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.Dict(x)}", new Hint(DictionaryHint));
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ません", new Hint(StemHint, "＋ません"));
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.VerbRuStem(x)}ない", new Hint(StemHint, "＋ない"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ました", new Hint(StemHint, "＋ました"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.VerbRuFormTa(x)}", new Hint(TeHint, "て　↦　た"));
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ませんでした", new Hint(StemHint, "＋ませんでした"));
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.VerbRuStem(x)}なかった", new Hint(StemHint, "＋なかった"));
        ContextualLookup.Add(WordClass.VerbRu, matrix);
    }
    
    private static void InitialiseVerbU()
    {
        DictionaryLookup.Add(WordClass.VerbU, IdentityConjugator);
        StemLookup.Add(WordClass.VerbU, new Conjugator(x => $"{Forms.VerbUStemI(x)}", new Hint(DictionaryHint, "～○○う　↦　～○○い"))); // TODO: check usage
        TeLookup.Add(WordClass.VerbU, new Conjugator(x => $"{Forms.VerbUFormTe(x)}", new Hint(DictionaryHint, "～○○う　↦　～○○て／で")));
        
        var matrix = new Conjugator[2, 2, 2];
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ます", new Hint(StemHint, "＋ます"));
        Set(matrix, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.Dict(x)}", new Hint(DictionaryHint));
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ません", new Hint(StemHint, "＋ません"));
        Set(matrix, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.VerbUStemA(x)}ない", new Hint(StemHint, "～○○う　↦　～○○あない"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ました", new Hint(StemHint, "＋ました"));
        Set(matrix, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.VerbUFormTa(x)}", new Hint(TeHint, "て／で　↦　た／だ"));
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ませんでした", new Hint(StemHint, "＋ませんでした"));
        Set(matrix, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.VerbUStemA(x)}なかった", new Hint(StemHint, "～○○う　↦　～○○あなかった"));
        ContextualLookup.Add(WordClass.VerbU, matrix);
    }

    private static Conjugator Conjugator1D(Word word, Dictionary<WordClass, Conjugator> lookup)
    {
        return lookup[word.Class];
    }

    private static Conjugator Conjugator4D(Word word, Dictionary<WordClass, Conjugator[,,]> lookup, Tense tense, Polarity polarity, Formality formality)
    {
        var matrix = lookup[word.Class];
        return matrix[tense.Index, polarity.Index, formality.Index];
    }

    private static void Set(Conjugator[,,] matrix, Tense tense, Polarity polarity, Formality formality, Func<string, string> function, Hint hint)
    {
        matrix[tense.Index, polarity.Index, formality.Index] = new Conjugator(function, hint);
    }
}