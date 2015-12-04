namespace Japangolin.Domain
{
    using System.Collections.Generic;

    public class TransliteratedKana
    {
        public string Kana { get; private set; }
        public string Romaji { get; private set; }
        public string Meaning { get; private set; }
        public List<string> Kanji { get; private set; }
        public int EntryId { get; private set; }

        public TransliteratedKana(string kana, string romaji, string meaning, List<string> kanji, int entryId)
        {
            this.Kana = kana;
            this.Romaji = romaji;
            this.Meaning = meaning;
            this.Kanji = kanji;
            this.EntryId = entryId;
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", this.Kana, this.Romaji);
        }
    }
}
