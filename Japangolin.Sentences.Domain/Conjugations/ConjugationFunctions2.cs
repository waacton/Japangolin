namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConjugationFunctions2
    {
        /* some useful stuff can be found at https://en.wikipedia.org/wiki/Japanese_verb_conjugation */

        public static readonly Func<string, string>[,,] Noun;
        public static readonly Func<string, string>[,,] AdjectiveNa;
        public static readonly Func<string, string>[,,] AdjectiveI;
        public static readonly Func<string, string>[,,] VerbRu;
        public static readonly Func<string, string>[,,] VerbU;
        public static readonly Dictionary<WordClass, Func<string, string>[,,]> ConjugationsByWordClass;

        public static readonly Dictionary<WordClass, Func<string, string>> StemsByWordClass;
        public static readonly Dictionary<WordClass, Func<string, string>> TeFormsByWordClass;

        static ConjugationFunctions2()
        {
            ConjugationsByWordClass = new Dictionary<WordClass, Func<string, string>[,,]>();
            StemsByWordClass = new Dictionary<WordClass, Func<string, string>>();
            TeFormsByWordClass = new Dictionary<WordClass, Func<string, string>>();

            Noun = new Func<string, string>[2, 2, 2];
            Set(Noun,            Tense.Present,  Polarity.Affirmative,   Formality.Long,    x => $"{x}です");
            Set(Noun,            Tense.Present,  Polarity.Affirmative,   Formality.Short,   x => $"{x}だ");
            Set(Noun,            Tense.Present,  Polarity.Negative,      Formality.Long,    x => $"{x}じゃないです");
            Set(Noun,            Tense.Present,  Polarity.Negative,      Formality.Short,   x => $"{x}じゃない");
            Set(Noun,            Tense.Past,     Polarity.Affirmative,   Formality.Long,    x => $"{x}でした");
            Set(Noun,            Tense.Past,     Polarity.Affirmative,   Formality.Short,   x => $"{x}だった");
            Set(Noun,            Tense.Past,     Polarity.Negative,      Formality.Long,    x => $"{x}じゃなかったです");
            Set(Noun,            Tense.Past,     Polarity.Negative,      Formality.Short,   x => $"{x}じゃなかった");
            StemsByWordClass.Add(WordClass.JapaneseNoun,                                    x => $"TODO: {x}");
            TeFormsByWordClass.Add(WordClass.JapaneseNoun,                                  x => $"{NounFormTe(x)}");
            ConjugationsByWordClass.Add(WordClass.JapaneseNoun, Noun);

            AdjectiveNa = new Func<string, string>[2, 2, 2];
            Set(AdjectiveNa,     Tense.Present,  Polarity.Affirmative,   Formality.Long,    x => $"{x}です");
            Set(AdjectiveNa,     Tense.Present,  Polarity.Affirmative,   Formality.Short,   x => $"{AdjectiveForm(x, false)}だ");
            Set(AdjectiveNa,     Tense.Present,  Polarity.Negative,      Formality.Long,    x => $"{AdjectiveForm(x, false)}じゃないです");
            Set(AdjectiveNa,     Tense.Present,  Polarity.Negative,      Formality.Short,   x => $"{AdjectiveForm(x, false)}じゃない");
            Set(AdjectiveNa,     Tense.Past,     Polarity.Affirmative,   Formality.Long,    x => $"{AdjectiveForm(x, false)}でした");
            Set(AdjectiveNa,     Tense.Past,     Polarity.Affirmative,   Formality.Short,   x => $"{AdjectiveForm(x, false)}だった");
            Set(AdjectiveNa,     Tense.Past,     Polarity.Negative,      Formality.Long,    x => $"{AdjectiveForm(x, false)}じゃなかったです");
            Set(AdjectiveNa,     Tense.Past,     Polarity.Negative,      Formality.Short,   x => $"{AdjectiveForm(x, false)}じゃなかった");
            StemsByWordClass.Add(WordClass.JapaneseAdjectiveNa,                             x => $"TODO: {x}");
            TeFormsByWordClass.Add(WordClass.JapaneseAdjectiveNa,                           x => $"{AdjectiveNaFormTe(x)}");
            ConjugationsByWordClass.Add(WordClass.JapaneseAdjectiveNa, AdjectiveNa);

            AdjectiveI = new Func<string, string>[2, 2, 2];
            Set(AdjectiveI,     Tense.Present,  Polarity.Affirmative,   Formality.Long,     x => $"{x}です");
            Set(AdjectiveI,     Tense.Present,  Polarity.Affirmative,   Formality.Short,    x => $"{x}");
            Set(AdjectiveI,     Tense.Present,  Polarity.Negative,      Formality.Long,     x => $"{AdjectiveForm(x, true)}くないです");
            Set(AdjectiveI,     Tense.Present,  Polarity.Negative,      Formality.Short,    x => $"{AdjectiveForm(x, true)}くない");
            Set(AdjectiveI,     Tense.Past,     Polarity.Affirmative,   Formality.Long,     x => $"{AdjectiveForm(x, true)}かったです");
            Set(AdjectiveI,     Tense.Past,     Polarity.Affirmative,   Formality.Short,    x => $"{AdjectiveForm(x, true)}かった");
            Set(AdjectiveI,     Tense.Past,     Polarity.Negative,      Formality.Long,     x => $"{AdjectiveForm(x, true)}くなかったです");
            Set(AdjectiveI,     Tense.Past,     Polarity.Negative,      Formality.Short,    x => $"{AdjectiveForm(x, true)}くなかった");
            StemsByWordClass.Add(WordClass.JapaneseAdjectiveI,                              x => $"TODO: {x}");
            TeFormsByWordClass.Add(WordClass.JapaneseAdjectiveI,                            x => $"{AdjectiveIFormTe(x)}");
            ConjugationsByWordClass.Add(WordClass.JapaneseAdjectiveI, AdjectiveI);

            VerbRu = new Func<string, string>[2, 2, 2];
            Set(VerbRu,          Tense.Present,  Polarity.Affirmative,   Formality.Long,    x => $"{VerbIchidanFormI(x)}ます");
            Set(VerbRu,          Tense.Present,  Polarity.Affirmative,   Formality.Short,   x => $"{x}");
            Set(VerbRu,          Tense.Present,  Polarity.Negative,      Formality.Long,    x => $"{VerbIchidanFormI(x)}ません");
            Set(VerbRu,          Tense.Present,  Polarity.Negative,      Formality.Short,   x => $"{VerbIchidanFormI(x)}ない");
            Set(VerbRu,          Tense.Past,     Polarity.Affirmative,   Formality.Long,    x => $"{VerbIchidanFormI(x)}ました");
            Set(VerbRu,          Tense.Past,     Polarity.Affirmative,   Formality.Short,   x => $"{VerbIchidanFormTa(x)}");
            Set(VerbRu,          Tense.Past,     Polarity.Negative,      Formality.Long,    x => $"{VerbIchidanFormI(x)}ませんでした");
            Set(VerbRu,          Tense.Past,     Polarity.Negative,      Formality.Short,   x => $"{VerbIchidanFormI(x)}なかった");
            StemsByWordClass.Add(WordClass.JapaneseVerbIchidan,                             x => $"TODO: {x}");
            TeFormsByWordClass.Add(WordClass.JapaneseVerbIchidan,                           x => $"{VerbIchidanFormTe(x)}");
            ConjugationsByWordClass.Add(WordClass.JapaneseVerbIchidan, VerbRu);

            VerbU = new Func<string, string>[2, 2, 2];
            Set(VerbU,          Tense.Present,  Polarity.Affirmative,   Formality.Long,     x => $"{VerbGodanFormI(x)}ます");
            Set(VerbU,          Tense.Present,  Polarity.Affirmative,   Formality.Short,    x => $"{x}");
            Set(VerbU,          Tense.Present,  Polarity.Negative,      Formality.Long,     x => $"{VerbGodanFormI(x)}ません");
            Set(VerbU,          Tense.Present,  Polarity.Negative,      Formality.Short,    x => $"{VerbGodanFormA(x)}ない");
            Set(VerbU,          Tense.Past,     Polarity.Affirmative,   Formality.Long,     x => $"{VerbGodanFormI(x)}ました");
            Set(VerbU,          Tense.Past,     Polarity.Affirmative,   Formality.Short,    x => $"{VerbGodanFormTa(x)}");
            Set(VerbU,          Tense.Past,     Polarity.Negative,      Formality.Long,     x => $"{VerbGodanFormI(x)}ませんでした");
            Set(VerbU,          Tense.Past,     Polarity.Negative,      Formality.Short,    x => $"{VerbGodanFormA(x)}なかった");
            StemsByWordClass.Add(WordClass.JapaneseVerbGodan,                               x => $"TODO: {x}");
            TeFormsByWordClass.Add(WordClass.JapaneseVerbGodan,                             x => $"{VerbGodanFormTe(x)}");
            ConjugationsByWordClass.Add(WordClass.JapaneseVerbGodan, VerbU);
        }

        public static string Get(string japanese, WordClass wordClass, Tense tense, Polarity polarity, Formality formality)
        {
            return ConjugationsByWordClass[wordClass][(int)tense - 1, (int)polarity - 1, (int)formality - 1](japanese);
        }

        public static string GetStem(string japanese, WordClass wordClass)
        {
            return StemsByWordClass[wordClass](japanese);
        }

        public static string GetTe(string japanese, WordClass wordClass)
        {
            return TeFormsByWordClass[wordClass](japanese);
        }

        private static void Set(Func<string, string>[,,] conjugationMatrix, Tense tense, Polarity polarity, Formality formality, Func<string, string> conjugationFunction)
        {
            conjugationMatrix[(int)tense - 1, (int)polarity - 1, (int)formality - 1] = conjugationFunction;
        }

        private static string NounFormTe(string dictionaryForm) => dictionaryForm + "で";
        private static string AdjectiveNaFormTe(string dictionaryForm) => dictionaryForm + "で";
        private static string AdjectiveIFormTe(string dictionaryForm) => dictionaryForm.Remove(dictionaryForm.Length - 1) + "くて";

        private static string VerbIchidanFormI(string dictionaryForm) => dictionaryForm.Remove(dictionaryForm.Length - 1);
        private static string VerbIchidanFormTe(string dictionaryForm) => VerbIchidanFormI(dictionaryForm) + "て";
        private static string VerbIchidanFormTa(string dictionaryForm) => ConvertToFormTa(VerbIchidanFormTe(dictionaryForm));

        private static readonly Dictionary<string, string> GodanReplacementsI =
            new Dictionary<string, string>
            {
                { "う", "い" },
                { "つ", "ち" },
                { "る", "り" },
                { "む", "み" },
                { "ぶ", "び" },
                { "ぬ", "に" },
                { "く", "き" },
                { "ぐ", "ぎ" },
                { "す", "し" }
            };

        private static readonly Dictionary<string, string> GodanReplacementsA =
            new Dictionary<string, string>
            {
                { "う", "わ" },
                { "つ", "た" },
                { "る", "ら" },
                { "む", "ま" },
                { "ぶ", "ば" },
                { "ぬ", "な" },
                { "く", "か" },
                { "ぐ", "が" },
                { "す", "さ" }
            };

        private static readonly Dictionary<string, string> GodanReplacementsTe =
            new Dictionary<string, string>
            {
                { "う", "って" },
                { "つ", "って" },
                { "る", "って" },
                { "む", "んで" },
                { "ぶ", "んで" },
                { "ぬ", "んで" },
                { "く", "いて" },
                { "ぐ", "いで" },
                { "す", "して" }
            };

        private static string VerbGodanFormI(string dictionaryForm) => VerbGodanForm(dictionaryForm, GodanReplacementsI);
        private static string VerbGodanFormA(string dictionaryForm) => VerbGodanForm(dictionaryForm, GodanReplacementsA);
        private static string VerbGodanFormTe(string dictionaryForm) => VerbGodanForm(dictionaryForm, GodanReplacementsTe);
        private static string VerbGodanFormTa(string dictionaryForm) => ConvertToFormTa(VerbGodanFormTe(dictionaryForm));

        private static string ConvertToFormTa(string teForm)
        {
            char replacementCharacter;

            var lastCharacter = teForm.Last();
            switch (lastCharacter)
            {
                case 'て':
                    replacementCharacter = 'た';
                    break;
                case 'で':
                    replacementCharacter = 'だ';
                    break;
                default:
                    throw new InvalidOperationException($"Cannot convert {teForm} to ta-form because it does not end in て or で");
            }

            return teForm.Remove(teForm.Length - 1) + replacementCharacter;
        }

        private static string VerbGodanForm(string dictionaryForm, Dictionary<string, string> godanReplacements)
        {
            var godanSuffix = godanReplacements[Convert.ToString(dictionaryForm.Last())];
            var baseForm = dictionaryForm.Remove(dictionaryForm.Length - 1);
            return baseForm + godanSuffix;
        }

        // NOTE: the japanese dictionary behind this application does not include "na" in the dictionary form of na-adjectives, 
        // so no need to remove anything
        private static string AdjectiveForm(string dictionaryForm, bool isAdjectiveI)
        {
            return isAdjectiveI ? dictionaryForm.Remove(dictionaryForm.Length - 1) : dictionaryForm;
        }
    }
}
