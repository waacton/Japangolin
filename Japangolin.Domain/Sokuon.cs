namespace Wacton.Japangolin.Domain
{
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public class Sokuon : Enumeration
    {
        public static readonly Sokuon ConsonantGemination = new Sokuon("ConsonantGemination", 'っ', 'ッ');

        private static int counter;
        private readonly Dictionary<KanaSyllabary, char> dict;

        public Sokuon(string friendlyString, char hiraganaTokushuon, char katakanaTokushuon)
            : base(counter, friendlyString)
        {
            counter++;
            this.dict = new Dictionary<KanaSyllabary, char>();
            this.dict.Add(KanaSyllabary.Hiragana, hiraganaTokushuon);
            this.dict.Add(KanaSyllabary.Katakana, katakanaTokushuon);
        }

        public char GetCharacter(KanaSyllabary kanaSyllabary)
        {
            return this.dict[kanaSyllabary];
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
