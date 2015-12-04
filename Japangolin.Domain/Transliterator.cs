namespace Japangolin.Domain
{
    using System.Collections.Generic;
    using System.Text;

    using Wacton.Tovarisch.Enum;

    public class Transliterator
    {
        private readonly Dictionary<char, Kana> kanas = new Dictionary<char, Kana>();
        private readonly Dictionary<char, Youon> youons = new Dictionary<char, Youon>();
        private readonly Dictionary<char, Chouon> chouons = new Dictionary<char, Chouon>();
        private readonly Dictionary<char, Tokushuon> tokushuons = new Dictionary<char, Tokushuon>();
        private readonly Dictionary<char, Sokuon> sokuons = new Dictionary<char, Sokuon>();
        private readonly Dictionary<char, Kurikaeshi> kurikaeshis = new Dictionary<char, Kurikaeshi>();
        private readonly Dictionary<char, string> punctuation = new Dictionary<char, string>(); 

        public Transliterator()
        {
            foreach (var kana in Enumeration.GetAll<Kana>())
            {
                this.kanas.Add(kana.GetCharacter(KanaSyllabary.Hiragana), kana);
                this.kanas.Add(kana.GetCharacter(KanaSyllabary.Katakana), kana);
            }

            foreach (var youon in Enumeration.GetAll<Youon>())
            {
                this.youons.Add(youon.GetCharacter(KanaSyllabary.Hiragana), youon);
                this.youons.Add(youon.GetCharacter(KanaSyllabary.Katakana), youon);
            }

            foreach (var chouon in Enumeration.GetAll<Chouon>())
            {
                this.chouons.Add(chouon.GetCharacter(KanaSyllabary.Hiragana), chouon);
                //this.chouons.Add(chouon.GetCharacter(KanaSyllabary.Katakana), chouon); - chouon is the same in both syllabaries
            }

            foreach (var tokushuon in Enumeration.GetAll<Tokushuon>())
            {
                this.tokushuons.Add(tokushuon.GetCharacter(KanaSyllabary.Hiragana), tokushuon);
                this.tokushuons.Add(tokushuon.GetCharacter(KanaSyllabary.Katakana), tokushuon);
            }

            foreach (var sokuon in Enumeration.GetAll<Sokuon>())
            {
                this.sokuons.Add(sokuon.GetCharacter(KanaSyllabary.Hiragana), sokuon);
                this.sokuons.Add(sokuon.GetCharacter(KanaSyllabary.Katakana), sokuon);
            }

            foreach (var kurikaeshi in Enumeration.GetAll<Kurikaeshi>())
            {
                this.kurikaeshis.Add(kurikaeshi.GetCharacter(KanaSyllabary.Hiragana), kurikaeshi);
                this.kurikaeshis.Add(kurikaeshi.GetCharacter(KanaSyllabary.Katakana), kurikaeshi);
            }

            this.punctuation.Add(' ', " ");
            this.punctuation.Add('・', "-");
            this.punctuation.Add('、', ", ");
            this.punctuation.Add('〜', "~");
        }

        private bool TryGetCharacter(string kanaCharacters, int index, out char character)
        {
            if (index >= kanaCharacters.Length)
            {
                character = '?';
                return false;
            }

            character = kanaCharacters[index];
            return true;
        }

        public string GetRomaji(string kanaCharacters)
        {
            var syllables = new List<string>();
            var i = 0;
            var isRunning = true;
            while (isRunning)
            {
                Kana kana = null;
                var chouon = false;
                var sokuon = false;
                Youon youon = null;
                Tokushuon tokushuon = null;

                string romaji;
                char currentKanaCharacter;
                isRunning = this.TryGetCharacter(kanaCharacters, i, out currentKanaCharacter);

                if (this.punctuation.ContainsKey(currentKanaCharacter) && isRunning)
                {
                    romaji = this.punctuation[currentKanaCharacter];
                    i++;
                    isRunning = this.TryGetCharacter(kanaCharacters, i, out currentKanaCharacter);
                }
                else
                {
                    isRunning = this.TryGetCharacter(kanaCharacters, i, out currentKanaCharacter);

                    if (this.sokuons.ContainsKey(currentKanaCharacter) && isRunning)
                    {
                        sokuon = true;
                        i++;
                        isRunning = this.TryGetCharacter(kanaCharacters, i, out currentKanaCharacter);
                    }

                    if (this.kanas.ContainsKey(currentKanaCharacter) && isRunning)
                    {
                        kana = this.kanas[currentKanaCharacter];
                        i++;
                        isRunning = this.TryGetCharacter(kanaCharacters, i, out currentKanaCharacter);
                    }

                    //if (this.kurikaeshis.ContainsKey(currentKanaCharacter) && isRunning)
                    //{
                    //    kurikaeshi = this.kurikaeshis[currentKanaCharacter];
                    //    i++;
                    //    isRunning = this.TryGetCharacter(kanaCharacters, i, out currentKanaCharacter);
                    //}
                    
                    if (this.youons.ContainsKey(currentKanaCharacter) && isRunning)
                    {
                        youon = this.youons[currentKanaCharacter];
                        i++;
                        isRunning = this.TryGetCharacter(kanaCharacters, i, out currentKanaCharacter);
                    }
                    else if (this.tokushuons.ContainsKey(currentKanaCharacter) && isRunning)
                    {
                        tokushuon = this.tokushuons[currentKanaCharacter];
                        i++;
                        isRunning = this.TryGetCharacter(kanaCharacters, i, out currentKanaCharacter);
                    }

                    if (this.chouons.ContainsKey(currentKanaCharacter) && isRunning)
                    {
                        chouon = true;
                        i++;
                        isRunning = this.TryGetCharacter(kanaCharacters, i, out currentKanaCharacter);
                    }

                    if (kana == null)
                    {
                        if (sokuon)
                        {
                            romaji = "'";
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var hasYouon = youon != null;
                        var hasTokushuon = tokushuon != null;

                        if (!hasYouon && !hasTokushuon)
                        {
                            romaji = kana.GetRomaji(chouon, sokuon);
                        }
                        else if (hasYouon)
                        {
                            romaji = kana.GetRomaji(youon, chouon, sokuon);
                        }
                        else
                        {
                            romaji = kana.GetRomaji(tokushuon, chouon, sokuon);
                        }
                    }
                }

                syllables.Add(romaji);
            }

            var stringBuilder = new StringBuilder();
            foreach (var syllable in syllables)
            {
                stringBuilder.Append(syllable);
            }

            return stringBuilder.ToString().ToLower();
        }
    }
}
