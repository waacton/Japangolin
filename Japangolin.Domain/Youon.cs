namespace Japangolin.Domain
{
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public class Youon : Enumeration
    {
        public static readonly Youon Ya = new Youon("Ya", 'ゃ', 'ャ');
        public static readonly Youon Yu = new Youon("Yu", 'ゅ', 'ュ');
        public static readonly Youon Yo = new Youon("Yo", 'ょ', 'ョ');

        private static int counter;
        private string romaji;
        private readonly Dictionary<KanaSyllabary, char> dict; 

        public Youon(string romaji, char hiraganaYouon, char katakanaYouon)
            : base(counter, romaji)
        {
            counter++;
            this.romaji = romaji;
            this.dict = new Dictionary<KanaSyllabary, char>();
            this.dict.Add(KanaSyllabary.Hiragana, hiraganaYouon);
            this.dict.Add(KanaSyllabary.Katakana, katakanaYouon);
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
