using System;

namespace Wacton.Japangolin
{
    public class Word
    {
        public string Kana { get; set; }
        public string Kanji { get; set; }
        public string English { get; set; }
        public WordClass Class { get; set; }

        public string ConjugateKana(Func<WordClass, Conjugator> conjugator)
        {
            return conjugator(this.Class).Conjugate(this.Kana);
        }

        // TODO: incorporate 'usually only kana'?
        public string ConjugateKanji(Func<WordClass, Conjugator> conjugator)
        {
            return conjugator(this.Class).Conjugate(this.Kanji);
        }

        public override string ToString()
        {
            return $"{this.Kana} / {this.Class} / {this.English}";
        }
    }
}
