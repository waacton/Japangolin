namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using Wacton.Tovarisch.Enum;

    public class Conjugation : Enumeration
    {
        public static readonly Conjugation LongPresentAffirmative = new Conjugation("LongPresentAffirmative");
        public static readonly Conjugation LongPresentNegative = new Conjugation("LongPresentNegative");
        public static readonly Conjugation LongPastAffirmative = new Conjugation("LongPastAffirmative");
        public static readonly Conjugation LongPastNegative = new Conjugation("LongPastNegative");
        public static readonly Conjugation LongFutureAffirmative = new Conjugation("LongFutureAffirmative");
        public static readonly Conjugation LongFutureNegative = new Conjugation("LongFutureNegative");
        public static readonly Conjugation ShortPresentAffirmative = new Conjugation("ShortPresentAffirmative");
        public static readonly Conjugation ShortPresentNegative = new Conjugation("ShortPresentNegative");
        public static readonly Conjugation ShortPastAffirmative = new Conjugation("ShortPastAffirmative");
        public static readonly Conjugation ShortPastNegative = new Conjugation("ShortPastNegative");
        public static readonly Conjugation ShortFutureAffirmative = new Conjugation("ShortFutureAffirmative");
        public static readonly Conjugation ShortFutureNegative = new Conjugation("ShortFutureNegative");

        public Conjugation(string displayName) : base(displayName)
        {
        }
    }
}
