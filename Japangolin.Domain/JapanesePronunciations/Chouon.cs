namespace Wacton.Japangolin.Domain.JapanesePronunciations
{
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public class Chouon : Enumeration
    {
        public static readonly Chouon VowelExtension = new Chouon("VowelExtension", 'ー', 'ー');

        private readonly Dictionary<Syllabary, char> dict;

        public Chouon(string friendlyString, char hiraganaTokushuon, char katakanaTokushuon)
            : base(friendlyString)
        {
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
