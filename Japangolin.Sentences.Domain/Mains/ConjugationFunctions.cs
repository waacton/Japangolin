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
                { Conjugation.ShortPresentAffirmative, s => "is" },
                { Conjugation.ShortPresentNegative, s => "is not" },
                { Conjugation.ShortPastAffirmative, s => "was" },
                { Conjugation.ShortPastNegative, s => "was not" },
                { Conjugation.ShortFutureAffirmative, s => "will be" },
                { Conjugation.ShortFutureNegative, s => "will not be" }
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseNoun =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s}です" },
                { Conjugation.LongPresentNegative, s => $"{s}じゃないです" },
                { Conjugation.LongPastAffirmative, s => $"{s}でした" },
                { Conjugation.LongPastNegative, s => $"{s}じゃなかったです" },
                { Conjugation.LongFutureAffirmative, s => $"{s}です" },
                { Conjugation.LongFutureNegative, s => $"{s}じゃないです" },
                { Conjugation.ShortPresentAffirmative, s => $"{s}だ" },
                { Conjugation.ShortPresentNegative, s => $"{s}じゃない" },
                { Conjugation.ShortPastAffirmative, s => $"{s}だった" },
                { Conjugation.ShortPastNegative, s => $"{s}じゃなかった" },
                { Conjugation.ShortFutureAffirmative, s => $"{s}だ" },
                { Conjugation.ShortFutureNegative, s => $"{s}じゃない" }
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseVerbRu =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s.Remove(s.Length - 1)}ます" }, // drop ru, add masu
                { Conjugation.LongPresentNegative, s => $"{s.Remove(s.Length - 1)}ません" }, // drop ru, add masen
                { Conjugation.LongPastAffirmative, s => $"{s.Remove(s.Length - 1)}ました" }, // drop ru, add mashita (as present, masu -> mashita)
                { Conjugation.LongPastNegative, s => $"{s.Remove(s.Length - 1)}ませんでした" }, // drop ru, add masendeshita (as present, masen -> masen deshita)
                { Conjugation.LongFutureAffirmative, s => $"{s.Remove(s.Length - 1)}ます ???" }, // same as present ???
                { Conjugation.LongFutureNegative, s => $"{s.Remove(s.Length - 1)}ません ???" }, // same as present ???
                { Conjugation.ShortPresentAffirmative, s => $"{s}" }, // same as dictionary
                { Conjugation.ShortPresentNegative, s => $"{s.Remove(s.Length - 1)}ない" }, // drop ru, add nai
                { Conjugation.ShortPastAffirmative, s => $"{s.Remove(s.Length - 1)}た" }, // convert to te-form but te becomes ta
                { Conjugation.ShortPastNegative, s => $"{s.Remove(s.Length - 1)}なかった" }, // // drop ru, add nakatta (as present, nai -> nakatta)
                { Conjugation.ShortFutureAffirmative, s => $"{s} ???" }, // same as present ???
                { Conjugation.ShortFutureNegative, s => $"{s.Remove(s.Length - 1)}ない ???" } // same as present ???
            };

        public static readonly Dictionary<Conjugation, Func<string, string>> JapaneseVerbU =
            new Dictionary<Conjugation, Func<string, string>>
            {
                { Conjugation.None, s => s },
                { Conjugation.LongPresentAffirmative, s => $"{s}-IMASU-U" },
                { Conjugation.LongPresentNegative, s => $"{s}-IMASEN-U" },
                { Conjugation.LongPastAffirmative, s => $"{s}-IMASHITA-U" },
                { Conjugation.LongPastNegative, s => $"{s}-IMASENDESHITA-U" },
                { Conjugation.LongFutureAffirmative, s => $"{s}-IMASU-U" },
                { Conjugation.LongFutureNegative, s => $"{s}-IMASEN-U" },
                { Conjugation.ShortPresentAffirmative, s => $"{s}-IMASU-U" },
                { Conjugation.ShortPresentNegative, s => $"{s}-IMASEN-U" },
                { Conjugation.ShortPastAffirmative, s => $"{s}-IMASHITA-U" },
                { Conjugation.ShortPastNegative, s => $"{s}-IMASENDESHITA-U" },
                { Conjugation.ShortFutureAffirmative, s => $"{s}-IMASU-U" },
                { Conjugation.ShortFutureNegative, s => $"{s}-IMASEN-U" }
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
                { Conjugation.ShortPresentAffirmative, s => $"{s}-ing" },
                { Conjugation.ShortPresentNegative, s => $"{s}-ing" },
                { Conjugation.ShortPastAffirmative, s => $"{s}-ed" },
                { Conjugation.ShortPastNegative, s => $"{s}-ed" },
                { Conjugation.ShortFutureAffirmative, s => $"{s}" },
                { Conjugation.ShortFutureNegative, s => $"{s}" }
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
