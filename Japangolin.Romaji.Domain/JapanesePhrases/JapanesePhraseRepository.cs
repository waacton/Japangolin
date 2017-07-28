﻿namespace Wacton.Japangolin.Romaji.Domain.JapanesePhrases
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Desu.Romaji;
    using Wacton.Tovarisch.Randomness;

    public class JapanesePhraseRepository : IJapanesePhraseRepository
    {
        private List<JapanesePhrase> japanesePhrases;

        public bool IsInitialised { get; private set; }
        public int PhraseCount { get; private set; }

        public JapanesePhraseRepository()
        {
            this.Initialise();
        }

        public void Initialise()
        {
            var japaneseDictionary = new JapaneseDictionary();
            var dictionaryEntries = japaneseDictionary.GetEntries();
            this.japanesePhrases = ProcessEntries(dictionaryEntries);
            this.PhraseCount = this.japanesePhrases.Count;
            this.IsInitialised = true;
        }

        public JapanesePhrase GetPhrase(int index)
        {
            return index < this.PhraseCount ? this.japanesePhrases[index] : null;
        }

        public JapanesePhrase GetRandomPhrase()
        {
            return RandomSelection.SelectOne(this.japanesePhrases);
        }

        private static List<JapanesePhrase> ProcessEntries(IEnumerable<IJapaneseEntry> dictionaryEntries)
        {
            var phrases = new List<JapanesePhrase>();
            var unprocessed = new List<string>(); // for debug purposes

            var transliterator = new Transliterator();
            foreach (var entry in dictionaryEntries)
            {
                var meaning = entry.Senses.First().Glosses.Where(gloss => gloss.Language.Equals(Language.English)).Select(gloss => gloss.Term).ToList();
                var kanjis = entry.Kanjis.Select(kanji => kanji.Text).ToList();
                var entryId = entry.Sequence;

                foreach (var reading in entry.Readings)
                {
                    var kana = reading.Text;
                    string romaji;
                    try
                    {
                        romaji = transliterator.GetRomaji(kana);
                    }
                    catch (TransliterationException)
                    {
                        unprocessed.Add(kana); // for debug purposes
                        continue;
                    }

                    phrases.Add(new JapanesePhrase(kana, romaji, meaning, kanjis, entryId));
                }
            }

            return phrases;
        }
    }
}