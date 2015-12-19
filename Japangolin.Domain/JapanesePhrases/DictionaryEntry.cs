namespace Wacton.Japangolin.Domain.JapanesePhrases
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DictionaryEntry
    {
        private bool isIdentifierSet;
        public int Identifier { get; private set; }
        public List<string> Kanji { get; private set; }
        public List<string> Kana { get; private set; }
        public string English { get; private set; }

        public List<string> Unprocessed { get; private set; }

        public DictionaryEntry()
        {
            this.Kanji = new List<string>();
            this.Kana = new List<string>();
            this.Unprocessed = new List<string>();
        }

        public void SetIdentifier(int identifier)
        {
            if (this.isIdentifierSet)
            {
                throw new InvalidOperationException("Identifier is already set");
            }

            this.Identifier = identifier;
            this.isIdentifierSet = true;
        }

        public void AddKanji(string kanji)
        {
            this.Kanji.Add(kanji);
        }

        public void AddKana(string kana)
        {
            if (!this.Kana.Contains(kana))
            {
                this.Kana.Add(kana);
            }
        }

        public void SetEnglish(string english)
        {
            this.English = english;
        }

        public override string ToString()
        {
            var stringbuilder = new StringBuilder();
            stringbuilder.Append(this.Identifier.ToString() + ": ");

            foreach (var kanji in this.Kanji)
            {
                stringbuilder.Append(kanji + " | ");
            }

            foreach (var kana in this.Kana)
            {
                stringbuilder.Append(kana + " | ");
            }

            stringbuilder.Append(this.English);
            return stringbuilder.ToString();
        }
    }
}