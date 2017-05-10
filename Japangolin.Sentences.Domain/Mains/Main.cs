namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Randomness;

    public class Main
    {
        private readonly List<IJapaneseEntry> japaneseEntries;
        public SentenceNounIsNoun CurrentSentence { get; private set; }

        public Main(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseEntries = japaneseDictionary.GetEntries().ToList();
            this.UpdateSentence();
        }

        public void UpdateSentence()
        {
            var nouns = this.japaneseEntries.Where(entry => entry.Senses.Any(sense => sense.PartsOfSpeech.Contains(PartOfSpeech.NounCommon)));

            var topicNoun = RandomSelection.SelectOne(nouns);
            var objectNoun = RandomSelection.SelectOne(nouns);

            this.CurrentSentence = new SentenceNounIsNoun(topicNoun, objectNoun);
        }

        private Translation ConvertToTranslation(IJapaneseEntry japaneseEntry)
        {
            var english = japaneseEntry.Senses.First().Glosses.First().Term;
            var kana = japaneseEntry.Readings.First().Text;
            var kanji = japaneseEntry.Kanjis.Any() ? japaneseEntry.Kanjis.First().Text : kana;
            return new Translation(english, kanji, kana);
        } 
    }
}
