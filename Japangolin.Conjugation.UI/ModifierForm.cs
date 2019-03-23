namespace Wacton.Japangolin.Conjugation
{
    using System;
    using System.Collections.Generic;

    public class ModifierForm : ModifierBase
    {
        public override string Variation => "Form";
        public override bool IsHighLevel => false;

        public static readonly ModifierForm FormDictionary = new ModifierForm("Dictionary", "{0}", (Dictionary, All));
        public static readonly ModifierForm FormStem = new ModifierForm("Stem", "{0}", (Stem, All));
        public static readonly ModifierForm FormTe = new ModifierForm("～て", "{0}", (Te, All));

        public ModifierForm(string displayName, string format, params (Func<WordClass, Conjugator> conjugatorByWordClass, List<WordClass> wordClasses)[] conjugations)
            : base(displayName, format, conjugations)
        {
        }
    }
}
