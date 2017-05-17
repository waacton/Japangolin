namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    public class Sentence
    {
        public static string TopicMarker = "は";
        public static string ObjectMarker = "です";

        public INounPhrase TopicNounPhrase { get; }
        public INounPhrase ObjectNounPhrase { get; }
        public Conjugation Conjugation { get; }

        public Sentence(INounPhrase topicNounPhrase, INounPhrase objectNounPhrase, Conjugation conjugation)
        {
            this.TopicNounPhrase = topicNounPhrase;
            this.ObjectNounPhrase = objectNounPhrase;
            this.Conjugation = conjugation;
        }

        public List<ITranslation> GetEnglishOrderTranslations()
        {
            string englishString;
            if (this.Conjugation.IsPresent)
            {
                englishString = this.Conjugation.IsAffirmative ? "is" : "is not";
            }
            else
            {
                englishString = this.Conjugation.IsAffirmative ? "was" : "was not";
            }

            var englishTranslations = new List<ITranslation>();
            englishTranslations.AddRange(this.TopicNounPhrase.GetEnglishOrder());
            englishTranslations.Add(new EnglishOnlyTranslation(englishString));
            englishTranslations.AddRange(this.ObjectNounPhrase.GetEnglishOrder());
            englishTranslations.Add(new EnglishOnlyTranslation("."));
            return englishTranslations;
        }

        public List<ITranslation> GetJapaneseOrderTranslations()
        {
            var japaneseTranslations = new List<ITranslation>();
            japaneseTranslations.AddRange(this.TopicNounPhrase.GetJapaneseOrder());
            japaneseTranslations.Add(new JapaneseOnlyTranslation(TopicMarker));
            japaneseTranslations.AddRange(this.ObjectNounPhrase.GetJapaneseOrder());
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

            var conjugated = sentenceEnd.Conjugate(conjugation, isKana);
            return string.Concat(string.Join(string.Empty, translations.Select(translation => isKana ? translation.Kana : translation.Kanji)), conjugated);
        }

        public override string ToString()
        {
            return $"{this.GetEnglish()} | {this.GetKana()}";
        }
    }
}
