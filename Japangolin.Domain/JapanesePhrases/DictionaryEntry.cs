namespace Wacton.Japangolin.Domain.JapanesePhrases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DictionaryEntry
    {
        private bool isIdentifierSet;
        public int Identifier { get; private set; }
        public List<string> Kanji { get; }
        public List<string> Kana { get; }
        public List<string> English { get; }
        public List<string> SpeechMarkings { get; }
        public List<string> FieldMarkings { get; }
        public List<string> MiscellaneousMarkings { get; }

        public DictionaryEntry()
        {
            this.Kanji = new List<string>();
            this.Kana = new List<string>();
            this.English = new List<string>();
            this.SpeechMarkings = new List<string>();
            this.FieldMarkings = new List<string>();
            this.MiscellaneousMarkings = new List<string>();
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

        public void AddEnglish(string english)
        {
            this.English.Add(english);
        }

        public void AddSpeechMarking(string speechMarking)
        {
            this.SpeechMarkings.Add(speechMarking);
        }

        public void AddFieldMarking(string fieldMarking)
        {
            this.FieldMarkings.Add(fieldMarking);
        }

        public void AddMiscellaneousMarking(string miscellaneousMarking)
        {
            this.MiscellaneousMarkings.Add(miscellaneousMarking);
        }

        public override string ToString()
        {
            var stringbuilder = new StringBuilder();
            stringbuilder.Append(this.Identifier + ": ");

            foreach (var kanji in this.Kanji)
            {
                stringbuilder.Append(kanji + " | ");
            }

            foreach (var kana in this.Kana)
            {
                stringbuilder.Append(kana + " | ");
            }

            stringbuilder.Append(this.English.First());
            return stringbuilder.ToString();
        }
    }
}