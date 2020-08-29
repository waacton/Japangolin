namespace Wacton.Japangolin.Domain.Conjugation
{
    using System;
    using System.Collections.Generic;
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Words;

    // TODO: explicitly handle irregularities (e.g. する、来る、 adjective-i いい conjugates as よく)
    // TODO: expand te-form to allow negative (which will also allow things like potential & passive forms)
    /* some useful stuff can be found at https://en.wikipedia.org/wiki/Japanese_verb_conjugation */
    public static class Conjugators
    {
        public static readonly WordConjugator Dictionary = new WordConjugator((word) => Conjugator1D(word, dictsByWordClass));
        public static readonly WordConjugator Stem = new WordConjugator((word) => Conjugator1D(word, stemsByWordClass));
        public static readonly WordConjugator Te = new WordConjugator((word) => Conjugator1D(word, teFormsByWordClass));
        public static readonly WordConjugator PresentAffirmativeLong = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Present, Polarity.Affirmative, Formality.Long));
        public static readonly WordConjugator PresentAffirmativeShort = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Present, Polarity.Affirmative, Formality.Short));
        public static readonly WordConjugator PresentNegativeLong = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Present, Polarity.Negative, Formality.Long));
        public static readonly WordConjugator PresentNegativeShort = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Present, Polarity.Negative, Formality.Short));
        public static readonly WordConjugator PastAffirmativeLong = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Past, Polarity.Affirmative, Formality.Long));
        public static readonly WordConjugator PastAffirmativeShort = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Past, Polarity.Affirmative, Formality.Short));
        public static readonly WordConjugator PastNegativeLong = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Past, Polarity.Negative, Formality.Long));
        public static readonly WordConjugator PastNegativeShort = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Past, Polarity.Negative, Formality.Short));

        private static readonly Dictionary<WordClass, Conjugator> dictsByWordClass;
        private static readonly Dictionary<WordClass, Conjugator> stemsByWordClass;
        private static readonly Dictionary<WordClass, Conjugator> teFormsByWordClass;
        private static readonly Dictionary<WordClass, Conjugator[,,]> ContextualByWordClass;
        private static readonly Conjugator[,,] noun;
        private static readonly Conjugator[,,] adjectiveNa;
        private static readonly Conjugator[,,] adjectiveI;
        private static readonly Conjugator[,,] verbRu;
        private static readonly Conjugator[,,] verbU;

        private static readonly string dictHint = "dictionary";
        private static readonly string stemHint = "stem";
        private static readonly string teHint = "te";

        static Conjugators()
        {
            var identityConjugator = new Conjugator(x => x, new Hint("the word itself"));

            dictsByWordClass = new Dictionary<WordClass, Conjugator>();
            stemsByWordClass = new Dictionary<WordClass, Conjugator>();
            teFormsByWordClass = new Dictionary<WordClass, Conjugator>();
            ContextualByWordClass = new Dictionary<WordClass, Conjugator[,,]>();

            noun = new Conjugator[2, 2, 2];
            dictsByWordClass.Add(WordClass.Noun, identityConjugator);
            stemsByWordClass.Add(WordClass.Noun, new Conjugator(x => $"{Forms.NounStem(x)}", new Hint(dictHint)));
            teFormsByWordClass.Add(WordClass.Noun, new Conjugator(x => $"{Forms.NounFormTe(x)}", new Hint(stemHint, "＋で")));
            Set(noun, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.NounStem(x)}です", new Hint(stemHint, "＋です"));
            Set(noun, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.NounStem(x)}だ", new Hint(stemHint, "＋だ"));
            Set(noun, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.NounStem(x)}じゃないです", new Hint(stemHint, "＋じゃないです")); // TODO: allow ではありません ?
            Set(noun, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.NounStem(x)}じゃない", new Hint(stemHint, "＋じゃない"));
            Set(noun, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.NounStem(x)}でした", new Hint(stemHint, "＋でした"));
            Set(noun, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.NounStem(x)}だった", new Hint(stemHint, "＋だった"));
            Set(noun, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.NounStem(x)}じゃなかったです", new Hint(stemHint, "＋じゃなかったです")); // TODO: allow ではありませんでした ?
            Set(noun, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.NounStem(x)}じゃなかった", new Hint(stemHint, "＋じゃなかった"));
            ContextualByWordClass.Add(WordClass.Noun, noun);

            adjectiveNa = new Conjugator[2, 2, 2];
            dictsByWordClass.Add(WordClass.AdjectiveNa, identityConjugator);
            stemsByWordClass.Add(WordClass.AdjectiveNa, new Conjugator(x => $"{Forms.AdjNaStem(x)}", new Hint(dictHint, "ーな")));
            teFormsByWordClass.Add(WordClass.AdjectiveNa, new Conjugator(x => $"{Forms.AdjNaFormTe(x)}", new Hint(stemHint, "＋で")));
            Set(adjectiveNa, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.AdjNaStem(x)}です", new Hint(stemHint, "＋です"));
            Set(adjectiveNa, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.AdjNaStem(x)}だ", new Hint(stemHint, "＋だ"));
            Set(adjectiveNa, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.AdjNaStem(x)}じゃないです", new Hint(stemHint, "＋じゃないです"));　// TODO: allow ではありません ?
            Set(adjectiveNa, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.AdjNaStem(x)}じゃない", new Hint(stemHint, "＋じゃない"));
            Set(adjectiveNa, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.AdjNaStem(x)}でした", new Hint(stemHint, "＋でした"));
            Set(adjectiveNa, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.AdjNaStem(x)}だった", new Hint(stemHint, "＋だった"));
            Set(adjectiveNa, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.AdjNaStem(x)}じゃなかったです", new Hint(stemHint, "＋じゃなかったです"));　// TODO: allow ではありませんでした ?
            Set(adjectiveNa, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.AdjNaStem(x)}じゃなかった", new Hint(stemHint, "＋じゃなかった"));
            ContextualByWordClass.Add(WordClass.AdjectiveNa, adjectiveNa);

            adjectiveI = new Conjugator[2, 2, 2];
            dictsByWordClass.Add(WordClass.AdjectiveI, identityConjugator);
            stemsByWordClass.Add(WordClass.AdjectiveI, new Conjugator(x => $"{Forms.AdjIStem(x)}", new Hint(dictHint, "ーい")));
            teFormsByWordClass.Add(WordClass.AdjectiveI, new Conjugator(x => $"{Forms.AdjIFormTe(x)}", new Hint(stemHint, "＋くて")));
            Set(adjectiveI, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.Dict(x)}です", new Hint(dictHint, "＋です"));
            Set(adjectiveI, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.Dict(x)}", new Hint(dictHint));
            Set(adjectiveI, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.AdjIStem(x)}くないです", new Hint(stemHint, "＋くないです"));　// TODO: allow くありませんでした ?
            Set(adjectiveI, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.AdjIStem(x)}くない", new Hint(stemHint, "＋くない"));
            Set(adjectiveI, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.AdjIStem(x)}かったです", new Hint(stemHint, "＋かったです"));
            Set(adjectiveI, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.AdjIStem(x)}かった", new Hint(stemHint, "＋かった"));
            Set(adjectiveI, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.AdjIStem(x)}くなかったです", new Hint(stemHint, "＋くなかったです"));　// TODO: allow くありませんでした ?
            Set(adjectiveI, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.AdjIStem(x)}くなかった", new Hint(stemHint, "＋くなかった"));
            ContextualByWordClass.Add(WordClass.AdjectiveI, adjectiveI);

            verbRu = new Conjugator[2, 2, 2];
            dictsByWordClass.Add(WordClass.VerbRu, identityConjugator);
            stemsByWordClass.Add(WordClass.VerbRu, new Conjugator(x => $"{Forms.VerbRuStem(x)}", new Hint(dictHint, "ーる")));
            teFormsByWordClass.Add(WordClass.VerbRu, new Conjugator(x => $"{Forms.VerbRuFormTe(x)}", new Hint(stemHint, "＋て")));
            Set(verbRu, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ます", new Hint(stemHint, "＋ます"));
            Set(verbRu, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.Dict(x)}", new Hint(dictHint));
            Set(verbRu, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ません",  new Hint(stemHint, "＋ません"));
            Set(verbRu, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.VerbRuStem(x)}ない",  new Hint(stemHint, "＋ない"));
            Set(verbRu, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ました",  new Hint(stemHint, "＋ました"));
            Set(verbRu, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.VerbRuFormTa(x)}", new Hint(teHint, "て　↦　た"));
            Set(verbRu, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ませんでした",  new Hint(stemHint, "＋ませんでした"));
            Set(verbRu, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.VerbRuStem(x)}なかった",  new Hint(stemHint, "＋なかった"));
            ContextualByWordClass.Add(WordClass.VerbRu, verbRu);

            verbU = new Conjugator[2, 2, 2];
            dictsByWordClass.Add(WordClass.VerbU, identityConjugator);
            stemsByWordClass.Add(WordClass.VerbU, new Conjugator(x => $"{Forms.VerbUStemI(x)}", new Hint(dictHint, "～〇〇う　↦　～〇〇い"))); // TODO: check usage
            teFormsByWordClass.Add(WordClass.VerbU, new Conjugator(x => $"{Forms.VerbUFormTe(x)}", new Hint(dictHint, "～〇〇う　↦　～〇〇て／で")));
            Set(verbU, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ます", new Hint(stemHint, "＋ます"));
            Set(verbU, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.Dict(x)}", new Hint(dictHint));
            Set(verbU, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ません",  new Hint(stemHint, "＋ません"));
            Set(verbU, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.VerbUStemA(x)}ない", new Hint(stemHint, "～〇〇い　↦　～〇〇あない"));
            Set(verbU, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ました",  new Hint(stemHint, "＋ました"));
            Set(verbU, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.VerbUFormTa(x)}", new Hint(teHint, "て／で　↦　た／だ"));
            Set(verbU, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ませんでした",  new Hint(stemHint, "＋ませんでした"));
            Set(verbU, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.VerbUStemA(x)}なかった", new Hint(stemHint, "～〇〇い　↦　～〇〇あなかった"));
            ContextualByWordClass.Add(WordClass.VerbU, verbU);
        }

        public static Conjugator Conjugator1D(Word word, Dictionary<WordClass, Conjugator> conjLookup)
        {
            return conjLookup[word.Class];
        }

        public static Conjugator Conjugator4D(Word word, Dictionary<WordClass, Conjugator[,,]> conjLookup, Tense tense, Polarity polarity, Formality formality)
        {
            var contextualConjugators = conjLookup[word.Class];
            return contextualConjugators[tense.Index, polarity.Index, formality.Index];
        }

        private static void Set(Conjugator[,,] conjugationMatrix, Tense tense, Polarity polarity, Formality formality, Func<string, string> function, Hint hint)
        {
            conjugationMatrix[tense.Index, polarity.Index, formality.Index] = new Conjugator(function, hint);
        }
    }
}
