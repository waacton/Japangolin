namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Collections;
    using Wacton.Tovarisch.Randomness;

    public class Main
    {
        private readonly List<IJapaneseEntry> japaneseEntries; 
        private IJapaneseEntry japaneseEntry;

        public string EnglishSentence { get; private set; }
        public string Help { get; private set; }
        public string KanaSentence { get; private set; }
        public string KanjiSentence { get; private set; }

        public List<Translation> Translations { get; private set; } 

        public Main(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseEntries = japaneseDictionary.GetEntries().ToList();
            this.UpdateSentence();
        }

        public void UpdateSentence()
        {
            var nouns = this.japaneseEntries.Where(entry => entry.Senses.Any(sense => sense.PartsOfSpeech.Contains(PartOfSpeech.NounCommon)));

            var noun1 = this.ConvertToTranslation(RandomSelection.SelectOne(nouns));
            var noun2 = this.ConvertToTranslation(RandomSelection.SelectOne(nouns));

            try
            {
                this.Translations = new List<Translation> { noun1, new Translation("is"), noun2 };
                this.EnglishSentence = string.Concat(this.Translations.ToDelimitedString(" "), ".");
                this.Help = $"[{noun1.English}] => {noun1.Kana}   |   [{noun2.English}] => {noun2.Kana}";
                this.KanaSentence = $"{noun1.Kana}は{noun2.Kana}です。";
                this.KanjiSentence = $"{noun1.Kanji}は{noun2.Kanji}です。";

            }
            catch (Exception)
            {
                // shh...
            }

            this.japaneseEntry = RandomSelection.SelectOne(this.japaneseEntries);
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
