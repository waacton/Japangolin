namespace Wacton.Japangolin.Domain.JapanesePhrases
{
    using System;

    using Wacton.Japangolin.Domain.JapanesePronunciations;

    public class JapaneseSyllable
    {
        public Kana Kana;
        public Kurikaeshi Kurikaeshi;
        public bool Chouon;
        public bool Sokuon;
        public Youon Youon;
        public Tokushuon Tokushuon;

        private bool HasYouon => this.Youon != null;
        private bool HasTokushuon => this.Tokushuon != null;

        public string GetRomaji()
        {
            string romaji;
            if (this.Kana == null)
            {
                if (this.Sokuon)
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
                if (this.HasYouon && this.HasTokushuon)
                {
                    throw new InvalidOperationException("Unable to represent in romaji a syllable that contains youon and tokushuon");
                }

                if (!this.HasYouon && !this.HasTokushuon)
                {
                    romaji = this.Kana.GetRomaji(this.Chouon, this.Sokuon);
                }
                else if (this.HasYouon)
                {
                    romaji = this.Kana.GetRomaji(this.Youon, this.Chouon, this.Sokuon);
                }
                else
                {
                    romaji = this.Kana.GetRomaji(this.Tokushuon, this.Chouon, this.Sokuon);
                }
            }

            return romaji;
        }

        public override string ToString()
        {
            return this.GetRomaji();
        }
    }
}
