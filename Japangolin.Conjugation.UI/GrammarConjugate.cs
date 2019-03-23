namespace Wacton.Japangolin.Conjugation
{
    public class GrammarConjugate : GrammarBase
    {
        public override string Variation => "Conjugation";
        public override bool IsHighLevel => false;

        public static readonly GrammarConjugate ConjugatePresentAffirmativeLong = new GrammarConjugate("🔜➕🙇", "{0}", GetConj(WordClasses.Any, PresentAffirmativeLong));
        public static readonly GrammarConjugate ConjugatePresentAffirmativeShort = new GrammarConjugate("🔜➕🗣", "{0}", GetConj(WordClasses.Any, PresentAffirmativeShort));
        public static readonly GrammarConjugate ConjugatePresentNegativeLong = new GrammarConjugate("🔜ー🙇", "{0}", GetConj(WordClasses.Any, PresentNegativeLong));
        public static readonly GrammarConjugate ConjugatePresentNegativeShort = new GrammarConjugate("🔜ー🗣", "{0}", GetConj(WordClasses.Any, PresentNegativeShort));
        public static readonly GrammarConjugate ConjugatePastAffirmativeLong = new GrammarConjugate("🔙➕🙇", "{0}", GetConj(WordClasses.Any, PastAffirmativeLong));
        public static readonly GrammarConjugate ConjugatePastAffirmativeShort = new GrammarConjugate("🔙➕🗣", "{0}", GetConj(WordClasses.Any, PastAffirmativeShort));
        public static readonly GrammarConjugate ConjugatePastNegativeLong = new GrammarConjugate("🔙ー🙇", "{0}", GetConj(WordClasses.Any, PastNegativeLong));
        public static readonly GrammarConjugate ConjugatePastNegativeShort = new GrammarConjugate("🔙ー🗣", "{0}", GetConj(WordClasses.Any, PastNegativeShort));

        public GrammarConjugate(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName, format, conjugators)
        {
        }
    }
}
