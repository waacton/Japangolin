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

        private readonly List<IJapaneseEntry> japaneseEntries;
        private readonly List<Inflection> allInflections = Enumeration.GetAll<Inflection>().ToList();

        public Main(IJapaneseDictionary japaneseDictionary)
        {
            this.japaneseEntries = japaneseDictionary.GetEntries()
                .Where(entry => JLPT.N5.Contains(entry.Sequence)).ToList();

            this.UpdateWordAndInflection();
        }

        public void UpdateWordAndInflection()
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
                var entry = RandomSelection.SelectOne(japaneseEntries);
                word = entry.ParseToWord();
                isValid = word.Class != WordClass.Unknown;
            }

            return word;
        }
    }
}
