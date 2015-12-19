namespace Wacton.Japangolin.Domain.Mains
{
    using System.Collections.Generic;

    using Wacton.Japangolin.Domain.JapanesePhrases;

    public class Main
    {
        private readonly JapanesePhraseProvider japanesePhraseProvider;
        private JapanesePhrase currentJapanesePhrase;

        public string Kana => this.currentJapanesePhrase.Kana;
        public string Romaji => this.currentJapanesePhrase.Romaji;
        public List<string> Kanji => this.currentJapanesePhrase.Kanji;
        public string Meaning => this.currentJapanesePhrase.Meaning;

        public Main(JapanesePhraseProvider japanesePhraseProvider)
        {
            this.japanesePhraseProvider = japanesePhraseProvider;
            this.UpdatePhrase();
        }

        public void UpdatePhrase()
        {
            this.currentJapanesePhrase = this.japanesePhraseProvider.GetRandomPhrase();
        }
    }
}
