namespace Wacton.Japangolin.Domain
{
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public class Tokushuon : Enumeration
    {
        public static readonly Tokushuon A = new Tokushuon("A", 'ぁ', 'ァ');
        public static readonly Tokushuon I = new Tokushuon("I", 'ぃ', 'ィ');
        public static readonly Tokushuon U = new Tokushuon("U", 'ぅ', 'ゥ');
        public static readonly Tokushuon E = new Tokushuon("E", 'ぇ', 'ェ');
        public static readonly Tokushuon O = new Tokushuon("O", 'ぉ', 'ォ');
        public static readonly Tokushuon Wa = new Tokushuon("Wa", 'ゎ', 'ヮ');

        private static int counter;
        private string romaji;
        private readonly Dictionary<KanaSyllabary, char> dict;

        public Tokushuon(string romaji, char hiraganaTokushuon, char katakanaTokushuon)
            : base(counter, romaji)
        {
            counter++;
            this.romaji = romaji;
            this.dict = new Dictionary<KanaSyllabary, char>();
            this.dict.Add(KanaSyllabary.Hiragana, hiraganaTokushuon);
            this.dict.Add(KanaSyllabary.Katakana, katakanaTokushuon);
        }

        public char GetCharacter(KanaSyllabary kanaSyllabary)
        {
            return this.dict[kanaSyllabary];
        }

        // TODO: 'romaji' is probably not very accurate in this case, but will do to get things working
        public string GetRomaji()
        {
            return this.romaji;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
