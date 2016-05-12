namespace Wacton.Japangolin.Domain.JapanesePhrases
{
    using System.Collections.Generic;

    using Wacton.Desu;
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

        private static List<JapanesePhrase> ProcessEntries(IEnumerable<JapaneseDictionaryEntry> dictionaryEntries)
        {
            var phrases = new List<JapanesePhrase>();
            var unprocessed = new List<string>(); // for debug purposes

            var transliterator = new Transliterator();
            foreach (var structure in dictionaryEntries)
            {
                var meaning = structure.Translations[Gloss.English];
                var kanji = structure.Kanji;
                var entryId = structure.Identifier;

                foreach (var kana in structure.Kana)
                {
                    var romaji = transliterator.GetRomaji(kana);
                    if (romaji == null)
                    {
                        unprocessed.Add(kana); // for debug purposes
                    }
                    else
                    {
                        phrases.Add(new JapanesePhrase(kana, romaji, meaning, kanji, entryId));
                    }
                }
            }

            return phrases;
        }
    }
}