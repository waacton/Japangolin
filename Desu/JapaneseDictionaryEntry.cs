namespace Wacton.Desu
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Wacton.Tovarisch.Collections;

    public class JapaneseDictionaryEntry : IJapaneseDictionaryEntry
    {
        public int Sequence { get; set; }
        public IEnumerable<Kanji> Kanjis { get; private set; }
        public IEnumerable<Reading> Readings { get; private set; }
        public IEnumerable<Sense> Senses { get; private set; }

        internal Kanji CurrentKanji => this.Kanjis.Last();
        internal Reading CurrentReading => this.Readings.Last();
        internal Sense CurrentSense => this.Senses.Last();

        public JapaneseDictionaryEntry()
        {
            this.Kanjis = new List<Kanji>();
            this.Readings = new List<Reading>();
            this.Senses = new List<Sense>();
        }

        internal void StartNewKanji()
        {
            this.Kanjis = this.Kanjis.Append(new Kanji());
        }

        internal void StartNewReading()
        {
            this.Readings = this.Readings.Append(new Reading());
        }

        internal void StartNewSense()
        {
            this.Senses = this.Senses.Append(new Sense());
        }

        public override string ToString()
        {
            var stringbuilder = new StringBuilder();
            stringbuilder.Append($"#{this.Sequence} :: ");

            foreach (var kanji in this.Kanjis)
            {
                stringbuilder.Append(kanji.Text + " | ");
            }

            foreach (var reading in this.Readings)
            {
                stringbuilder.Append(reading.Text + " | ");
            }

            stringbuilder.Append(this.Senses.First().Glosses.First(gloss => gloss.Language.Equals(Language.English)).Term);
            return stringbuilder.ToString();
        }
    }
}