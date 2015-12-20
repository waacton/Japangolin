namespace Wacton.Japangolin.Domain.JapanesePhrases
{
    using System;
    using System.Collections.Generic;

    public class JapanesePhrase
    {
        public string Kana { get; private set; }
        public string Romaji { get; private set; }
        public List<string> Meaning { get; private set; }
        public List<string> Kanji { get; private set; }
        public int EntryId { get; private set; }

        public JapanesePhrase(string kana, string romaji, List<string> meaning, List<string> kanji, int entryId)
        {
            this.Kana = kana;
            this.Romaji = romaji;
            this.Meaning = meaning;
            this.Kanji = kanji;
            this.EntryId = entryId;
        }

        public override string ToString() => $"{this.Kana} : {this.Romaji}";
    }
}
