namespace Wacton.Japangolin.Conjugation
{
    public class WordData
    {
        public string Kana { get; set; }
        public string Kanji { get; set; }
        public string English { get; set; }
        public WordClass Class { get; set; }

        public string ConjugateKana(WordClassConjugator conjugator)
        {
            return conjugator.Conjugate(this.Kana, this.Class);
        }

        // TODO: incorporate 'usually only kana'?
        public string ConjugateKanji(WordClassConjugator conjugator)
        {
            return conjugator.Conjugate(this.Kanji, this.Class);
        }

        public override string ToString()
        {
            return $"{this.Kana} / {this.Class} / {this.English}";
        }
    }
}
