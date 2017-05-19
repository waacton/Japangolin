namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    public class Sentence
    {
        public TopicBlock TopicBlock { get; }
        public ObjectBlock ObjectBlock { get; }
        public Conjugation Conjugation { get; }

        public Sentence(TopicBlock topicBlock, ObjectBlock objectBlock, Conjugation conjugation)
        {
            this.TopicBlock = topicBlock;
            this.ObjectBlock = objectBlock;
            this.Conjugation = conjugation;
        }

        public List<ITranslation> GetEnglishOrderTranslations()
        {
            var englishTranslations = new List<ITranslation>();
            englishTranslations.AddRange(this.TopicBlock.GetEnglishOrder());
            englishTranslations.AddRange(this.ObjectBlock.GetEnglishOrder());
            return englishTranslations;
        }

        public List<ITranslation> GetJapaneseOrderTranslations()
        {
            var japaneseTranslations = new List<ITranslation>();
            japaneseTranslations.AddRange(this.TopicBlock.GetJapaneseOrder());
            japaneseTranslations.AddRange(this.ObjectBlock.GetJapaneseOrder());
            return japaneseTranslations;
        }

        public string GetEnglish() => ConvertToEnglish(this.GetEnglishOrderTranslations());

        public string GetKana() => ConvertToKana(this.GetJapaneseOrderTranslations(), this.Conjugation);

        public string GetKanji() => ConvertToKanji(this.GetJapaneseOrderTranslations(), this.Conjugation);

        private static string ConvertToEnglish(List<ITranslation> translations)
        {
            return string.Join(" ", translations.Select(translation => translation.English));
        }

        private static string ConvertToKana(List<ITranslation> translations, Conjugation conjugation)
        {
            return ConvertToJapanese(translations, conjugation, true);
        }

        private static string ConvertToKanji(List<ITranslation> translations, Conjugation conjugation)
        {
            return ConvertToJapanese(translations, conjugation, false);
        }

        private static string ConvertToJapanese(List<ITranslation> translations, Conjugation conjugation, bool isKana)
        {
            var sentenceEnd = translations.Last();
            translations.Remove(sentenceEnd);

            var conjugated = sentenceEnd.KanaConjugated;
            return string.Concat(string.Join(string.Empty, translations.Select(translation => isKana ? translation.Kana : translation.Kanji)), conjugated);
        }

        public override string ToString()
        {
            return $"{this.GetEnglish()} | {this.GetKana()}";
        }
    }
}
