namespace Wacton.Japangolin.Conjugation
{
    using System;
    using System.Collections.Generic;

    public class ModifierConj : ModifierBase
    {
        public override string Variation => "Conjugation";
        public override bool IsHighLevel => false;

        public static readonly ModifierConj ConjugatePresentAffirmativeLong = new ModifierConj("🔜➕🙇", "{0}", (PresentAffirmativeLong, All));
        public static readonly ModifierConj ConjugatePresentAffirmativeShort = new ModifierConj("🔜➕🗣", "{0}", (PresentAffirmativeShort, All));
        public static readonly ModifierConj ConjugatePresentNegativeLong = new ModifierConj("🔜ー🙇", "{0}", (PresentNegativeLong, All));
        public static readonly ModifierConj ConjugatePresentNegativeShort = new ModifierConj("🔜ー🗣", "{0}", (PresentNegativeShort, All));
        public static readonly ModifierConj ConjugatePastAffirmativeLong = new ModifierConj("🔙➕🙇", "{0}", (PastAffirmativeLong, All));
        public static readonly ModifierConj ConjugatePastAffirmativeShort = new ModifierConj("🔙➕🗣", "{0}", (PastAffirmativeShort, All));
        public static readonly ModifierConj ConjugatePastNegativeLong = new ModifierConj("🔙ー🙇", "{0}", (PastNegativeLong, All));
        public static readonly ModifierConj ConjugatePastNegativeShort = new ModifierConj("🔙ー🗣", "{0}", (PastNegativeShort, All));

        public ModifierConj(string displayName, string format, params (Func<WordClass, Conjugator> conjugatorByWordClass, List<WordClass> wordClasses)[] conjugations)
            : base(displayName, format, conjugations)
        {
        }
    }
}
