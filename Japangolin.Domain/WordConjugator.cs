namespace Wacton.Japangolin.Domain
{
    using System;

    public class WordConjugator
    {
        private readonly Func<Word, Conjugator> GetConjugator;

        public WordConjugator(Func<Word, Conjugator> getConjugator)
        {
            this.GetConjugator = getConjugator;
        }

        public (string kana, string kanji) Conjugate(Word word)
        {
            var conjugator = this.GetConjugator(word);
            var kana = conjugator.Conjugate(word.Kana);
            var kanji = conjugator.Conjugate(word.Kanji);
            return (kana, kanji);
        }

        public string GetHint(Word word)
        {
            var conjugator = this.GetConjugator(word);
            return conjugator.Hint;
        }
    }
}
