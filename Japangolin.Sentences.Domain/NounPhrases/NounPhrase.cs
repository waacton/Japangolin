namespace Wacton.Japangolin.Sentences.Domain.NounPhrases
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;

    public abstract class NounPhrase
    {
        private readonly IJapaneseEntry noun;
        private Conjugation conjugation = Conjugation.None;

        public IGolin Noun => GolinFactory.Noun(this.noun, this.conjugation);

        protected NounPhrase(IJapaneseEntry noun)
        {
            this.noun = noun;
        }

        public void SetConjugation(Conjugation newConjugation)
        {
            this.conjugation = newConjugation;
        }

        public abstract List<IGolin> GolinEnglish();
        public abstract List<IGolin> GolinJapanese();

        public override string ToString() => this.GetNounPhraseToString();

        // TODO: remove japanese spacing - only used for testing purposes
        private static readonly string EnglishSpace = " ";
        private static readonly string JapaneseSpace = "　";
        public string GetNounPhraseToString()
        {
            var english = string.Join(EnglishSpace, this.GolinEnglish().Select(translation => translation.EnglishBase));
            var kana = string.Join(JapaneseSpace, this.GolinJapanese().Select(translation => translation.KanaBase));
            return $"{english} | {kana}";
        }
    }
}
