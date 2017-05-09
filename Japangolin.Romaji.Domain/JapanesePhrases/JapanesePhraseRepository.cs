namespace Wacton.Japangolin.Romaji.Domain.JapanesePhrases
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Tovarisch.Randomness;

    public class JapanesePhraseRepository : IJapanesePhraseRepository
    {
        private readonly IJapaneseDictionary japaneseDictionary;
        private List<JapanesePhrase> japanesePhrases;

        public bool IsInitialised { get; private set; }
        public int PhraseCount { get; private set; }

        public JapanesePhraseRepository(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseDictionary = japaneseDictionary;
            this.Initialise();
        }

        public void Initialise()
        {
            var dictionaryEntries = this.japaneseDictionary.GetEntries();
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
                    var romaji = transliterator.GetRomaji(kana);
                    if (romaji == null)
                    {
                        unprocessed.Add(kana); // for debug purposes
                    }
                    else
                    {
                        phrases.Add(new JapanesePhrase(kana, romaji, meaning, kanjis, entryId));
                    }
                }
            }

            return phrases;
        }
    }
}