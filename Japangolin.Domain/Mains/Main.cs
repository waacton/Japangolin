namespace Wacton.Japangolin.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;
    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Domain.Conjugation;
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Utils;
    using Wacton.Japangolin.Domain.Words;

    public class Main
    {
        public Word Word { get; private set; }
        public Inflection Inflection { get; private set; }
        public Hint Hint { get; private set; }
        public string AnswerKana { get; private set; }
        public string AnswerKanji { get; private set; }

        private readonly List<IJapaneseEntry> allEntries;
        private readonly List<IJapaneseEntry> jlptN5Entries;
        private readonly List<Inflection> allInflections = Enumeration.GetAll<Inflection>().ToList();

        private readonly Settings settings;

        public Main(List<IJapaneseEntry> japaneseEntries, Settings settings)
        {
            this.settings = settings;
            this.allEntries = japaneseEntries;
            this.jlptN5Entries = allEntries.Where(entry => JLPT.N5.Contains(entry.Sequence)).ToList();
        }

        internal void UpdateWordAndInflection()
        {
            this.Word = GetRandomWord();
            this.Inflection = RNG.SelectOne(this.allInflections);
            this.Hint = this.Inflection.GetHint(this.Word);
            (this.AnswerKana, this.AnswerKanji) = this.Inflection.Conjugate(this.Word);
        }

        private Word GetRandomWord()
        {
            var isValid = false;
            Word word = null;

            while (!isValid)
            {
                var entry = RNG.SelectOne(this.settings.WordFilter == WordFilter.JLPTN5 ? jlptN5Entries : allEntries);
                word = entry.ParseToWord();
                isValid = word.Class != WordClass.Unknown;
            }

            return word;
        }
    }
}
