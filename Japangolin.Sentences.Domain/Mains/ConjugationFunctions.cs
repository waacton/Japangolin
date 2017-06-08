namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public static class ConjugationFunctions
    {
        public static readonly Dictionary<Conjugation, Func<string, string>> EnglishTopicPrepositions =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => "???" },
                { Conjugation.LongPresentAffirmative, s => "is" },
                { Conjugation.LongPresentNegative, s => "is not" },
                { Conjugation.LongPastAffirmative, s => "was" },
                { Conjugation.LongPastNegative, s => "was not" },
                { Conjugation.LongFutureAffirmative, s => "will be" },
                { Conjugation.LongFutureNegative, s => "will not be" },
                { Conjugation.ShortPresentAffirmative, s => $"{EnglishTopicPrepositions[Conjugation.LongPresentAffirmative](s)}" },
                { Conjugation.ShortPresentNegative, s => $"{EnglishTopicPrepositions[Conjugation.LongPresentNegative](s)}" },
                { Conjugation.ShortPastAffirmative, s => $"{EnglishTopicPrepositions[Conjugation.LongPastAffirmative](s)}" },
                { Conjugation.ShortPastNegative, s => $"{EnglishTopicPrepositions[Conjugation.LongPastNegative](s)}" },
                { Conjugation.ShortFutureAffirmative, s => $"{EnglishTopicPrepositions[Conjugation.LongFutureAffirmative](s)}" },
                { Conjugation.ShortFutureNegative, s => $"{EnglishTopicPrepositions[Conjugation.LongFutureNegative](s)}" }
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseNoun =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s}です" }, // add desu
                { Conjugation.LongPresentNegative, s => $"{s}じゃないです" }, // add janaidesu (ja arimasen)
                { Conjugation.LongPastAffirmative, s => $"{s}でした" }, // add deshita
                { Conjugation.LongPastNegative, s => $"{s}じゃなかったです" }, // add janakattadesu
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseNoun[Conjugation.LongPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseNoun[Conjugation.LongPresentNegative](s)}？？？" }, // same as present ???
                { Conjugation.ShortPresentAffirmative, s => $"{s}だ" }, // long desu ~> da
                { Conjugation.ShortPresentNegative, s => $"{s}じゃない" }, // long janaidesu ~> janai (drop desu)
                { Conjugation.ShortPastAffirmative, s => $"{s}だった" }, // long deshita ~> datta
                { Conjugation.ShortPastNegative, s => $"{s}じゃなかった" }, // long janakattadesu ~> janakatta (drop desu)
                { Conjugation.ShortFutureAffirmative, s => $"{JapaneseNoun[Conjugation.ShortPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{JapaneseNoun[Conjugation.ShortPresentNegative](s)}？？？" } // same as present ???
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseVerbRu =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s.Remove(s.Length - 1)}ます" }, // drop ru, add masu
                { Conjugation.LongPresentNegative, s => $"{s.Remove(s.Length - 1)}ません" }, // drop ru, add masen
                { Conjugation.LongPastAffirmative, s => $"{s.Remove(s.Length - 1)}ました" }, // drop ru, add mashita
                { Conjugation.LongPastNegative, s => $"{s.Remove(s.Length - 1)}ませんでした" }, // drop ru, add masendeshita
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseVerbRu[Conjugation.LongPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseVerbRu[Conjugation.LongPresentNegative](s)}？？？" }, // same as present ???
                { Conjugation.ShortPresentAffirmative, s => $"{s}" }, // same as dictionary
                { Conjugation.ShortPresentNegative, s => $"{s.Remove(s.Length - 1)}ない" }, // drop ru, add nai
                { Conjugation.ShortPastAffirmative, s => $"{s.Remove(s.Length - 1)}た" }, // convert to te-form but te becomes ta
                { Conjugation.ShortPastNegative, s => $"{s.Remove(s.Length - 1)}なかった" }, // short present nai ~> nakatta
                { Conjugation.ShortFutureAffirmative, s => $"{JapaneseVerbRu[Conjugation.ShortPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{JapaneseVerbRu[Conjugation.ShortPresentNegative](s)}？？？" } // same as present ???
            };

        // TODO: handle the 5 basic types of u-verb + te-form conjugations.  messy?
        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseVerbU =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s}-IMASU-U" },
                { Conjugation.LongPresentNegative, s => $"{s}-IMASEN-U" },
                { Conjugation.LongPastAffirmative, s => $"{s}-IMASHITA-U" },
                { Conjugation.LongPastNegative, s => $"{s}-IMASENDESHITA-U" },
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseVerbU[Conjugation.LongPresentAffirmative](s)} ？？？" }, // same as present ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseVerbU[Conjugation.LongPresentNegative](s)} ？？？" }, // same as present ???
                { Conjugation.ShortPresentAffirmative, s => $"{s}-IMASU-U" },
                { Conjugation.ShortPresentNegative, s => $"{s}-IMASEN-U" },
                { Conjugation.ShortPastAffirmative, s => $"{s}-IMASHITA-U" },
                { Conjugation.ShortPastNegative, s => $"{s}-IMASENDESHITA-U" },
                { Conjugation.ShortFutureAffirmative,s => $"{JapaneseVerbU[Conjugation.ShortPresentAffirmative](s)} ？？？" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{JapaneseVerbU[Conjugation.ShortPresentNegative](s)} ？？？" } // same as present ???
            };

        // TODO: make this better?
        public static readonly Dictionary<Conjugation, Func<string, string>> EnglishVerb =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s}-ing" },
                { Conjugation.LongPresentNegative, s => $"{s}-ing" },
                { Conjugation.LongPastAffirmative, s => $"{s}-ed" },
                { Conjugation.LongPastNegative, s => $"{s}-ed" },
                { Conjugation.LongFutureAffirmative, s => $"{s}" },
                { Conjugation.LongFutureNegative, s => $"{s}" },
                { Conjugation.ShortPresentAffirmative, s => $"{EnglishVerb[Conjugation.LongPresentAffirmative](s)}" },
                { Conjugation.ShortPresentNegative, s => $"{EnglishVerb[Conjugation.LongPresentNegative](s)}" },
                { Conjugation.ShortPastAffirmative, s => $"{EnglishVerb[Conjugation.LongPastAffirmative](s)}" },
                { Conjugation.ShortPastNegative, s => $"{EnglishVerb[Conjugation.LongPastNegative](s)}" },
                { Conjugation.ShortFutureAffirmative, s => $"{EnglishVerb[Conjugation.LongFutureAffirmative](s)}" },
                { Conjugation.ShortFutureNegative, s => $"{EnglishVerb[Conjugation.LongFutureNegative](s)}" }
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseAdjectiveI =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s}です" }, // add desu
                { Conjugation.LongPresentNegative, s => $"{s.Remove(s.Length - 1)}くないです" }, // drop i, add kunaidesu
                { Conjugation.LongPastAffirmative, s => $"{s.Remove(s.Length - 1)}かったです" }, // drop i, add kattadesu
                { Conjugation.LongPastNegative, s => $"{s.Remove(s.Length - 1)}くなかったです" }, // drop i, add kunakattadesu
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseAdjectiveI[Conjugation.LongPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseAdjectiveI[Conjugation.LongPresentNegative](s)}？？？" }, // same as present ???
                { Conjugation.ShortPresentAffirmative, s => $"{s}" }, // same as dictionary
                { Conjugation.ShortPresentNegative, s => $"{s.Remove(s.Length - 1)}くない" }, // long kundaidesu ~> kunai
                { Conjugation.ShortPastAffirmative, s => $"{s.Remove(s.Length - 1)}かった" }, // long kattadesu ~> katta
                { Conjugation.ShortPastNegative, s => $"{s.Remove(s.Length - 1)}くなかった" }, // long kunakattadesu ~> kunakatta
                { Conjugation.ShortFutureAffirmative, s => $"{JapaneseAdjectiveI[Conjugation.ShortPresentAffirmative](s)}？？？" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{JapaneseAdjectiveI[Conjugation.ShortPresentNegative](s)}？？？" } // same as present ???
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseAdjectiveNa =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{JapaneseNoun[Conjugation.LongPresentAffirmative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun
                { Conjugation.LongPresentNegative, s => $"{JapaneseNoun[Conjugation.LongPresentNegative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun
                { Conjugation.LongPastAffirmative, s => $"{JapaneseNoun[Conjugation.LongPastAffirmative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun
                { Conjugation.LongPastNegative, s => $"{JapaneseNoun[Conjugation.LongPastNegative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun
                { Conjugation.LongFutureAffirmative, s => $"{JapaneseNoun[Conjugation.LongFutureAffirmative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun ???
                { Conjugation.LongFutureNegative, s => $"{JapaneseNoun[Conjugation.LongFutureNegative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun ???
                { Conjugation.ShortPresentAffirmative, s => $"{JapaneseNoun[Conjugation.ShortPresentAffirmative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun
                { Conjugation.ShortPresentNegative, s => $"{JapaneseNoun[Conjugation.ShortPresentNegative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun
                { Conjugation.ShortPastAffirmative, s => $"{JapaneseNoun[Conjugation.ShortPastAffirmative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun
                { Conjugation.ShortPastNegative, s => $"{JapaneseNoun[Conjugation.ShortPastNegative](s.Remove(s.Length - 1))}" }, // drop na, conjugate as noun
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
    }
}
