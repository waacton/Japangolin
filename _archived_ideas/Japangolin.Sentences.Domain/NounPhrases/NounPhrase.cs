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

        public IGolin Noun { get; private set; }

        protected NounPhrase(IJapaneseEntry noun)
        {
            this.noun = noun;
            this.Noun = GolinFactory.Noun(this.noun, this.conjugation);
        }

        public void SetConjugation(Conjugation newConjugation)
        {
            this.conjugation = newConjugation;
            this.Noun = GolinFactory.Noun(this.noun, this.conjugation);
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
