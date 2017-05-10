namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Randomness;

    public class SentenceNounIsNoun
    {
        public static string TopicMarker = "は";

        //public bool IsAffirmative => true;
        //public bool IsPresent => true;
        //public bool IsLongForm => true;

        public bool IsAffirmative { get; set; }
        public bool IsPresent { get; set; }
        public bool IsLongForm { get; set; }

        public Translation TopicNoun { get; private set; }
        public Translation ObjectNoun { get; private set; }

        public SentenceNounIsNoun(IJapaneseEntry topicNoun, IJapaneseEntry objectNoun)
        {
            this.TopicNoun = ConvertToTranslation(topicNoun);
            this.ObjectNoun = ConvertToTranslation(objectNoun);

            // TODO: not this? :)
            this.IsAffirmative = RandomSelection.IsSuccessful(0.5);
            this.IsPresent = RandomSelection.IsSuccessful(0.5);
            this.IsLongForm = RandomSelection.IsSuccessful(0.5);
        }

        public List<Translation> GetTranslations()
        {
            Translation englishOnly;
            if (this.IsPresent)
            {
                if (this.IsAffirmative)
                {
                    englishOnly = new Translation("is");
                }
                else
                {
                    englishOnly = new Translation("is not");
                }
            }
            else
            {
                if (this.IsAffirmative)
                {
                    englishOnly = new Translation("was");
                }
                else
                {
                    englishOnly = new Translation("was not");
                }
            }

            return new List<Translation> { this.TopicNoun, englishOnly, this.ObjectNoun, new Translation(".") };
        }

        public string GetKana()
        {
            return $"{this.TopicNoun.Kana}{TopicMarker}{ConjugateNoun(this.ObjectNoun.Kana, this.IsAffirmative, this.IsPresent, this.IsLongForm)}。";
        }

        public string GetKanji()
        {
            return $"{this.TopicNoun.Kana}{TopicMarker}{ConjugateNoun(this.ObjectNoun.Kanji, this.IsAffirmative, this.IsPresent, this.IsLongForm)}。";
        }

        private Translation ConvertToTranslation(IJapaneseEntry japaneseEntry)
        {
            var english = japaneseEntry.Senses.First().Glosses.First().Term;
            var kana = japaneseEntry.Readings.First().Text;
            var kanji = japaneseEntry.Kanjis.Any() ? japaneseEntry.Kanjis.First().Text : kana;
            return new Translation(english, kanji, kana);
        }

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
