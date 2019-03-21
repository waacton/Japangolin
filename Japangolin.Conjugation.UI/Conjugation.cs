namespace Wacton.Japangolin.Conjugation
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Tovarisch.Enum;

    public class Conjugation : Enumeration
    {
        public static readonly Conjugation None = new Conjugation("None", Formality.None, Tense.None, Polarity.None);
        public static readonly Conjugation LongPresentAffirmative = new Conjugation("LongPresentAffirmative", Formality.Long, Tense.Present, Polarity.Affirmative);
        public static readonly Conjugation LongPresentNegative = new Conjugation("LongPresentNegative", Formality.Long, Tense.Present, Polarity.Negative);
        public static readonly Conjugation LongPastAffirmative = new Conjugation("LongPastAffirmative", Formality.Long, Tense.Past, Polarity.Affirmative);
        public static readonly Conjugation LongPastNegative = new Conjugation("LongPastNegative", Formality.Long, Tense.Past, Polarity.Negative);
        public static readonly Conjugation LongFutureAffirmative = new Conjugation("LongFutureAffirmative", Formality.Long, Tense.Future, Polarity.Affirmative);
        public static readonly Conjugation LongFutureNegative = new Conjugation("LongFutureNegative", Formality.Long, Tense.Future, Polarity.Negative);
        public static readonly Conjugation ShortPresentAffirmative = new Conjugation("ShortPresentAffirmative", Formality.Short, Tense.Present, Polarity.Affirmative);
        public static readonly Conjugation ShortPresentNegative = new Conjugation("ShortPresentNegative", Formality.Short, Tense.Present, Polarity.Negative);
        public static readonly Conjugation ShortPastAffirmative = new Conjugation("ShortPastAffirmative", Formality.Short, Tense.Past, Polarity.Affirmative);
        public static readonly Conjugation ShortPastNegative = new Conjugation("ShortPastNegative", Formality.Short, Tense.Past, Polarity.Negative);
        public static readonly Conjugation ShortFutureAffirmative = new Conjugation("ShortFutureAffirmative", Formality.Short, Tense.Future, Polarity.Affirmative);
        public static readonly Conjugation ShortFutureNegative = new Conjugation("ShortFutureNegative", Formality.Short, Tense.Future, Polarity.Negative);

        public Formality Formality { get; }
        public Tense Tense { get; }
        public Polarity Polarity { get; }

        public string Description => $"{this.Tense.ToString()}, {this.Polarity.ToString()}, {this.Formality.ToString()}";

        public Conjugation(string displayName, Formality formality, Tense tense, Polarity polarity) : base(displayName)
        {
            this.Formality = formality;
            this.Tense = tense;
            this.Polarity = polarity;
        }

        public static IEnumerable<Conjugation> KnownConjugations()
        {
            var allConjugation = GetAll<Conjugation>();
            var unknownConjugations = new List<Conjugation> { None, LongFutureAffirmative, LongFutureNegative, ShortFutureAffirmative, ShortFutureNegative };
            return allConjugation.Except(unknownConjugations);
        }
    }
}
