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
        private readonly List<Conjugation> conjugations = Enumeration.GetAll<Conjugation>().Except(new List<Conjugation> { Conjugation.None }).ToList();

        public Sentence CurrentSentence { get; private set; }

        public Main(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseEntries = japaneseDictionary.GetEntries().ToList();
            this.UpdateSentence();
        }

        public void UpdateSentence()
        {
            var nouns = this.japaneseEntries.Where(entry => entry.Senses.Any(sense => sense.PartsOfSpeech.Contains(PartOfSpeech.NounCommon))).ToList();
            var conjugation = RandomSelection.SelectOne(this.conjugations);

            var topicBlock = GetTopicBlock(nouns, conjugation);
            var objectBlock = GetObjectBlock(nouns, conjugation);

            this.CurrentSentence = new Sentence(topicBlock, objectBlock);
        }

        private static TopicBlock GetTopicBlock(List<IJapaneseEntry> nouns, Conjugation conjugation)
        {
            var noun = RandomSelection.SelectOne(nouns);
            var nounPhrase = new SimpleNounPhrase(noun);
            return new TopicBlock(nounPhrase, conjugation);
        }

        private static ObjectBlock GetObjectBlock(List<IJapaneseEntry> nouns, Conjugation conjugation)
        {
            var targetNoun = RandomSelection.SelectOne(nouns);
            var modifyingNoun = RandomSelection.SelectOne(nouns);
            var nounPhrase = new ModifiedNounPhrase(targetNoun, modifyingNoun, conjugation);
            return new ObjectBlock(nounPhrase);
        }
    }
}
