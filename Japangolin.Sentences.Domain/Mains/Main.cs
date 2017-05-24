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

            // TODO: how to deal with all this conjugation getting passed around?
            var conjugation = RandomSelection.SelectOne(this.conjugations);
            var topicBlock = new TopicBlock(new SimpleNounPhrase(RandomSelection.SelectOne(nouns), conjugation));
            var objectBlock = new ObjectBlock(new ModifiedNounPhrase(RandomSelection.SelectOne(nouns), RandomSelection.SelectOne(nouns), conjugation));

            this.CurrentSentence = new Sentence(topicBlock, objectBlock);
        }
    }
}
