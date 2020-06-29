using Wacton.Tovarisch.Enum;

namespace Wacton.Japangolin.Domain
{
    public class Inflection : Enumeration
    {
        public static readonly Inflection Dictionary = new Inflection("Dictionary", Conjugators.Dictionary);
        public static readonly Inflection Stem = new Inflection("Stem", Conjugators.Stem);
        public static readonly Inflection Te = new Inflection("Te", Conjugators.Te);
        public static readonly Inflection PresentAffirmativeLong = new Inflection("PresentAffirmativeLong", Conjugators.PresentAffirmativeLong);
        public static readonly Inflection PresentAffirmativeShort = new Inflection("PresentAffirmativeShort", Conjugators.PresentAffirmativeShort);
        public static readonly Inflection PresentNegativeLong = new Inflection("PresentNegativeLong", Conjugators.PresentNegativeLong);
        public static readonly Inflection PresentNegativeShort = new Inflection("PresentNegativeShort", Conjugators.PresentNegativeShort);
        public static readonly Inflection PastAffirmativeLong = new Inflection("PastAffirmativeLong", Conjugators.PastAffirmativeLong);
        public static readonly Inflection PastAffirmativeShort = new Inflection("PastAffirmativeShort", Conjugators.PastAffirmativeShort);
        public static readonly Inflection PastNegativeLong = new Inflection("PastNegativeLong", Conjugators.PastNegativeLong);
        public static readonly Inflection PastNegativeShort = new Inflection("PastNegativeShort", Conjugators.PastNegativeShort);

        // ---

        private readonly WordConjugator wordConjugator;

        public Inflection(string displayName, WordConjugator wordConjugator)
            : base(displayName)
        {
            this.wordConjugator = wordConjugator;
        }

        public (string kana, string kanji) Conjugate(Word word)
        {
            return this.wordConjugator.Conjugate(word);
        }

        public string GetHint(Word word)
        {
            return this.wordConjugator.GetHint(word);
        }

        public string PrettyDisplay()
        {
            return PascalCase.InsertSeparator(this.DisplayName, " · ").ToLower();
        }

        public override string ToString() => this.PrettyDisplay();
    }
}
