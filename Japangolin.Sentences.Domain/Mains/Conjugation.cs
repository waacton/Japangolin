namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using Wacton.Tovarisch.Enum;

    public class Conjugation : Enumeration
    {
        public static readonly Conjugation LongPresentAffirmative = new Conjugation("LongPresentAffirmative", true, true, true);
        public static readonly Conjugation LongPresentNegative = new Conjugation("LongPresentNegative", true, true, false);
        public static readonly Conjugation LongPastAffirmative = new Conjugation("LongPastAffirmative", true, false, true);
        public static readonly Conjugation LongPastNegative = new Conjugation("LongPastNegative", true, false, false);
        public static readonly Conjugation ShortPresentAffirmative = new Conjugation("ShortPresentAffirmative", false, true, true);
        public static readonly Conjugation ShortPresentNegative = new Conjugation("ShortPresentNegative", false, true, false);
        public static readonly Conjugation ShortPastAffirmative = new Conjugation("ShortPastAffirmative", false, false, true);
        public static readonly Conjugation ShortPastNegative = new Conjugation("ShortPastNegative", false, false, false);

        public bool IsAffirmative { get; }
        public bool IsPresent { get; }
        public bool IsLong { get; }

        public Conjugation(string displayName, bool isLong, bool isPresent, bool isAffirmative) : base(displayName)
        {
            this.IsAffirmative = isAffirmative;
            this.IsPresent = isPresent;
            this.IsLong = isLong;
        }
    }
}
