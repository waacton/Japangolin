namespace Wacton.Japangolin.Sentences.Domain.Golins
{
    using Wacton.Japangolin.Sentences.Domain.Conjugations;

    public class English
    {
        public string EnglishBase { get; }
        public WordClass WordClass { get; }
        public Conjugation Conjugation { get; }

        public string EnglishConjugated => this.WordClass.GetConjugation(this.EnglishBase, this.Conjugation);

        public string ConjugationInformation => this.WordClass.GetConjugationInformation(this.Conjugation);

        public English(string englishBase, WordClass wordClass, Conjugation conjugation)
        {
            this.EnglishBase = englishBase;
            this.WordClass = wordClass;
            this.Conjugation = conjugation;
        }

        public English(string englishBase) : this(englishBase, WordClass.None, Conjugation.None)
        {
        }
    }
}
