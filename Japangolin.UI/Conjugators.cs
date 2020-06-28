﻿namespace Wacton.Japangolin
{
    using System;
    using System.Collections.Generic;

    // TODO: explicitly handle irregularities (e.g. adjective-i いい conjugates as よく)
    // TODO: expand te-form to allow negative (which will also allow things like potential & passive forms)
    /* some useful stuff can be found at https://en.wikipedia.org/wiki/Japanese_verb_conjugation */
    public static class Conjugators
    {
        public static readonly WordConjugator Dictionary = new WordConjugator((word) => Conjugator1D(word, DictByWordClass));
        public static readonly WordConjugator Stem = new WordConjugator((word) => Conjugator1D(word, StemsByWordClass));
        public static readonly WordConjugator Te = new WordConjugator((word) => Conjugator1D(word, TeFormsByWordClass));
        public static readonly WordConjugator PresentAffirmativeLong = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Present, Polarity.Affirmative, Formality.Long));
        public static readonly WordConjugator PresentAffirmativeShort = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Present, Polarity.Affirmative, Formality.Short));
        public static readonly WordConjugator PresentNegativeLong = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Present, Polarity.Negative, Formality.Long));
        public static readonly WordConjugator PresentNegativeShort = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Present, Polarity.Negative, Formality.Short));
        public static readonly WordConjugator PastAffirmativeLong = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Past, Polarity.Affirmative, Formality.Long));
        public static readonly WordConjugator PastAffirmativeShort = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Past, Polarity.Affirmative, Formality.Short));
        public static readonly WordConjugator PastNegativeLong = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Past, Polarity.Negative, Formality.Long));
        public static readonly WordConjugator PastNegativeShort = new WordConjugator((word) => Conjugator4D(word, ContextualByWordClass, Tense.Past, Polarity.Negative, Formality.Short));

        private static readonly Dictionary<WordClass, Conjugator> DictByWordClass;
        private static readonly Dictionary<WordClass, Conjugator> StemsByWordClass;
        private static readonly Dictionary<WordClass, Conjugator> TeFormsByWordClass;
        private static readonly Dictionary<WordClass, Conjugator[,,]> ContextualByWordClass;
        private static readonly Conjugator[,,] Noun;
        private static readonly Conjugator[,,] AdjectiveNa;
        private static readonly Conjugator[,,] AdjectiveI;
        private static readonly Conjugator[,,] VerbRu;
        private static readonly Conjugator[,,] VerbU;

        static Conjugators()
        {
            var identityConjugator = new Conjugator(x => x, "dict");

            ContextualByWordClass = new Dictionary<WordClass, Conjugator[,,]>();
            DictByWordClass = new Dictionary<WordClass, Conjugator>();
            StemsByWordClass = new Dictionary<WordClass, Conjugator>();
            TeFormsByWordClass = new Dictionary<WordClass, Conjugator>();

            Noun = new Conjugator[2, 2, 2];
            Set(Noun, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.NounStem(x)}です", $"{Infos.NounStem()}　＋です");
            Set(Noun, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.NounStem(x)}だ", $"{Infos.NounStem()}　＋だ");
            Set(Noun, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.NounStem(x)}じゃないです", $"{Infos.NounStem()}　＋じゃないです");
            Set(Noun, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.NounStem(x)}じゃない", $"{Infos.NounStem()}　＋じゃない");
            Set(Noun, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.NounStem(x)}でした", $"{Infos.NounStem()}　＋でした");
            Set(Noun, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.NounStem(x)}だった", $"{Infos.NounStem()}　＋だった");
            Set(Noun, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.NounStem(x)}じゃなかったです", $"{Infos.NounStem()}　＋じゃなかったです");
            Set(Noun, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.NounStem(x)}じゃなかった", $"{Infos.NounStem()}　＋じゃなかった");
            DictByWordClass.Add(WordClass.Noun, identityConjugator);
            StemsByWordClass.Add(WordClass.Noun, new Conjugator(x => $"{Forms.NounStem(x)}", $"{Infos.NounStem()}"));
            TeFormsByWordClass.Add(WordClass.Noun, new Conjugator(x => $"{Forms.NounFormTe(x)}", $"{Infos.NounFormTe()}"));
            ContextualByWordClass.Add(WordClass.Noun, Noun);

            AdjectiveNa = new Conjugator[2, 2, 2];
            Set(AdjectiveNa, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.AdjNaStem(x)}です", $"{Infos.AdjNaStem()}　＋です");
            Set(AdjectiveNa, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.AdjNaStem(x)}だ", $"{Infos.AdjNaStem()}　＋だ");
            Set(AdjectiveNa, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.AdjNaStem(x)}じゃないです", $"{Infos.AdjNaStem()}　＋じゃないです");
            Set(AdjectiveNa, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.AdjNaStem(x)}じゃない", $"{Infos.AdjNaStem()}　＋じゃない");
            Set(AdjectiveNa, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.AdjNaStem(x)}でした", $"{Infos.AdjNaStem()}　＋でした");
            Set(AdjectiveNa, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.AdjNaStem(x)}だった", $"{Infos.AdjNaStem()}　＋だった");
            Set(AdjectiveNa, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.AdjNaStem(x)}じゃなかったです", $"{Infos.AdjNaStem()}　＋じゃなかったです");
            Set(AdjectiveNa, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.AdjNaStem(x)}じゃなかった", $"{Infos.AdjNaStem()}　＋じゃなかった");
            DictByWordClass.Add(WordClass.AdjectiveNa, identityConjugator);
            StemsByWordClass.Add(WordClass.AdjectiveNa, new Conjugator(x => $"{Forms.AdjNaStem(x)}", $"{Infos.AdjNaStem()}"));
            TeFormsByWordClass.Add(WordClass.AdjectiveNa, new Conjugator(x => $"{Forms.AdjNaFormTe(x)}", $"{Infos.AdjNaFormTe()}"));
            ContextualByWordClass.Add(WordClass.AdjectiveNa, AdjectiveNa);

            AdjectiveI = new Conjugator[2, 2, 2];
            Set(AdjectiveI, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.Dict(x)}です", $"{Infos.Dict()}　＋です");
            Set(AdjectiveI, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.Dict(x)}", $"{Infos.Dict()}");
            Set(AdjectiveI, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.AdjIStem(x)}くないです", $"{Infos.AdjIStem()}　＋くないです");
            Set(AdjectiveI, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.AdjIStem(x)}くない", $"{Infos.AdjIStem()}　＋くない");
            Set(AdjectiveI, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.AdjIStem(x)}かったです", $"{Infos.AdjIStem()}　＋かったです");
            Set(AdjectiveI, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.AdjIStem(x)}かった", $"{Infos.AdjIStem()}　＋かった");
            Set(AdjectiveI, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.AdjIStem(x)}くなかったです", $"{Infos.AdjIStem()}　＋くなかったです");
            Set(AdjectiveI, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.AdjIStem(x)}くなかった", $"{Infos.AdjIStem()}　＋くなかった");
            DictByWordClass.Add(WordClass.AdjectiveI, identityConjugator);
            StemsByWordClass.Add(WordClass.AdjectiveI, new Conjugator(x => $"{Forms.AdjIStem(x)}", $"{Infos.AdjIStem()}"));
            TeFormsByWordClass.Add(WordClass.AdjectiveI, new Conjugator(x => $"{Forms.AdjIFormTe(x)}", $"{Infos.AdjIFormTe()}"));
            ContextualByWordClass.Add(WordClass.AdjectiveI, AdjectiveI);

            VerbRu = new Conjugator[2, 2, 2];
            Set(VerbRu, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ます", $"{Infos.VerbRuStem()}　＋ます");
            Set(VerbRu, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.Dict(x)}", $"{Infos.Dict()}");
            Set(VerbRu, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ません", $"{Infos.VerbRuStem()}　＋ません");
            Set(VerbRu, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.VerbRuStem(x)}ない", $"{Infos.VerbRuStem()}　＋ない");
            Set(VerbRu, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ました", $"{Infos.VerbRuStem()}　＋ました");
            Set(VerbRu, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.VerbRuFormTa(x)}", $"{Infos.VerbRuFormTa()}");
            Set(VerbRu, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.VerbRuStem(x)}ませんでした", $"{Infos.VerbRuStem()}　＋ませんでした");
            Set(VerbRu, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.VerbRuStem(x)}なかった", $"{Infos.VerbRuStem()}　＋なかった");
            DictByWordClass.Add(WordClass.VerbRu, identityConjugator);
            StemsByWordClass.Add(WordClass.VerbRu, new Conjugator(x => $"{Forms.VerbRuStem(x)}", $"{Infos.VerbRuStem()}"));
            TeFormsByWordClass.Add(WordClass.VerbRu, new Conjugator(x => $"{Forms.VerbRuFormTe(x)}", $"{Infos.VerbRuFormTe()}"));
            ContextualByWordClass.Add(WordClass.VerbRu, VerbRu);

            VerbU = new Conjugator[2, 2, 2];
            Set(VerbU, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ます", $"{Infos.VerbUStemI()}　＋ます");
            Set(VerbU, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{Forms.Dict(x)}", $"{Infos.Dict()}");
            Set(VerbU, Tense.Present, Polarity.Negative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ません", $"{Infos.VerbUStemI()}　＋ません");
            Set(VerbU, Tense.Present, Polarity.Negative, Formality.Short, x => $"{Forms.VerbUStemA(x)}ない", $"{Infos.VerbUStemA()}　＋ない");
            Set(VerbU, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ました", $"{Infos.VerbUStemI()}　＋ました");
            Set(VerbU, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{Forms.VerbUFormTa(x)}", $"{Infos.VerbUFormTa()}");
            Set(VerbU, Tense.Past, Polarity.Negative, Formality.Long, x => $"{Forms.VerbUStemI(x)}ませんでした", $"{Infos.VerbUStemI()}　＋ませんでした");
            Set(VerbU, Tense.Past, Polarity.Negative, Formality.Short, x => $"{Forms.VerbUStemA(x)}なかった", $"{Infos.VerbUStemA()}なかった");
            DictByWordClass.Add(WordClass.VerbU, identityConjugator);
            StemsByWordClass.Add(WordClass.VerbU, new Conjugator(x => $"{Forms.VerbUStemI(x)}", $"{Infos.VerbUStemI()}"));
            TeFormsByWordClass.Add(WordClass.VerbU, new Conjugator(x => $"{Forms.VerbUFormTe(x)}", $"{Infos.VerbUFormTe()}"));
            ContextualByWordClass.Add(WordClass.VerbU, VerbU);
        }

        public static Conjugator Conjugator1D(Word word, Dictionary<WordClass, Conjugator> conjLookup)
        {
            return conjLookup[word.Class];
        }

        public static Conjugator Conjugator4D(Word word, Dictionary<WordClass, Conjugator[,,]> conjLookup, Tense tense, Polarity polarity, Formality formality)
        {
            var contextualConjugators = conjLookup[word.Class];
            return contextualConjugators[(int)tense - 1, (int)polarity - 1, (int)formality - 1];
        }

        public static Conjugator Get(WordClass wordClass, Tense tense, Polarity polarity, Formality formality)
        {
            return ContextualByWordClass[wordClass][(int)tense - 1, (int)polarity - 1, (int)formality - 1];
        }

        public static Conjugator GetDict(WordClass wordClass)
        {
            return DictByWordClass[wordClass];
        }

        public static Conjugator GetStem(WordClass wordClass)
        {
            return StemsByWordClass[wordClass];
        }

        public static Conjugator GetTe(WordClass wordClass)
        {
            return TeFormsByWordClass[wordClass];
        }

        private static void Set(Conjugator[,,] conjugationMatrix, Tense tense, Polarity polarity, Formality formality, 
            Func<string, string> function, string hint)
        {
            // TODO: consider special cases like ～た and ～ない forms
            var abstractInfo = 
                $"{(tense == Tense.Present ? "🔜" : "🔙")}{(polarity == Polarity.Affirmative ? "✔" : "❌")}{(formality == Formality.Long ? "🙇" : "🗣")}";
            conjugationMatrix[(int)tense - 1, (int)polarity - 1, (int)formality - 1] = new Conjugator(function, hint);
        }
    }
}
