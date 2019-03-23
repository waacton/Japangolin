namespace Wacton.Japangolin.Conjugation
{
    public class ModifierConj : ModifierBase
    {
        public override string Variation => "Conjugation";
        public override bool IsHighLevel => false;

        public static readonly ModifierConj ConjugatePresentAffirmativeLong = new ModifierConj("🔜➕🙇", "{0}", GetConj(WordClasses.Any, PresentAffirmativeLong));
        public static readonly ModifierConj ConjugatePresentAffirmativeShort = new ModifierConj("🔜➕🗣", "{0}", GetConj(WordClasses.Any, PresentAffirmativeShort));
        public static readonly ModifierConj ConjugatePresentNegativeLong = new ModifierConj("🔜ー🙇", "{0}", GetConj(WordClasses.Any, PresentNegativeLong));
        public static readonly ModifierConj ConjugatePresentNegativeShort = new ModifierConj("🔜ー🗣", "{0}", GetConj(WordClasses.Any, PresentNegativeShort));
        public static readonly ModifierConj ConjugatePastAffirmativeLong = new ModifierConj("🔙➕🙇", "{0}", GetConj(WordClasses.Any, PastAffirmativeLong));
        public static readonly ModifierConj ConjugatePastAffirmativeShort = new ModifierConj("🔙➕🗣", "{0}", GetConj(WordClasses.Any, PastAffirmativeShort));
        public static readonly ModifierConj ConjugatePastNegativeLong = new ModifierConj("🔙ー🙇", "{0}", GetConj(WordClasses.Any, PastNegativeLong));
        public static readonly ModifierConj ConjugatePastNegativeShort = new ModifierConj("🔙ー🗣", "{0}", GetConj(WordClasses.Any, PastNegativeShort));

        public ModifierConj(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName, format, conjugators)
        {
        }
    }
}
