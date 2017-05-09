namespace Wacton.Japangolin.Romaji.Domain.JapanesePronunciations
{
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public class Youon : Enumeration
    {
        public static readonly Youon Ya = new Youon("Ya", 'ゃ', 'ャ');
        public static readonly Youon Yu = new Youon("Yu", 'ゅ', 'ュ');
        public static readonly Youon Yo = new Youon("Yo", 'ょ', 'ョ');

        private string romaji;
        private readonly Dictionary<Syllabary, char> dict; 

        public Youon(string romaji, char hiraganaYouon, char katakanaYouon)
            : base(romaji)
        {
            this.romaji = romaji;
            this.dict = new Dictionary<Syllabary, char>();
            this.dict.Add(Syllabary.Hiragana, hiraganaYouon);
            this.dict.Add(Syllabary.Katakana, katakanaYouon);
        }

        public char GetCharacter(Syllabary kanaSyllabary)
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
