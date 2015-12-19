namespace Wacton.Japangolin.Domain.JapanesePronunciations
{
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public class Kurikaeshi : Enumeration
    {
        public static readonly Kurikaeshi SeionRepetition = new Kurikaeshi("SeionRepetition", 'ゝ', 'ゞ');
        public static readonly Kurikaeshi DakutenRepetition = new Kurikaeshi("DakutenRepetition", 'ヽ', 'ヾ');

        private static int counter;
        private readonly Dictionary<Syllabary, char> dict;

        public Kurikaeshi(string friendlyString, char hiraganaTokushuon, char katakanaTokushuon)
            : base(counter, friendlyString)
        {
            counter++;
            this.dict = new Dictionary<Syllabary, char>();
            this.dict.Add(Syllabary.Hiragana, hiraganaTokushuon);
            this.dict.Add(Syllabary.Katakana, katakanaTokushuon);
        }

        public char GetCharacter(Syllabary kanaSyllabary)
        {
            return this.dict[kanaSyllabary];
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
