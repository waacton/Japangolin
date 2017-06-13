namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Tovarisch.Enum;

    public static class ConjugationFunctions
    {
        /* some useful stuff can be found at https://en.wikipedia.org/wiki/Japanese_verb_conjugation */

        public static readonly Dictionary<Conjugation, Func<string, string>> EnglishTopicPrepositionsWithoutVerb =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => "???" },
                { Conjugation.LongPresentAffirmative, s => "is" },
                { Conjugation.LongPresentNegative, s => "is not" },
                { Conjugation.LongPastAffirmative, s => "was" },
                { Conjugation.LongPastNegative, s => "was not" },
                { Conjugation.LongFutureAffirmative, s => "will be" },
                { Conjugation.LongFutureNegative, s => "will not be" },
                { Conjugation.ShortPresentAffirmative, s => $"{EnglishTopicPrepositionsWithoutVerb[Conjugation.LongPresentAffirmative](s)}" },
                { Conjugation.ShortPresentNegative, s => $"{EnglishTopicPrepositionsWithoutVerb[Conjugation.LongPresentNegative](s)}" },
                { Conjugation.ShortPastAffirmative, s => $"{EnglishTopicPrepositionsWithoutVerb[Conjugation.LongPastAffirmative](s)}" },
                { Conjugation.ShortPastNegative, s => $"{EnglishTopicPrepositionsWithoutVerb[Conjugation.LongPastNegative](s)}" },
                { Conjugation.ShortFutureAffirmative, s => $"{EnglishTopicPrepositionsWithoutVerb[Conjugation.LongFutureAffirmative](s)}" },
                { Conjugation.ShortFutureNegative, s => $"{EnglishTopicPrepositionsWithoutVerb[Conjugation.LongFutureNegative](s)}" }
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> EnglishTopicPrepositionsWithVerb =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => "???" },
                { Conjugation.LongPresentAffirmative, s => "does" },
                { Conjugation.LongPresentNegative, s => "does not" },
                { Conjugation.LongPastAffirmative, s => "did" },
                { Conjugation.LongPastNegative, s => "did not" },
                { Conjugation.LongFutureAffirmative, s => "will" },
                { Conjugation.LongFutureNegative, s => "will not" },
                { Conjugation.ShortPresentAffirmative, s => $"{EnglishTopicPrepositionsWithVerb[Conjugation.LongPresentAffirmative](s)}" },
                { Conjugation.ShortPresentNegative, s => $"{EnglishTopicPrepositionsWithVerb[Conjugation.LongPresentNegative](s)}" },
                { Conjugation.ShortPastAffirmative, s => $"{EnglishTopicPrepositionsWithVerb[Conjugation.LongPastAffirmative](s)}" },
                { Conjugation.ShortPastNegative, s => $"{EnglishTopicPrepositionsWithVerb[Conjugation.LongPastNegative](s)}" },
                { Conjugation.ShortFutureAffirmative, s => $"{EnglishTopicPrepositionsWithVerb[Conjugation.LongFutureAffirmative](s)}" },
                { Conjugation.ShortFutureNegative, s => $"{EnglishTopicPrepositionsWithVerb[Conjugation.LongFutureNegative](s)}" }
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseNoun =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s}です" }, // add desu
                { Conjugation.LongPresentNegative, s => $"{s}じゃないです" }, // add janaidesu (colloquial of ja arimasen)
                { Conjugation.LongPastAffirmative, s => $"{s}でした" }, // add deshita
                { Conjugation.LongPastNegative, s => $"{s}じゃなかったです" }, // add janakattadesu
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseNoun[Conjugation.LongPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseNoun[Conjugation.LongPresentNegative](s)}？？？" }, // same as present ???
                { Conjugation.ShortPresentAffirmative, s => $"{s}だ" }, // add da [long: desu ~> short: da]
                { Conjugation.ShortPresentNegative, s => $"{s}じゃない" }, // add janai [long: janaidesu ~> short: janai (drop desu)]
                { Conjugation.ShortPastAffirmative, s => $"{s}だった" }, // add datta [long: deshita ~> short: datta]
                { Conjugation.ShortPastNegative, s => $"{s}じゃなかった" }, // add janakatta [long: janakattadesu ~> short: janakatta (drop desu)]
                { Conjugation.ShortFutureAffirmative, s => $"{JapaneseNoun[Conjugation.ShortPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{JapaneseNoun[Conjugation.ShortPresentNegative](s)}？？？" } // same as present ???
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseVerbIchidan =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{VerbIchidanFormI(s)}ます" }, // drop ru, add masu
                { Conjugation.LongPresentNegative, s => $"{VerbIchidanFormI(s)}ません" }, // drop ru, add masen
                { Conjugation.LongPastAffirmative, s => $"{VerbIchidanFormI(s)}ました" }, // drop ru, add mashita
                { Conjugation.LongPastNegative, s => $"{VerbIchidanFormI(s)}ませんでした" }, // drop ru, add masendeshita
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseVerbIchidan[Conjugation.LongPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseVerbIchidan[Conjugation.LongPresentNegative](s)}？？？" }, // same as present ???
                { Conjugation.ShortPresentAffirmative, s => $"{s}" }, // dictionary form
                { Conjugation.ShortPresentNegative, s => $"{VerbIchidanFormI(s)}ない" }, // drop ru, add nai
                { Conjugation.ShortPastAffirmative, s => $"{VerbIchidanFormTa(s)}" }, // te-form, te becomes ta
                { Conjugation.ShortPastNegative, s => $"{VerbIchidanFormI(s)}なかった" }, // drop ru, add nakatta [short-present: nai ~> short-past: nakatta]
                { Conjugation.ShortFutureAffirmative, s => $"{JapaneseVerbIchidan[Conjugation.ShortPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{JapaneseVerbIchidan[Conjugation.ShortPresentNegative](s)}？？？" } // same as present ???
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseVerbGodan =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{VerbGodanFormI(s)}ます" }, // ~u becomes ~i, add masu
                { Conjugation.LongPresentNegative, s => $"{VerbGodanFormI(s)}ません" }, // ~u becomes ~i, add masen
                { Conjugation.LongPastAffirmative, s => $"{VerbGodanFormI(s)}ました" }, // ~u becomes ~i, add mashita
                { Conjugation.LongPastNegative, s => $"{VerbGodanFormI(s)}ませんでした" }, // ~u becomes ~i, add masendeshita
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseVerbGodan[Conjugation.LongPresentAffirmative](s)} ？？？" }, // same as present ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseVerbGodan[Conjugation.LongPresentNegative](s)} ？？？" }, // same as present ???
                { Conjugation.ShortPresentAffirmative, s => s }, // dictionary form
                { Conjugation.ShortPresentNegative, s => $"{VerbGodanFormA(s)}ない" }, // ~u becomes ~a, add nai 
                { Conjugation.ShortPastAffirmative, s => $"{VerbGodanFormTa(s)}" }, // te-form, te becomes ta
                { Conjugation.ShortPastNegative, s => $"{VerbGodanFormA(s)}なかった" }, // ~u becomes ~a, add nakatta [short-present: nai ~> short-past: nakatta]
                { Conjugation.ShortFutureAffirmative,s => $"{JapaneseVerbGodan[Conjugation.ShortPresentAffirmative](s)} ？？？" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{JapaneseVerbGodan[Conjugation.ShortPresentNegative](s)} ？？？" } // same as present ???
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseAdjectiveI =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s}です" }, // add desu
                { Conjugation.LongPresentNegative, s => $"{AdjectiveForm(s, true)}くないです" }, // drop i, add kunaidesu
                { Conjugation.LongPastAffirmative, s => $"{AdjectiveForm(s, true)}かったです" }, // drop i, add kattadesu
                { Conjugation.LongPastNegative, s => $"{AdjectiveForm(s, true)}くなかったです" }, // drop i, add kunakattadesu
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseAdjectiveI[Conjugation.LongPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseAdjectiveI[Conjugation.LongPresentNegative](s)}？？？" }, // same as present ???
                { Conjugation.ShortPresentAffirmative, s => $"{s}" }, // dictionary form
                { Conjugation.ShortPresentNegative, s => $"{AdjectiveForm(s, true)}くない" }, // add kunai [long: kunaidesu ~> short: kunai (drop desu)]
                { Conjugation.ShortPastAffirmative, s => $"{AdjectiveForm(s, true)}かった" }, // add katta [long: kattadesu ~> short: katta (drop desu)]
                { Conjugation.ShortPastNegative, s => $"{AdjectiveForm(s, true)}くなかった" }, // add kunakatta [long: kunakattadesu ~> short: kunakatta (drop desu)]
                { Conjugation.ShortFutureAffirmative, s => $"{JapaneseAdjectiveI[Conjugation.ShortPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{JapaneseAdjectiveI[Conjugation.ShortPresentNegative](s)}？？？" } // same as present ???
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseAdjectiveNa =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{JapaneseNoun[Conjugation.LongPresentAffirmative](AdjectiveForm(s, false))}" }, // drop na, conjugate as noun
                { Conjugation.LongPresentNegative, s => $"{JapaneseNoun[Conjugation.LongPresentNegative](AdjectiveForm(s, false))}" }, // drop na, conjugate as noun
                { Conjugation.LongPastAffirmative, s => $"{JapaneseNoun[Conjugation.LongPastAffirmative](AdjectiveForm(s, false))}" }, // drop na, conjugate as noun
                { Conjugation.LongPastNegative, s => $"{JapaneseNoun[Conjugation.LongPastNegative](AdjectiveForm(s, false))}" }, // drop na, conjugate as noun
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseNoun[Conjugation.LongFutureAffirmative](AdjectiveForm(s, false))} ？？？" }, // drop na, conjugate as noun ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseNoun[Conjugation.LongFutureNegative](AdjectiveForm(s, false))} ？？？" }, // drop na, conjugate as noun ???
                { Conjugation.ShortPresentAffirmative, s => $"{JapaneseNoun[Conjugation.ShortPresentAffirmative](AdjectiveForm(s, false))}" }, // drop na, conjugate as noun
                { Conjugation.ShortPresentNegative, s => $"{JapaneseNoun[Conjugation.ShortPresentNegative](AdjectiveForm(s, false))}" }, // drop na, conjugate as noun
                { Conjugation.ShortPastAffirmative, s => $"{JapaneseNoun[Conjugation.ShortPastAffirmative](AdjectiveForm(s, false))}" }, // drop na, conjugate as noun
                { Conjugation.ShortPastNegative, s => $"{JapaneseNoun[Conjugation.ShortPastNegative](AdjectiveForm(s, false))}" }, // drop na, conjugate as noun
                { Conjugation.ShortFutureAffirmative, s => $"{JapaneseAdjectiveNa[Conjugation.ShortPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{JapaneseAdjectiveNa[Conjugation.ShortPresentNegative](s)}？？？" } // same as present ???
            };

        public static Dictionary<Conjugation, Func<string, string>> Defaults => DefaultConjugationFunctions();
        private static Dictionary<Conjugation, Func<string, string>> DefaultConjugationFunctions()
        {
            var funcs = new Dictionary<Conjugation, Func<string, string>>();
            foreach (var conjugation in Enumeration.GetAll<Conjugation>())
            {
                funcs.Add(conjugation, s => s);
            }

            return funcs;
        }

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

        // NOTE: the japanese dictionary behind this application does not include "na" in the dictionary form, so no need to remove anything
        private static string AdjectiveForm(string dictionaryForm, bool isAdjectiveI)
        {
            return isAdjectiveI ? dictionaryForm.Remove(dictionaryForm.Length - 1) : dictionaryForm;
        }
    }
}
