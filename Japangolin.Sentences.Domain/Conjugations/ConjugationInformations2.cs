namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // TODO: review naming and structure
    public class ConjugationInformations2
    {
        /* some useful stuff can be found at https://en.wikipedia.org/wiki/Japanese_verb_conjugation */

        public static readonly string[,,] Noun;
        public static readonly string[,,] AdjectiveNa;
        public static readonly string[,,] AdjectiveI;
        public static readonly string[,,] VerbRu;
        public static readonly string[,,] VerbU;
        public static readonly Dictionary<WordClass, string[,,]> ConjugationsByWordClass;

        public static readonly Dictionary<WordClass, string> StemsByWordClass;
        public static readonly Dictionary<WordClass, string> TeFormsByWordClass;

        static ConjugationInformations2()
        {
            ConjugationsByWordClass = new Dictionary<WordClass, string[,,]>();
            StemsByWordClass = new Dictionary<WordClass, string>();
            TeFormsByWordClass = new Dictionary<WordClass, string>();

            Noun = new string[2, 2, 2];
            Set(Noun, Tense.Present, Polarity.Affirmative, Formality.Long, $"＋です");
            Set(Noun, Tense.Present, Polarity.Affirmative, Formality.Short, $"＋だ");
            Set(Noun, Tense.Present, Polarity.Negative, Formality.Long, $"＋じゃないです");
            Set(Noun, Tense.Present, Polarity.Negative, Formality.Short, $"＋じゃない");
            Set(Noun, Tense.Past, Polarity.Affirmative, Formality.Long, $"＋でした");
            Set(Noun, Tense.Past, Polarity.Affirmative, Formality.Short, $"＋だった");
            Set(Noun, Tense.Past, Polarity.Negative, Formality.Long, $"＋じゃなかったです");
            Set(Noun, Tense.Past, Polarity.Negative, Formality.Short, $"＋じゃなかった");
            StemsByWordClass.Add(WordClass.JapaneseNoun, $"｛dict｝");
            TeFormsByWordClass.Add(WordClass.JapaneseNoun, $"{NounFormTe()}");
            ConjugationsByWordClass.Add(WordClass.JapaneseNoun, Noun);

            AdjectiveNa = new string[2, 2, 2];
            Set(AdjectiveNa, Tense.Present, Polarity.Affirmative, Formality.Long, $"＋です");
            Set(AdjectiveNa, Tense.Present, Polarity.Affirmative, Formality.Short, $"{AdjectiveForm(false)}　＋だ");
            Set(AdjectiveNa, Tense.Present, Polarity.Negative, Formality.Long, $"{AdjectiveForm(false)}　＋じゃないです");
            Set(AdjectiveNa, Tense.Present, Polarity.Negative, Formality.Short, $"{AdjectiveForm(false)}　＋じゃない");
            Set(AdjectiveNa, Tense.Past, Polarity.Affirmative, Formality.Long, $"{AdjectiveForm(false)}　＋でした");
            Set(AdjectiveNa, Tense.Past, Polarity.Affirmative, Formality.Short, $"{AdjectiveForm(false)}　＋だった");
            Set(AdjectiveNa, Tense.Past, Polarity.Negative, Formality.Long, $"{AdjectiveForm(false)}　＋じゃなかったです");
            Set(AdjectiveNa, Tense.Past, Polarity.Negative, Formality.Short, $"{AdjectiveForm(false)}　＋じゃなかった");
            StemsByWordClass.Add(WordClass.JapaneseAdjectiveNa, $"{AdjectiveForm(false)}");
            TeFormsByWordClass.Add(WordClass.JapaneseAdjectiveNa, $"{AdjectiveNaFormTe()}");
            ConjugationsByWordClass.Add(WordClass.JapaneseAdjectiveNa, AdjectiveNa);

            AdjectiveI = new string[2, 2, 2];
            Set(AdjectiveI, Tense.Present, Polarity.Affirmative, Formality.Long, $"＋です");
            Set(AdjectiveI, Tense.Present, Polarity.Affirmative, Formality.Short, $"＋だ");
            Set(AdjectiveI, Tense.Present, Polarity.Negative, Formality.Long, $"{AdjectiveForm(true)}　＋くないです");
            Set(AdjectiveI, Tense.Present, Polarity.Negative, Formality.Short, $"{AdjectiveForm(true)}　＋くない");
            Set(AdjectiveI, Tense.Past, Polarity.Affirmative, Formality.Long, $"{AdjectiveForm(true)}　＋かったです");
            Set(AdjectiveI, Tense.Past, Polarity.Affirmative, Formality.Short, $"{AdjectiveForm(true)}　＋かった");
            Set(AdjectiveI, Tense.Past, Polarity.Negative, Formality.Long, $"{AdjectiveForm(true)}　＋くなかったです");
            Set(AdjectiveI, Tense.Past, Polarity.Negative, Formality.Short, $"{AdjectiveForm(true)}　＋くなかった");
            StemsByWordClass.Add(WordClass.JapaneseAdjectiveI, $"{AdjectiveForm(true)}");
            TeFormsByWordClass.Add(WordClass.JapaneseAdjectiveI, $"{AdjectiveIFormTe()}");
            ConjugationsByWordClass.Add(WordClass.JapaneseAdjectiveI, AdjectiveI);

            VerbRu = new string[2, 2, 2];
            Set(VerbRu, Tense.Present, Polarity.Affirmative, Formality.Long, $"{VerbIchidanFormI()}　＋ます");
            Set(VerbRu, Tense.Present, Polarity.Affirmative, Formality.Short, $"｛dict｝");
            Set(VerbRu, Tense.Present, Polarity.Negative, Formality.Long, $"{VerbIchidanFormI()}　＋ません");
            Set(VerbRu, Tense.Present, Polarity.Negative, Formality.Short, $"{VerbIchidanFormI()}　＋ない");
            Set(VerbRu, Tense.Past, Polarity.Affirmative, Formality.Long, $"{VerbIchidanFormI()}　＋ました");
            Set(VerbRu, Tense.Past, Polarity.Affirmative, Formality.Short, $"{VerbIchidanFormTa()}");
            Set(VerbRu, Tense.Past, Polarity.Negative, Formality.Long, $"{VerbIchidanFormI()}　＋ませんでした");
            Set(VerbRu, Tense.Past, Polarity.Negative, Formality.Short, $"{VerbIchidanFormI()}　＋なかった");
            StemsByWordClass.Add(WordClass.JapaneseVerbIchidan, $"{VerbIchidanFormI()}");
            TeFormsByWordClass.Add(WordClass.JapaneseVerbIchidan, $"{VerbIchidanFormTe()}");
            ConjugationsByWordClass.Add(WordClass.JapaneseVerbIchidan, VerbRu);

            VerbU = new string[2, 2, 2];
            Set(VerbU, Tense.Present, Polarity.Affirmative, Formality.Long, $"{VerbGodanFormI()}　＋ます");
            Set(VerbU, Tense.Present, Polarity.Affirmative, Formality.Short, $"｛dict｝");
            Set(VerbU, Tense.Present, Polarity.Negative, Formality.Long, $"{VerbGodanFormI()}　＋ません");
            Set(VerbU, Tense.Present, Polarity.Negative, Formality.Short, $"{VerbGodanFormA()}　＋ない");
            Set(VerbU, Tense.Past, Polarity.Affirmative, Formality.Long, $"{VerbGodanFormI()}　＋ました");
            Set(VerbU, Tense.Past, Polarity.Affirmative, Formality.Short, $"{VerbGodanFormTa()}");
            Set(VerbU, Tense.Past, Polarity.Negative, Formality.Long, $"{VerbGodanFormI()}　＋ませんでした");
            Set(VerbU, Tense.Past, Polarity.Negative, Formality.Short, $"{VerbGodanFormA()}なかった");
            StemsByWordClass.Add(WordClass.JapaneseVerbGodan, $"{VerbGodanFormI()}");
            TeFormsByWordClass.Add(WordClass.JapaneseVerbGodan, $"{VerbGodanFormTe()}");
            ConjugationsByWordClass.Add(WordClass.JapaneseVerbGodan, VerbU);
        }

        public static string Get(WordClass wordClass, Tense tense, Polarity polarity, Formality formality)
        {
            return ConjugationsByWordClass[wordClass][(int)tense - 1, (int)polarity - 1, (int)formality - 1];
        }

        public static string GetStem(WordClass wordClass)
        {
            return StemsByWordClass[wordClass];
        }

        public static string GetTe(WordClass wordClass)
        {
            return TeFormsByWordClass[wordClass];
        }

        private static void Set(string[,,] conjugationMatrix, Tense tense, Polarity polarity, Formality formality, string conjugationInformation)
        {
            conjugationMatrix[(int)tense - 1, (int)polarity - 1, (int)formality - 1] = conjugationInformation;
        }

        private static string NounFormTe() => "＋で";
        private static string AdjectiveNaFormTe() => "＋で";
        private static string AdjectiveIFormTe() => "ーい　＋くて";

        private static string VerbIchidanFormI() => "ーる";
        private static string VerbIchidanFormTe() => "｛～て｝";
        private static string VerbIchidanFormTa() => ConvertToFormTa(VerbIchidanFormTe());

        private static string VerbGodanFormI() => "《う→い》";
        private static string VerbGodanFormA() => "《う→あ》";
        private static string VerbGodanFormTe() => "｛～て｝";
        private static string VerbGodanFormTa() => ConvertToFormTa(VerbGodanFormTe());

        private static string ConvertToFormTa(string teForm) => teForm.Replace('て', 'た').Replace('で', 'だ');

        private static string AdjectiveForm(bool isAdjectiveI) => isAdjectiveI ? "ーい" : "ーな";
    }
}
