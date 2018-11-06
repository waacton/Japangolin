namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // TODO: review naming and structure
    public class ConjFuncs
    {
        /* some useful stuff can be found at https://en.wikipedia.org/wiki/Japanese_verb_conjugation */

        public static readonly ConjFunc[,,] Noun;
        public static readonly ConjFunc[,,] AdjectiveNa;
        public static readonly ConjFunc[,,] AdjectiveI;
        public static readonly ConjFunc[,,] VerbRu;
        public static readonly ConjFunc[,,] VerbU;
        public static readonly Dictionary<WordClass, ConjFunc[,,]> ConjugationsByWordClass;
        public static readonly Dictionary<WordClass, ConjFunc> DictByWordClass;
        public static readonly Dictionary<WordClass, ConjFunc> StemsByWordClass;
        public static readonly Dictionary<WordClass, ConjFunc> TeFormsByWordClass;

        static ConjFuncs()
        {
            var identityConjFunc = new ConjFunc(x => x, "｛dict｝");

            ConjugationsByWordClass = new Dictionary<WordClass, ConjFunc[,,]>();
            DictByWordClass = new Dictionary<WordClass, ConjFunc>();
            StemsByWordClass = new Dictionary<WordClass, ConjFunc>();
            TeFormsByWordClass = new Dictionary<WordClass, ConjFunc>();

            Noun = new ConjFunc[2, 2, 2];
            Set(Noun, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{x}です", $"＋です");
            Set(Noun, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{x}だ", $"＋だ");
            Set(Noun, Tense.Present, Polarity.Negative, Formality.Long, x => $"{x}じゃないです", $"＋じゃないです");
            Set(Noun, Tense.Present, Polarity.Negative, Formality.Short, x => $"{x}じゃない", $"＋じゃない");
            Set(Noun, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{x}でした", $"＋でした");
            Set(Noun, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{x}だった", $"＋だった");
            Set(Noun, Tense.Past, Polarity.Negative, Formality.Long, x => $"{x}じゃなかったです", $"＋じゃなかったです");
            Set(Noun, Tense.Past, Polarity.Negative, Formality.Short, x => $"{x}じゃなかった", $"＋じゃなかった");
            DictByWordClass.Add(WordClass.JapaneseNoun, identityConjFunc);
            StemsByWordClass.Add(WordClass.JapaneseNoun, new ConjFunc(x => $"{x}", $"｛dict｝"));
            TeFormsByWordClass.Add(WordClass.JapaneseNoun, new ConjFunc(x => $"{NounFormTe(x)}", $"{NounFormTe()}"));
            ConjugationsByWordClass.Add(WordClass.JapaneseNoun, Noun);

            AdjectiveNa = new ConjFunc[2, 2, 2];
            Set(AdjectiveNa, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{x}です", $"＋です");
            Set(AdjectiveNa, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{AdjectiveForm(x, false)}だ", $"{AdjectiveForm(false)}　＋だ");
            Set(AdjectiveNa, Tense.Present, Polarity.Negative, Formality.Long, x => $"{AdjectiveForm(x, false)}じゃないです", $"{AdjectiveForm(false)}　＋じゃないです");
            Set(AdjectiveNa, Tense.Present, Polarity.Negative, Formality.Short, x => $"{AdjectiveForm(x, false)}じゃない", $"{AdjectiveForm(false)}　＋じゃない");
            Set(AdjectiveNa, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{AdjectiveForm(x, false)}でした", $"{AdjectiveForm(false)}　＋でした");
            Set(AdjectiveNa, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{AdjectiveForm(x, false)}だった", $"{AdjectiveForm(false)}　＋だった");
            Set(AdjectiveNa, Tense.Past, Polarity.Negative, Formality.Long, x => $"{AdjectiveForm(x, false)}じゃなかったです", $"{AdjectiveForm(false)}　＋じゃなかったです");
            Set(AdjectiveNa, Tense.Past, Polarity.Negative, Formality.Short, x => $"{AdjectiveForm(x, false)}じゃなかった", $"{AdjectiveForm(false)}　＋じゃなかった");
            DictByWordClass.Add(WordClass.JapaneseAdjectiveNa, identityConjFunc);
            StemsByWordClass.Add(WordClass.JapaneseAdjectiveNa, new ConjFunc(x => $"{AdjectiveForm(x, false)}", $"{AdjectiveForm(false)}"));
            TeFormsByWordClass.Add(WordClass.JapaneseAdjectiveNa, new ConjFunc(x => $"{AdjectiveNaFormTe(x)}", $"{AdjectiveNaFormTe()}"));
            ConjugationsByWordClass.Add(WordClass.JapaneseAdjectiveNa, AdjectiveNa);

            AdjectiveI = new ConjFunc[2, 2, 2];
            Set(AdjectiveI, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{x}です", $"＋です");
            Set(AdjectiveI, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{x}", $"｛dict｝");
            Set(AdjectiveI, Tense.Present, Polarity.Negative, Formality.Long, x => $"{AdjectiveForm(x, true)}くないです", $"{AdjectiveForm(true)}　＋くないです");
            Set(AdjectiveI, Tense.Present, Polarity.Negative, Formality.Short, x => $"{AdjectiveForm(x, true)}くない", $"{AdjectiveForm(true)}　＋くない");
            Set(AdjectiveI, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{AdjectiveForm(x, true)}かったです", $"{AdjectiveForm(true)}　＋かったです");
            Set(AdjectiveI, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{AdjectiveForm(x, true)}かった", $"{AdjectiveForm(true)}　＋かった");
            Set(AdjectiveI, Tense.Past, Polarity.Negative, Formality.Long, x => $"{AdjectiveForm(x, true)}くなかったです", $"{AdjectiveForm(true)}　＋くなかったです");
            Set(AdjectiveI, Tense.Past, Polarity.Negative, Formality.Short, x => $"{AdjectiveForm(x, true)}くなかった", $"{AdjectiveForm(true)}　＋くなかった");
            DictByWordClass.Add(WordClass.JapaneseAdjectiveI, identityConjFunc);
            StemsByWordClass.Add(WordClass.JapaneseAdjectiveI, new ConjFunc(x => $"{AdjectiveForm(x, true)}", $"{AdjectiveForm(true)}"));
            TeFormsByWordClass.Add(WordClass.JapaneseAdjectiveI, new ConjFunc(x => $"{AdjectiveIFormTe(x)}", $"{AdjectiveIFormTe()}"));
            ConjugationsByWordClass.Add(WordClass.JapaneseAdjectiveI, AdjectiveI);

            VerbRu = new ConjFunc[2, 2, 2];
            Set(VerbRu, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{VerbIchidanFormI(x)}ます", $"{VerbIchidanFormI()}　＋ます");
            Set(VerbRu, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{x}", $"｛dict｝");
            Set(VerbRu, Tense.Present, Polarity.Negative, Formality.Long, x => $"{VerbIchidanFormI(x)}ません", $"{VerbIchidanFormI()}　＋ません");
            Set(VerbRu, Tense.Present, Polarity.Negative, Formality.Short, x => $"{VerbIchidanFormI(x)}ない", $"{VerbIchidanFormI()}　＋ない");
            Set(VerbRu, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{VerbIchidanFormI(x)}ました", $"{VerbIchidanFormI()}　＋ました");
            Set(VerbRu, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{VerbIchidanFormTa(x)}", $"{VerbIchidanFormTa()}");
            Set(VerbRu, Tense.Past, Polarity.Negative, Formality.Long, x => $"{VerbIchidanFormI(x)}ませんでした", $"{VerbIchidanFormI()}　＋ませんでした");
            Set(VerbRu, Tense.Past, Polarity.Negative, Formality.Short, x => $"{VerbIchidanFormI(x)}なかった", $"{VerbIchidanFormI()}　＋なかった");
            DictByWordClass.Add(WordClass.JapaneseVerbIchidan, identityConjFunc);
            StemsByWordClass.Add(WordClass.JapaneseVerbIchidan, new ConjFunc(x => $"{VerbIchidanFormI(x)}", $"{VerbIchidanFormI()}"));
            TeFormsByWordClass.Add(WordClass.JapaneseVerbIchidan, new ConjFunc(x => $"{VerbIchidanFormTe(x)}", $"{VerbIchidanFormTe()}"));
            ConjugationsByWordClass.Add(WordClass.JapaneseVerbIchidan, VerbRu);

            VerbU = new ConjFunc[2, 2, 2];
            Set(VerbU, Tense.Present, Polarity.Affirmative, Formality.Long, x => $"{VerbGodanFormI(x)}ます", $"{VerbGodanFormI()}　＋ます");
            Set(VerbU, Tense.Present, Polarity.Affirmative, Formality.Short, x => $"{x}", $"｛dict｝");
            Set(VerbU, Tense.Present, Polarity.Negative, Formality.Long, x => $"{VerbGodanFormI(x)}ません", $"{VerbGodanFormI()}　＋ません");
            Set(VerbU, Tense.Present, Polarity.Negative, Formality.Short, x => $"{VerbGodanFormA(x)}ない", $"{VerbGodanFormA()}　＋ない");
            Set(VerbU, Tense.Past, Polarity.Affirmative, Formality.Long, x => $"{VerbGodanFormI(x)}ました", $"{VerbGodanFormI()}　＋ました");
            Set(VerbU, Tense.Past, Polarity.Affirmative, Formality.Short, x => $"{VerbGodanFormTa(x)}", $"{VerbGodanFormTa()}");
            Set(VerbU, Tense.Past, Polarity.Negative, Formality.Long, x => $"{VerbGodanFormI(x)}ませんでした", $"{VerbGodanFormI()}　＋ませんでした");
            Set(VerbU, Tense.Past, Polarity.Negative, Formality.Short, x => $"{VerbGodanFormA(x)}なかった", $"{VerbGodanFormA()}なかった");
            DictByWordClass.Add(WordClass.JapaneseVerbGodan, identityConjFunc);
            StemsByWordClass.Add(WordClass.JapaneseVerbGodan, new ConjFunc(x => $"{VerbGodanFormI(x)}", $"{VerbGodanFormI()}"));
            TeFormsByWordClass.Add(WordClass.JapaneseVerbGodan, new ConjFunc(x => $"{VerbGodanFormTe(x)}", $"{VerbGodanFormTe()}"));
            ConjugationsByWordClass.Add(WordClass.JapaneseVerbGodan, VerbU);
        }

        public static ConjFunc GetConjFunc(WordClass wordClass, Tense tense, Polarity polarity, Formality formality)
        {
            return ConjugationsByWordClass[wordClass][(int)tense - 1, (int)polarity - 1, (int)formality - 1];
        }

        // TODO: merge into another matrix with enum?
        public static ConjFunc GetDictFunc(WordClass wordClass)
        {
            return DictByWordClass[wordClass];
        }

        public static ConjFunc GetStemFunc(WordClass wordClass)
        {
            return StemsByWordClass[wordClass];
        }

        public static ConjFunc GetTeFunc(WordClass wordClass)
        {
            return TeFormsByWordClass[wordClass];
        }

        private static void Set(ConjFunc[,,] conjugationMatrix, Tense tense, Polarity polarity, Formality formality, 
            Func<string, string> conjugationFunction, string information)
        {
            conjugationMatrix[(int)tense - 1, (int)polarity - 1, (int)formality - 1] = new ConjFunc(conjugationFunction, information);
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


        /* information functions */
        private static string NounFormTe() => "＋で";
        private static string AdjectiveNaFormTe() => "＋で";
        private static string AdjectiveIFormTe() => "ーい　＋くて";

        private static string VerbIchidanFormI() => "ーる";
        private static string VerbIchidanFormTe() => "ーる　＋て｝";
        private static string VerbIchidanFormTa() => ConvertInfoToFormTa(VerbIchidanFormTe());

        private static string VerbGodanFormI() => "《う→い》";
        private static string VerbGodanFormA() => "《う→あ》";
        private static string VerbGodanFormTe() => "ー《う》　＋《て》"; // TODO: need to be more specific about how to handle て-form?
        private static string VerbGodanFormTa() => ConvertInfoToFormTa(VerbGodanFormTe());

        private static string ConvertInfoToFormTa(string teForm) => teForm.Replace('て', 'た').Replace('で', 'だ');

        private static string AdjectiveForm(bool isAdjectiveI) => isAdjectiveI ? "ーい" : "ーな";
    }
}
