namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Randomness;

    public class Main
    {
        private readonly IEnumerable<IJapaneseEntry> japaneseEntries;
        private readonly List<Conjugation> conjugations = Conjugation.KnownConjugations().ToList();
        private readonly List<ObjectBlockType> objectBlockTypes = new List<ObjectBlockType> { ObjectBlockType.Noun, ObjectBlockType.Verb, ObjectBlockType.Adjective };
        private readonly List<NounPhraseType> nounPhraseTypes = new List<NounPhraseType> { NounPhraseType.Simple, NounPhraseType.Modified };

        public Sentence Sentence { get; private set; }
        public Conjugation Conjugation { get; private set; }

        public ObjectBlockType ObjectBlockType { get; private set; }
        public NounPhraseType ObjectBlockNounPhraseType { get; private set; }

        public Main(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseEntries = japaneseDictionary.GetEntries();
            this.UpdateSentence();
        }

        public void UpdateSentence()
        {
            this.ObjectBlockType = RandomSelection.SelectOne(this.objectBlockTypes);
            this.ObjectBlockNounPhraseType = RandomSelection.SelectOne(this.nounPhraseTypes);

            this.Conjugation = RandomSelection.SelectOne(this.conjugations);
            var objectBlock = this.CreateObjectBlock();
            var topicBlock = this.CreateTopicBlock(objectBlock.HasVerb);
            this.Sentence = new Sentence(topicBlock, objectBlock);
        }

        private TopicBlock CreateTopicBlock(bool isVerbInSentence)
        {
            var nounPhrase = NounPhraseFactory.Simple(this.japaneseEntries);
            return new TopicBlock(nounPhrase, this.Conjugation, isVerbInSentence);
        }

        private ObjectBlock CreateObjectBlock()
        {
            switch (this.ObjectBlockType)
            {
                case ObjectBlockType.Noun:
                    return ObjectBlockFactory.Noun(this.japaneseEntries, this.Conjugation, this.ObjectBlockNounPhraseType);
                case ObjectBlockType.Verb:
                    return ObjectBlockFactory.Verb(this.japaneseEntries, this.Conjugation, this.ObjectBlockNounPhraseType);
                case ObjectBlockType.Adjective:
                    return ObjectBlockFactory.Adjective(this.japaneseEntries, this.Conjugation);
                default:
                    throw new ArgumentOutOfRangeException($"Object block type {this.ObjectBlockType} is not handled");
            }
        }
    }
}
