namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    public class Sentence
    {
        public static string TopicMarker = "は";
        public static string ObjectMarker = "です";

        public bool IsAffirmative { get; set; }
        public bool IsPresent { get; set; }
        public bool IsLongForm { get; set; }

        public INounPhrase TopicNounPhrase { get; }
        public INounPhrase ObjectNounPhrase { get; }

        public Sentence(INounPhrase topicNounPhrase, INounPhrase objectNounPhrase)
        {
            this.TopicNounPhrase = topicNounPhrase;
            this.ObjectNounPhrase = objectNounPhrase;

            // TODO: not this? :)
            this.IsAffirmative = true; //RandomSelection.IsSuccessful(0.5);
            this.IsPresent = true; //RandomSelection.IsSuccessful(0.5);
            this.IsLongForm = true; //RandomSelection.IsSuccessful(0.5);
        }

        public List<ITranslation> GetEnglishOrderTranslations()
        {
            EnglishOnlyTranslation englishIs;
            if (this.IsPresent)
            {
                if (this.IsAffirmative)
                {
                    englishIs = new EnglishOnlyTranslation("is");
                }
                else
                {
                    englishIs = new EnglishOnlyTranslation("is not");
                }
            }
            else
            {
                if (this.IsAffirmative)
                {
                    englishIs = new EnglishOnlyTranslation("was");
                }
                else
                {
                    englishIs = new EnglishOnlyTranslation("was not");
                }
            }

            var englishTranslations = new List<ITranslation>();
            englishTranslations.AddRange(this.TopicNounPhrase.GetEnglishOrder());
            englishTranslations.Add(englishIs);
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
            japaneseTranslations.Add(new JapaneseOnlyTranslation("です"));
            return japaneseTranslations;
        }

        public string GetKana() => this.ConvertToKana(this.GetJapaneseOrderTranslations());

        public string GetKanji() => this.ConvertToKanji(this.GetJapaneseOrderTranslations());

        private string ConvertToEnglish(List<ITranslation> translations)
        {
            return translations.Aggregate(string.Empty, (current, translation) => current + translation.English);
        }

        private string ConvertToKana(List<ITranslation> translations)
        {
            return translations.Aggregate(string.Empty, (current, translation) => current + translation.Kana);
        }

        private string ConvertToKanji(List<ITranslation> translations)
        {
            return translations.Aggregate(string.Empty, (current, translation) => current + translation.Kanji);
        }

        // TODO: this kinda feels like it belongs on the noun phrase class?  ask the noun phrase to conjugate based on affirmative/present/long?
        public static string ConjugateNoun(string noun, bool isAffirmative, bool isPresent, bool isLongForm)
        {
            if (isLongForm)
            {
                if (isPresent)
                {
                    if (isAffirmative)
                    {
                        return $"{noun}です";
                    }

                    return $"{noun}じゃないです";
                }
                else
                {
                    if (isAffirmative)
                    {
                        return $"{noun}でした";
                    }

                    return $"{noun}じゃなかったです";
                }
            }
            else
            {
                if (isPresent)
                {
                    if (isAffirmative)
                    {
                        return $"{noun}だ";
                    }

                    return $"{noun}じゃない";
                }
                else
                {
                    if (isAffirmative)
                    {
                        return $"{noun}だった";
                    }

                    return $"{noun}じゃなかった";
                }
            }
        }
    }
}
