namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public static class ConjugationFunctions
    {
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
