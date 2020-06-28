namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System;
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public static class ConjugationInformations
    {
        public static readonly Dictionary<Conjugation, Func<string>> JapaneseNoun =
            new Dictionary<Conjugation, Func<string>>
            {
                { Conjugation.None, () => "n/a" },
                { Conjugation.LongPresentAffirmative, () => "add です" },  
                { Conjugation.LongPresentNegative, () => "add じゃないです" },  
                { Conjugation.LongPastAffirmative, () => "add でした" },  
                { Conjugation.LongPastNegative, () => "add じゃなかったです" },  
                { Conjugation.LongFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseNoun[Conjugation.LongPresentAffirmative]()}" },
                { Conjugation.LongFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseNoun[Conjugation.LongPresentNegative]()}" },
                { Conjugation.ShortPresentAffirmative, () => $"add だ{Environment.NewLine}[long: です ~> short: だ]" },  
                { Conjugation.ShortPresentNegative, () => $"add じゃない{Environment.NewLine}[long: じゃないです ~> short: じゃない (drop です)]" },  
                { Conjugation.ShortPastAffirmative, () => $"add だった{Environment.NewLine}[long: でした ~> short: だった]" },  
                { Conjugation.ShortPastNegative, () => $"add じゃなかった{Environment.NewLine}[long: じゃなかったです ~> short: じゃなかった (drop です)]" },  
                { Conjugation.ShortFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseNoun[Conjugation.ShortPresentAffirmative]()}" },
                { Conjugation.ShortFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseNoun[Conjugation.ShortPresentNegative]()}" }
            };

        public static readonly Dictionary<Conjugation, Func<string>> JapaneseVerbIchidan =
            new Dictionary<Conjugation, Func<string>>
            {
                { Conjugation.None, () => "n/a" },
                { Conjugation.LongPresentAffirmative, () => "drop る, add ます" },  
                { Conjugation.LongPresentNegative, () => "drop る, add ません" },  
                { Conjugation.LongPastAffirmative, () => "drop る, add ました" },  
                { Conjugation.LongPastNegative, () => "drop る, add ませんでした" },  
                { Conjugation.LongFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseVerbIchidan[Conjugation.LongPresentAffirmative]()}" },
                { Conjugation.LongFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseVerbIchidan[Conjugation.LongPresentNegative]()}" },
                { Conjugation.ShortPresentAffirmative, () => "dictionary form" },  
                { Conjugation.ShortPresentNegative, () => "drop る, add ない" },  
                { Conjugation.ShortPastAffirmative, () => "て-form, て becomes た" },  
                { Conjugation.ShortPastNegative, () => $"drop る, add なかった{Environment.NewLine}[short-present: ない ~> short-past: なかった]" },  
                { Conjugation.ShortFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseVerbIchidan[Conjugation.ShortPresentAffirmative]()}" },
                { Conjugation.ShortFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseVerbIchidan[Conjugation.ShortPresentNegative]()}" }
            };

        public static readonly Dictionary<Conjugation, Func<string>> JapaneseVerbGodan =
            new Dictionary<Conjugation, Func<string>>
            {
                { Conjugation.None, () => "n/a" },
                { Conjugation.LongPresentAffirmative, () => "~う becomes ~い, add ます" },  
                { Conjugation.LongPresentNegative, () => "~う becomes ~い, add ません" },  
                { Conjugation.LongPastAffirmative, () => "~う becomes ~い, add ました" },  
                { Conjugation.LongPastNegative, () => "~う becomes ~い, add ませんでした" },  
                { Conjugation.LongFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseVerbGodan[Conjugation.LongPresentAffirmative]()}" },
                { Conjugation.LongFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseVerbGodan[Conjugation.LongPresentNegative]()}" },
                { Conjugation.ShortPresentAffirmative, () => "dictionary form" },  
                { Conjugation.ShortPresentNegative, () => "~う becomes ~あ, add ない" },  
                { Conjugation.ShortPastAffirmative, () => "て-form, て becomes た" }, 
                { Conjugation.ShortPastNegative, () => $"~う becomes ~あ, add なかった{Environment.NewLine}[short-present: ない ~> short-past: なかった]" },  
                { Conjugation.ShortFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseVerbGodan[Conjugation.ShortPresentAffirmative]()}" }, 
                { Conjugation.ShortFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseVerbGodan[Conjugation.ShortPresentNegative]()}" }
            };

        public static readonly Dictionary<Conjugation, Func<string>> JapaneseAdjectiveI =
            new Dictionary<Conjugation, Func<string>>
            {
                { Conjugation.None, () => "n/a" },
                { Conjugation.LongPresentAffirmative, () => "add です" },  
                { Conjugation.LongPresentNegative, () => "drop い, add くないです" },  
                { Conjugation.LongPastAffirmative, () => "drop い, add かったです" },  
                { Conjugation.LongPastNegative, () => "drop い, add くなかったです" },  
                { Conjugation.LongFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseAdjectiveI[Conjugation.LongPresentAffirmative]()}" },
                { Conjugation.LongFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseAdjectiveI[Conjugation.LongPresentNegative]()}" },
                { Conjugation.ShortPresentAffirmative, () => "dictionary form" },  
                { Conjugation.ShortPresentNegative, () => $"add くない{Environment.NewLine}[long: くないです ~> short: くない (drop です)]" },  
                { Conjugation.ShortPastAffirmative, () => $"add かった{Environment.NewLine}[long: かったです ~> short: かった (drop です)]" },  
                { Conjugation.ShortPastNegative, () => $"add くなかった{Environment.NewLine}[long: くなかったです ~> short: kunakatta (drop です)]" },  
                { Conjugation.ShortFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseAdjectiveI[Conjugation.ShortPresentAffirmative]()}" },
                { Conjugation.ShortFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseAdjectiveI[Conjugation.ShortPresentNegative]()}" },
            };

        public static readonly Dictionary<Conjugation, Func<string>> JapaneseAdjectiveNa =
            new Dictionary<Conjugation, Func<string>>
            {
                { Conjugation.None, () => "n/a" },
                { Conjugation.LongPresentAffirmative, () => $"drop な, conjugate as noun:{Environment.NewLine}{JapaneseNoun[Conjugation.LongPresentAffirmative]()}" },  
                { Conjugation.LongPresentNegative, () => $"drop な, conjugate as noun:{Environment.NewLine}{JapaneseNoun[Conjugation.LongPresentNegative]()}" },
                { Conjugation.LongPastAffirmative, () => $"drop な, conjugate as noun:{Environment.NewLine}{JapaneseNoun[Conjugation.LongPastAffirmative]()}" },
                { Conjugation.LongPastNegative, () => $"drop な, conjugate as noun:{Environment.NewLine}{JapaneseNoun[Conjugation.LongPastNegative]()}" },
                { Conjugation.LongFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseAdjectiveNa[Conjugation.LongPresentAffirmative]()}" },
                { Conjugation.LongFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseAdjectiveNa[Conjugation.LongPresentNegative]()}" },
                { Conjugation.ShortPresentAffirmative, () => $"drop な, conjugate as noun:{Environment.NewLine}{JapaneseNoun[Conjugation.ShortPresentAffirmative]()}" },
                { Conjugation.ShortPresentNegative, () => $"drop な, conjugate as noun:{Environment.NewLine}{JapaneseNoun[Conjugation.ShortPresentNegative]()}" },
                { Conjugation.ShortPastAffirmative, () => $"drop な, conjugate as noun:{Environment.NewLine}{JapaneseNoun[Conjugation.ShortPastAffirmative]()}" },
                { Conjugation.ShortPastNegative, () => $"drop な, conjugate as noun:{Environment.NewLine}{JapaneseNoun[Conjugation.ShortPastNegative]()}" },
                { Conjugation.ShortFutureAffirmative, () => $"= present:{Environment.NewLine}{JapaneseAdjectiveNa[Conjugation.ShortPresentAffirmative]()}" },
                { Conjugation.ShortFutureNegative, () => $"= present:{Environment.NewLine}{JapaneseAdjectiveNa[Conjugation.ShortPresentNegative]()}" }
            };

        public static Dictionary<Conjugation, Func<string>> Defaults => DefaultConjugationInformations();
        private static Dictionary<Conjugation, Func<string>> DefaultConjugationInformations()
        {
            var funcs = new Dictionary<Conjugation, Func<string>>();
            foreach (var conjugation in Enumeration.GetAll<Conjugation>())
            {
                funcs.Add(conjugation, () => string.Empty);
            }

            return funcs;
        }
    }
}
