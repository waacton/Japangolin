namespace Wacton.Japangolin.Sentences.Domain.Golins
{
    using Wacton.Japangolin.Sentences.Domain.Conjugations;

    public class Japanese
    {
        public string KanaBase { get; }
        public string KanjiBase { get; }
        public WordClass WordClass { get; }
        public Conjugation Conjugation { get; }

        public string KanaConjugated => this.WordClass.GetConjugation(this.KanaBase, this.Conjugation);
        public string KanjiConjugated => this.WordClass.GetConjugation(this.KanjiBase, this.Conjugation);

        public string ConjugationInformation => this.WordClass.GetConjugationInformation(this.Conjugation);

        public Japanese(string kanaBase, string kanjiBase, WordClass wordClass, Conjugation conjugation)
        {
            this.KanaBase = kanaBase;
            this.KanjiBase = kanjiBase;
            this.WordClass = wordClass;
            this.Conjugation = conjugation;
        }

        public Japanese(string kanaBase, string kanjiBase) : this(kanaBase, kanjiBase, WordClass.None, Conjugation.None)
        {
        }

        public Japanese(string japaneseBase) : this(japaneseBase, japaneseBase)
        {
        }
    }
}
