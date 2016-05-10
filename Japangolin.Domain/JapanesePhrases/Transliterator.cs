namespace Wacton.Japangolin.Domain.JapanesePhrases
{
    using System;
    using System.Collections.Generic;

    using Wacton.Japangolin.Domain.JapanesePronunciations;
    using Wacton.Tovarisch.Enum;

    public class Transliterator
    {
        private readonly Dictionary<char, Kana> kanas = new Dictionary<char, Kana>();
        private readonly Dictionary<char, Youon> youons = new Dictionary<char, Youon>();
        private readonly Dictionary<char, Chouon> chouons = new Dictionary<char, Chouon>();
        private readonly Dictionary<char, Tokushuon> tokushuons = new Dictionary<char, Tokushuon>();
        private readonly Dictionary<char, Sokuon> sokuons = new Dictionary<char, Sokuon>();
        private readonly Dictionary<char, Kurikaeshi> kurikaeshis = new Dictionary<char, Kurikaeshi>();
        private readonly Dictionary<char, string> punctuations = new Dictionary<char, string>(); 

        public Transliterator()
        {
            this.Initialise();
        }

        private void Initialise()
        {
            foreach (var kana in Enumeration.GetAll<Kana>())
            {
                this.kanas.Add(kana.GetCharacter(Syllabary.Hiragana), kana);
                this.kanas.Add(kana.GetCharacter(Syllabary.Katakana), kana);
            }

            foreach (var youon in Enumeration.GetAll<Youon>())
            {
                this.youons.Add(youon.GetCharacter(Syllabary.Hiragana), youon);
                this.youons.Add(youon.GetCharacter(Syllabary.Katakana), youon);
            }

            foreach (var chouon in Enumeration.GetAll<Chouon>())
            {
                this.chouons.Add(chouon.GetCharacter(Syllabary.Hiragana), chouon);
                //this.chouons.Add(chouon.GetCharacter(KanaSyllabary.Katakana), chouon); - chouon is the same in both syllabaries
            }

            foreach (var tokushuon in Enumeration.GetAll<Tokushuon>())
            {
                this.tokushuons.Add(tokushuon.GetCharacter(Syllabary.Hiragana), tokushuon);
                this.tokushuons.Add(tokushuon.GetCharacter(Syllabary.Katakana), tokushuon);
            }

            foreach (var sokuon in Enumeration.GetAll<Sokuon>())
            {
                this.sokuons.Add(sokuon.GetCharacter(Syllabary.Hiragana), sokuon);
                this.sokuons.Add(sokuon.GetCharacter(Syllabary.Katakana), sokuon);
            }

            foreach (var kurikaeshi in Enumeration.GetAll<Kurikaeshi>())
            {
                this.kurikaeshis.Add(kurikaeshi.GetCharacter(Syllabary.Hiragana), kurikaeshi);
                this.kurikaeshis.Add(kurikaeshi.GetCharacter(Syllabary.Katakana), kurikaeshi);
            }

            this.punctuations.Add(' ', " ");
            this.punctuations.Add('・', "-");
            this.punctuations.Add('、', ", ");
            this.punctuations.Add('〜', "~");
        }

        public string GetRomaji(string kanaCharacters)
        {
            var syllables = new List<string>();

            var i = 0;
            while (i < kanaCharacters.Length)
            {
                string romaji = null;

                var isPunctuation = Process(kanaCharacters, ref i, this.punctuations, punctuation => romaji = punctuation);
                if (isPunctuation)
                {
                    syllables.Add(romaji);
                    continue;
                }

                /* currently no entries contain a kurikaeshi (except the actual kurikaeshi entry, which just confuses things), so ignoring */
                var syllable = new JapaneseSyllable();
                Process(kanaCharacters, ref i, this.sokuons, sokuon => syllable.Sokuon = true);
                Process(kanaCharacters, ref i, this.kanas, kana => syllable.Kana = kana);
                //Process(kanaCharacters, ref i, this.kurikaeshis, kurikaeshi => syllable.Kurikaeshi = kurikaeshi); 
                Process(kanaCharacters, ref i, this.youons, youon => syllable.Youon = youon);
                Process(kanaCharacters, ref i, this.tokushuons, tokushuon => syllable.Tokushuon = tokushuon);
                Process(kanaCharacters, ref i, this.chouons, chouon => syllable.Chouon = true);

                // if romaji for this syllable is null, bail out
                // no point in dealing with the other syllables if part of the word is "null"
                romaji = syllable.GetRomaji();
                if (romaji == null)
                {
                    return null;
                }

                syllables.Add(romaji);
            }

            return string.Concat(syllables).ToLower();
        }

        private static bool Process<T>(string kanaCharacters, ref int index, IReadOnlyDictionary<char, T> dictionary, Action<T> onFoundAction)
        {
            var character = GetCharacter(kanaCharacters, index);
            if (!character.HasValue)
            {
                return false;
            }

            if (!dictionary.ContainsKey(character.Value))
            {
                return false;
            }

            onFoundAction(dictionary[character.Value]);
            index++;
            return true;
        }

        private static char? GetCharacter(string kanaCharacters, int index)
        {
            if (index >= kanaCharacters.Length)
            {
                return null;
            }

            return kanaCharacters[index];
        }
    }
}
