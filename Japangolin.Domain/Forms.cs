namespace Wacton.Japangolin.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Forms
    {
        public static string Dict(string dictForm) => dictForm;

        public static string NounStem(string dictForm) => dictForm;
        public static string NounFormTe(string dictForm) => NounStem(dictForm) + "で";

        // NOTE: the japanese dictionary behind this application does not include "な" in the dictionary form of na-adjectives, 
        // so no need to remove anything
        public static string AdjNaStem(string dictForm) => dictForm;
        public static string AdjNaFormTe(string dictForm) => AdjNaStem(dictForm) + "で";

        public static string AdjIStem(string dictForm) => dictForm.Remove(dictForm.Length - 1);
        public static string AdjIFormTe(string dictForm) => AdjIStem(dictForm) + "くて";

        public static string VerbRuStem(string dictForm) => dictForm.Remove(dictForm.Length - 1);
        public static string VerbRuFormTe(string dictForm) => VerbRuStem(dictForm) + "て";
        public static string VerbRuFormTa(string dictForm) => VerbRuStem(dictForm) + "た";

        public static string VerbUStemI(string dictForm) => VerbUForm(dictForm, VerbUReplacementsI);
        public static string VerbUStemA(string dictForm) => VerbUForm(dictForm, VerbUReplacementsA);
        public static string VerbUFormTe(string dictForm) => VerbUForm(dictForm, VerbUReplacementsTe);
        public static string VerbUFormTa(string dictForm) => VerbUForm(dictForm, VerbUReplacementsTa);

        private static readonly Dictionary<string, string> VerbUReplacementsI =
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

        private static readonly Dictionary<string, string> VerbUReplacementsA =
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

        private static readonly Dictionary<string, string> VerbUReplacementsTe =
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

        private static readonly Dictionary<string, string> VerbUReplacementsTa =
            new Dictionary<string, string>
            {
                { "う", "った" },
                { "つ", "った" },
                { "る", "った" },
                { "む", "んだ" },
                { "ぶ", "んだ" },
                { "ぬ", "んだ" },
                { "く", "いた" },
                { "ぐ", "いだ" },
                { "す", "した" }
            };

        private static string VerbUForm(string dictForm, Dictionary<string, string> verbUReplacements)
        {
            var verbBase = dictForm.Remove(dictForm.Length - 1);
            var verbEnding = verbUReplacements[Convert.ToString(dictForm.Last())];
            return verbBase + verbEnding;
        }
    }
}
