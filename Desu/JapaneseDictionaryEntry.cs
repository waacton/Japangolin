namespace Wacton.Desu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // TODO: needs updating to better reflect JMdict structure (as part of Project Desu)
    public class JapaneseDictionaryEntry
    {
        private bool isIdentifierSet;
        public int Identifier { get; private set; }
        public List<string> Kanji { get; }
        public List<string> Kana { get; }
        public Dictionary<Gloss, List<string>> Translations { get; }
        public List<string> SpeechMarkings { get; }
        public List<string> FieldMarkings { get; }
        public List<string> MiscellaneousMarkings { get; }

        public JapaneseDictionaryEntry()
        {
            this.Kanji = new List<string>();
            this.Kana = new List<string>();
            this.Translations = new Dictionary<Gloss, List<string>>();
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

        public void AddTranslation(Gloss gloss, string translation)
        {
            if (!this.Translations.ContainsKey(gloss))
            {
                this.Translations.Add(gloss, new List<string>());
            }

            this.Translations[gloss].Add(translation);
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

            stringbuilder.Append(this.Translations[Gloss.English].First());
            return stringbuilder.ToString();
        }
    }
}