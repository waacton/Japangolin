namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Randomness;

    public class Main
    {
        private readonly IEnumerable<IJapaneseEntry> japaneseEntries;
        private readonly List<Conjugation> conjugations = Conjugation.KnownConjugations().ToList();

        public Sentence CurrentSentence { get; private set; }

        public Main(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseEntries = japaneseDictionary.GetEntries();
            this.UpdateSentence();
        }

        public void UpdateSentence()
        {
            var conjugation = RandomSelection.SelectOne(this.conjugations);

            var topicBlock = GetTopicBlock(this.japaneseEntries, conjugation);
            var objectBlock = GetObjectBlock(this.japaneseEntries, conjugation);

            this.CurrentSentence = new Sentence(topicBlock, objectBlock);
        }

        private static TopicBlock GetTopicBlock(IEnumerable<IJapaneseEntry> japaneseEntries, Conjugation conjugation)
        {
            var nounPhrase = CreateNounPhrase(japaneseEntries, false);
            return new TopicBlock(nounPhrase, conjugation);
        }

        private static ObjectBlock GetObjectBlock(IEnumerable<IJapaneseEntry> japaneseEntries, Conjugation conjugation)
        {
            var nounPhrase = CreateNounPhrase(japaneseEntries, true);

            var hasVerb = RandomSelection.IsSuccessful(0.5);
            if (hasVerb)
            {
                var verb = RandomSelection.SelectOne(japaneseEntries.GetVerbs());
                var verbGolin = GolinFactory.Verb(verb, conjugation);
                return new ObjectBlock(nounPhrase, verbGolin);
            }

            nounPhrase.SetConjugation(conjugation);
            return new ObjectBlock(nounPhrase);
        }

        private static NounPhrase CreateNounPhrase(IEnumerable<IJapaneseEntry> japaneseEntries, bool isModified)
        {
            var noun = RandomSelection.SelectOne(japaneseEntries.GetNouns());
            if (!isModified)
            {
                return new SimpleNounPhrase(noun);
            }

            var modifyingNoun = RandomSelection.SelectOne(japaneseEntries.GetNouns());
            return new ModifiedNounPhrase(noun, modifyingNoun);
        }
    }
}
