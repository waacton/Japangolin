namespace Wacton.Japangolin.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Domain.Conjugation;
    using Wacton.Japangolin.Domain.Enums;
    using Wacton.Japangolin.Domain.Words;
    using Wacton.Tovarisch.Enum;
    using Wacton.Tovarisch.Randomness;

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

        public Main(IJapaneseDictionary japaneseDictionary, Settings settings)
        {
            this.settings = settings;
            this.allEntries = japaneseDictionary.GetEntries().ToList();
            this.jlptN5Entries = allEntries.Where(entry => JLPT.N5.Contains(entry.Sequence)).ToList();
        }

        internal void UpdateWordAndInflection()
        {
            this.Word = GetRandomWord();
            this.Inflection = RandomSelection.SelectOne(this.allInflections);
            this.Hint = this.Inflection.GetHint(this.Word);
            (this.AnswerKana, this.AnswerKanji) = this.Inflection.Conjugate(this.Word);
        }

        private Word GetRandomWord()
        {
            var isValid = false;
            Word word = null;

            while (!isValid)
            {
                var entry = RandomSelection.SelectOne(this.settings.WordFilter == WordFilter.JLPTN5 ? jlptN5Entries : allEntries);
                word = entry.ParseToWord();
                isValid = word.Class != WordClass.Unknown;
            }

            return word;
        }
    }
}
