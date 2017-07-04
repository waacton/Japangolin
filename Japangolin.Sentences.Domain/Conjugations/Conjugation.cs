namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Tovarisch.Enum;

    public class Conjugation : Enumeration
    {
        public static readonly Conjugation None = new Conjugation("None", Formality.None, Tense.None, Polarity.None);
        public static readonly Conjugation LongPresentAffirmative = new Conjugation("LongPresentAffirmative", Formality.Polite, Tense.Present, Polarity.Affirmative);
        public static readonly Conjugation LongPresentNegative = new Conjugation("LongPresentNegative", Formality.Polite, Tense.Present, Polarity.Negative);
        public static readonly Conjugation LongPastAffirmative = new Conjugation("LongPastAffirmative", Formality.Polite, Tense.Past, Polarity.Affirmative);
        public static readonly Conjugation LongPastNegative = new Conjugation("LongPastNegative", Formality.Polite, Tense.Past, Polarity.Negative);
        public static readonly Conjugation LongFutureAffirmative = new Conjugation("LongFutureAffirmative", Formality.Polite, Tense.Future, Polarity.Affirmative);
        public static readonly Conjugation LongFutureNegative = new Conjugation("LongFutureNegative", Formality.Polite, Tense.Future, Polarity.Negative);
        public static readonly Conjugation ShortPresentAffirmative = new Conjugation("ShortPresentAffirmative", Formality.Casual, Tense.Present, Polarity.Affirmative);
        public static readonly Conjugation ShortPresentNegative = new Conjugation("ShortPresentNegative", Formality.Casual, Tense.Present, Polarity.Negative);
        public static readonly Conjugation ShortPastAffirmative = new Conjugation("ShortPastAffirmative", Formality.Casual, Tense.Past, Polarity.Affirmative);
        public static readonly Conjugation ShortPastNegative = new Conjugation("ShortPastNegative", Formality.Casual, Tense.Past, Polarity.Negative);
        public static readonly Conjugation ShortFutureAffirmative = new Conjugation("ShortFutureAffirmative", Formality.Casual, Tense.Future, Polarity.Affirmative);
        public static readonly Conjugation ShortFutureNegative = new Conjugation("ShortFutureNegative", Formality.Casual, Tense.Future, Polarity.Negative);

        public Formality Formality { get; }
        public Tense Tense { get; }
        public Polarity Polarity { get; }

        public string Description => $"{this.Formality.ToString()}, {this.Tense.ToString()}, {this.Polarity.ToString()}";

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
