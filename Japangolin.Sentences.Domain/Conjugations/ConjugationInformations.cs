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
                { Conjugation.LongPresentAffirmative, () => "add desu" },  
                { Conjugation.LongPresentNegative, () => "add janaidesu (colloquial of ja arimasen)" },  
                { Conjugation.LongPastAffirmative, () => "add deshita" },  
                { Conjugation.LongPastNegative, () => "add janakattadesu" },  
                { Conjugation.LongFutureAffirmative, () => $"same as present ??? <{JapaneseNoun[Conjugation.LongPresentAffirmative]()}>" },
                { Conjugation.LongFutureNegative, () => $"same as present ??? <{JapaneseNoun[Conjugation.LongPresentNegative]()}>" },
                { Conjugation.ShortPresentAffirmative, () => "add da [long: desu ~> short: da]" },  
                { Conjugation.ShortPresentNegative, () => "add janai [long: janaidesu ~> short: janai (drop desu)]" },  
                { Conjugation.ShortPastAffirmative, () => "add datta [long: deshita ~> short: datta]" },  
                { Conjugation.ShortPastNegative, () => "add janakatta [long: janakattadesu ~> short: janakatta (drop desu)]" },  
                { Conjugation.ShortFutureAffirmative, () => $"same as present ??? <{JapaneseNoun[Conjugation.ShortPresentAffirmative]()}>" },
                { Conjugation.ShortFutureNegative, () => $"same as present ??? <{JapaneseNoun[Conjugation.ShortPresentNegative]()}>" }
            };

        public static readonly Dictionary<Conjugation, Func<string>> JapaneseVerbIchidan =
            new Dictionary<Conjugation, Func<string>>
            {
                { Conjugation.None, () => "n/a" },
                { Conjugation.LongPresentAffirmative, () => "drop ru, add masu" },  
                { Conjugation.LongPresentNegative, () => "drop ru, add masen" },  
                { Conjugation.LongPastAffirmative, () => "drop ru, add mashita" },  
                { Conjugation.LongPastNegative, () => "drop ru, add masendeshita" },  
                { Conjugation.LongFutureAffirmative, () => $"same as present ??? <{JapaneseVerbIchidan[Conjugation.LongPresentAffirmative]()}>" },
                { Conjugation.LongFutureNegative, () => $"same as present ??? <{JapaneseVerbIchidan[Conjugation.LongPresentNegative]()}>" },
                { Conjugation.ShortPresentAffirmative, () => "dictionary form" },  
                { Conjugation.ShortPresentNegative, () => "drop ru, add nai" },  
                { Conjugation.ShortPastAffirmative, () => "te-form, te becomes ta" },  
                { Conjugation.ShortPastNegative, () => "drop ru, add nakatta [short-present: nai ~> short-past: nakatta]" },  
                { Conjugation.ShortFutureAffirmative, () => $"same as present ??? <{JapaneseVerbIchidan[Conjugation.ShortPresentAffirmative]()}>" },
                { Conjugation.ShortFutureNegative, () => $"same as present ??? <{JapaneseVerbIchidan[Conjugation.ShortPresentNegative]()}>" }
            };

        public static readonly Dictionary<Conjugation, Func<string>> JapaneseVerbGodan =
            new Dictionary<Conjugation, Func<string>>
            {
                { Conjugation.None, () => "n/a" },
                { Conjugation.LongPresentAffirmative, () => "~u becomes ~i, add masu" },  
                { Conjugation.LongPresentNegative, () => "~u becomes ~i, add masen" },  
                { Conjugation.LongPastAffirmative, () => "~u becomes ~i, add mashita" },  
                { Conjugation.LongPastNegative, () => "~u becomes ~i, add masendeshita" },  
                { Conjugation.LongFutureAffirmative, () => $"same as present ??? <{JapaneseVerbGodan[Conjugation.LongPresentAffirmative]()}>" },
                { Conjugation.LongFutureNegative, () => $"same as present ??? <{JapaneseVerbGodan[Conjugation.LongPresentNegative]()}>" },
                { Conjugation.ShortPresentAffirmative, () => "dictionary form" },  
                { Conjugation.ShortPresentNegative, () => "~u becomes ~a, add nai" },  
                { Conjugation.ShortPastAffirmative, () => "te-form, te becomes ta" }, 
                { Conjugation.ShortPastNegative, () => "~u becomes ~a, add nakatta [short-present: nai ~> short-past: nakatta]" },  
                { Conjugation.ShortFutureAffirmative, () => $"same as present ??? <{JapaneseVerbGodan[Conjugation.ShortPresentAffirmative]()}>" }, 
                { Conjugation.ShortFutureNegative, () => $"same as present ??? <{JapaneseVerbGodan[Conjugation.ShortPresentNegative]()}>" }
            };

        public static readonly Dictionary<Conjugation, Func<string>> JapaneseAdjectiveI =
            new Dictionary<Conjugation, Func<string>>
            {
                { Conjugation.None, () => "n/a" },
                { Conjugation.LongPresentAffirmative, () => "add desu" },  
                { Conjugation.LongPresentNegative, () => "drop i, add kunaidesu" },  
                { Conjugation.LongPastAffirmative, () => "drop i, add kattadesu" },  
                { Conjugation.LongPastNegative, () => "drop i, add kunakattadesu" },  
                { Conjugation.LongFutureAffirmative, () => $"same as present ??? <{JapaneseAdjectiveI[Conjugation.LongPresentAffirmative]()}>" },
                { Conjugation.LongFutureNegative, () => $"same as present ??? <{JapaneseAdjectiveI[Conjugation.LongPresentNegative]()}>" },
                { Conjugation.ShortPresentAffirmative, () => "dictionary form" },  
                { Conjugation.ShortPresentNegative, () => "add kunai [long: kunaidesu ~> short: kunai (drop desu)]" },  
                { Conjugation.ShortPastAffirmative, () => "add katta [long: kattadesu ~> short: katta (drop desu)]" },  
                { Conjugation.ShortPastNegative, () => "add kunakatta [long: kunakattadesu ~> short: kunakatta (drop desu)]" },  
                { Conjugation.ShortFutureAffirmative, () => $"same as present ??? <{JapaneseAdjectiveI[Conjugation.ShortPresentAffirmative]()}>" },
                { Conjugation.ShortFutureNegative, () => $"same as present ??? <{JapaneseAdjectiveI[Conjugation.ShortPresentNegative]()}>" },
            };

        public static readonly Dictionary<Conjugation, Func<string>> JapaneseAdjectiveNa =
            new Dictionary<Conjugation, Func<string>>
            {
                { Conjugation.None, () => "n/a" },
                { Conjugation.LongPresentAffirmative, () => $"drop na, conjugate as noun: {JapaneseNoun[Conjugation.LongPresentAffirmative]()}" },  
                { Conjugation.LongPresentNegative, () => $"drop na, conjugate as noun: {JapaneseNoun[Conjugation.LongPresentNegative]()}" },
                { Conjugation.LongPastAffirmative, () => $"drop na, conjugate as noun: {JapaneseNoun[Conjugation.LongPastAffirmative]()}" },
                { Conjugation.LongPastNegative, () => $"drop na, conjugate as noun: {JapaneseNoun[Conjugation.LongPastNegative]()}" },
                { Conjugation.LongFutureAffirmative, () => $"same as present ??? <{JapaneseAdjectiveNa[Conjugation.LongPresentAffirmative]()}>" },
                { Conjugation.LongFutureNegative, () => $"same as present ??? <{JapaneseAdjectiveNa[Conjugation.LongPresentNegative]()}>" },
                { Conjugation.ShortPresentAffirmative, () => $"drop na, conjugate as noun: {JapaneseNoun[Conjugation.ShortPresentAffirmative]()}" },
                { Conjugation.ShortPresentNegative, () => $"drop na, conjugate as noun: {JapaneseNoun[Conjugation.ShortPresentNegative]()}" },
                { Conjugation.ShortPastAffirmative, () => $"drop na, conjugate as noun: {JapaneseNoun[Conjugation.ShortPastAffirmative]()}" },
                { Conjugation.ShortPastNegative, () => $"drop na, conjugate as noun: {JapaneseNoun[Conjugation.ShortPastNegative]()}" },
                { Conjugation.ShortFutureAffirmative, () => $"same as present ??? <{JapaneseAdjectiveNa[Conjugation.ShortPresentAffirmative]()}>" },
                { Conjugation.ShortFutureNegative, () => $"same as present ??? <{JapaneseAdjectiveNa[Conjugation.ShortPresentNegative]()}>" }
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
