namespace Wacton.Japangolin.Conjugation
{
    public class GrammarForm : GrammarBase
    {
        public override string Variation => "Form";
        public override bool IsHighLevel => false;

        public static readonly GrammarForm Dictionary = new GrammarForm("Dictionary", "{0}", WordClasses.Any.Link(ConjugatorFuncs.Dictionary));
        public static readonly GrammarForm Stem = new GrammarForm("Stem", "{0}", WordClasses.Any.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarForm Te = new GrammarForm("～て", "{0}", WordClasses.Any.Link(ConjugatorFuncs.Te));

        public static readonly GrammarForm PresentAffirmativeLong = new GrammarForm("🔜✔🙇", "{0}", WordClasses.Any.Link(ConjugatorFuncs.PresentAffirmativeLong));
        public static readonly GrammarForm PresentAffirmativeShort = new GrammarForm("🔜✔🗣", "{0}", WordClasses.Any.Link(ConjugatorFuncs.PresentAffirmativeShort));
        public static readonly GrammarForm PresentNegativeLong = new GrammarForm("🔜❌🙇", "{0}", WordClasses.Any.Link(ConjugatorFuncs.PresentNegativeLong));
        public static readonly GrammarForm PresentNegativeShort = new GrammarForm("🔜❌🗣", "{0}", WordClasses.Any.Link(ConjugatorFuncs.PresentNegativeShort));
        public static readonly GrammarForm PastAffirmativeLong = new GrammarForm("🔙✔🙇", "{0}", WordClasses.Any.Link(ConjugatorFuncs.PastAffirmativeLong));
        public static readonly GrammarForm PastAffirmativeShort = new GrammarForm("🔙✔🗣", "{0}", WordClasses.Any.Link(ConjugatorFuncs.PastAffirmativeShort));
        public static readonly GrammarForm PastNegativeLong = new GrammarForm("🔙❌🙇", "{0}", WordClasses.Any.Link(ConjugatorFuncs.PastNegativeLong));
        public static readonly GrammarForm PastNegativeShort = new GrammarForm("🔙❌🗣", "{0}", WordClasses.Any.Link(ConjugatorFuncs.PastNegativeShort));

        public GrammarForm(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName, format, conjugators)
        {
        }
    }
}
