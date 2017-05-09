namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Randomness;

    public class Main
    {
        private readonly List<IJapaneseEntry> japaneseEntries; 
        private IJapaneseEntry japaneseEntry;

        public string EnglishSentence { get; private set; }
        public string Help { get; private set; }
        public string KanaSentence { get; private set; }
        public string KanjiSentence { get; private set; }

        public Main(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseEntries = japaneseDictionary.GetEntries().ToList();
            this.UpdateSentence();
        }

        public void UpdateSentence()
        {
            //var nounType = RandomSelection.SelectOne(PartOfSpeech.Nouns);
            var nouns = this.japaneseEntries.Where(entry => entry.Senses.Any(sense => sense.PartsOfSpeech.Contains(PartOfSpeech.NounCommon)));

            var noun1 = RandomSelection.SelectOne(nouns);
            var noun2 = RandomSelection.SelectOne(nouns);

            try
            {
                var nounEnglish1 = noun1.Senses.First().Glosses.First().Term;
                var nounKana1 = noun1.Readings.First().Text;
                var nounKanji1 = noun1.Kanjis.Any() ? noun1.Kanjis.First().Text : nounKana1;

                var nounEnglish2 = noun2.Senses.First().Glosses.First().Term;
                var nounKana2 = noun2.Readings.First().Text;
                var nounKanji2 = noun2.Kanjis.Any() ? noun2.Kanjis.First().Text : nounKana2;

                this.EnglishSentence = $"[{nounEnglish1}] is [{nounEnglish2}].";
                this.Help = $"[{nounEnglish1}] => {nounKana1}   |   [{nounEnglish2}] => {nounKana2}";
                this.KanaSentence = $"{nounKana1}は{nounKana2}です。";
                this.KanjiSentence = $"{nounKanji1}は{nounKanji2}です。";
            }
            catch (Exception)
            {
                // shh...
            }

            this.japaneseEntry = RandomSelection.SelectOne(this.japaneseEntries);
        }
    }
}
