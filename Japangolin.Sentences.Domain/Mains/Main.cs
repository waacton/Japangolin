namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Randomness;

    using Enumeration = Wacton.Tovarisch.Enum.Enumeration;

    public class Main
    {
        private readonly List<IJapaneseEntry> japaneseEntries;
        private readonly List<Conjugation> conjugations = Enumeration.GetAll<Conjugation>().ToList();

        public Sentence CurrentSentence { get; private set; }

        public Main(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseEntries = japaneseDictionary.GetEntries().ToList();
            this.UpdateSentence();
        }

        public void UpdateSentence()
        {
            var nouns = this.japaneseEntries.Where(entry => entry.Senses.Any(sense => sense.PartsOfSpeech.Contains(PartOfSpeech.NounCommon))).ToList();

            var topicNounPhrase = new SimpleNounPhrase(RandomSelection.SelectOne(nouns));
            var objectNounPhrase = new ModifiedNounPhrase(RandomSelection.SelectOne(nouns), RandomSelection.SelectOne(nouns));
            var conjugation = RandomSelection.SelectOne(this.conjugations);

            this.CurrentSentence = new Sentence(topicNounPhrase, objectNounPhrase, conjugation);
        }
    }
}
