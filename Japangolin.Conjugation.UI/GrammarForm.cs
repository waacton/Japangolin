namespace Wacton.Japangolin.Conjugation
{
    public class GrammarForm : GrammarBase
    {
        public override string Variation => "Form";
        public override bool IsHighLevel => false;

        public static readonly GrammarForm FormDictionary = new GrammarForm("Dictionary", "{0}", GetConj(WordClasses.Any, Dictionary));
        public static readonly GrammarForm FormStem = new GrammarForm("Stem", "{0}", GetConj(WordClasses.Any, Stem));
        public static readonly GrammarForm FormTe = new GrammarForm("～て", "{0}", GetConj(WordClasses.Any, Te));

        public GrammarForm(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName, format, conjugators)
        {
        }
    }
}
