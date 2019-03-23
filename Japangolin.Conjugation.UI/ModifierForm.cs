namespace Wacton.Japangolin.Conjugation
{
    public class ModifierForm : ModifierBase
    {
        public override string Variation => "Form";
        public override bool IsHighLevel => false;

        public static readonly ModifierForm FormDictionary = new ModifierForm("Dictionary", "{0}", GetConj(WordClasses.Any, Dictionary));
        public static readonly ModifierForm FormStem = new ModifierForm("Stem", "{0}", GetConj(WordClasses.Any, Stem));
        public static readonly ModifierForm FormTe = new ModifierForm("～て", "{0}", GetConj(WordClasses.Any, Te));

        public ModifierForm(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName, format, conjugators)
        {
        }
    }
}
