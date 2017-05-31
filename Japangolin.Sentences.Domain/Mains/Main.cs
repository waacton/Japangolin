﻿namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Randomness;

    public class Main
    {
        private readonly List<IJapaneseEntry> japaneseEntries;
        private readonly List<Conjugation> conjugations = Conjugation.KnownConjugations().ToList();

        public Sentence CurrentSentence { get; private set; }

        public Main(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseEntries = japaneseDictionary.GetEntries().ToList();
            this.UpdateSentence();
        }

        public void UpdateSentence()
        {
            var nouns = this.japaneseEntries.Where(entry => entry.Senses.Any(sense => sense.PartsOfSpeech.Contains(PartOfSpeech.NounCommon))).ToList();
            var verbs = this.japaneseEntries.Where(entry => entry.Senses.Any(sense => sense.PartsOfSpeech.Contains(PartOfSpeech.Verb1))).ToList();

            var conjugation = RandomSelection.SelectOne(this.conjugations);

            var topicBlock = GetTopicBlock(nouns, conjugation);
            var objectBlock = GetObjectBlock(nouns, verbs, conjugation);

            this.CurrentSentence = new Sentence(topicBlock, objectBlock);
        }

        private static TopicBlock GetTopicBlock(List<IJapaneseEntry> nouns, Conjugation conjugation)
        {
            var noun = RandomSelection.SelectOne(nouns);
            var nounPhrase = new SimpleNounPhrase(noun);
            nounPhrase.SetConjugation(conjugation);
            return new TopicBlock(nounPhrase, conjugation);
        }

        private static ObjectBlock GetObjectBlock(List<IJapaneseEntry> nouns, List<IJapaneseEntry> verbs, Conjugation conjugation)
        {
            var targetNoun = RandomSelection.SelectOne(nouns);
            var modifyingNoun = RandomSelection.SelectOne(nouns);
            var nounPhrase = new ModifiedNounPhrase(targetNoun, modifyingNoun);

            var hasVerb = RandomSelection.IsSuccessful(0.5);
            if (hasVerb)
            {
                var verb = RandomSelection.SelectOne(verbs);
                var verbGolin = GolinFactory.Verb(verb, conjugation);
                return new ObjectBlock(nounPhrase, verbGolin);
            }

            nounPhrase.SetConjugation(conjugation);
            return new ObjectBlock(nounPhrase);
        }
    }
}
