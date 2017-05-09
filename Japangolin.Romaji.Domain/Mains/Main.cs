namespace Wacton.Japangolin.Romaji.Domain.Mains
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Japangolin.Romaji.Domain.JapanesePhrases;

    public class Main
    {
        private readonly IJapanesePhraseRepository japanesePhraseRepository;
        private JapanesePhrase currentJapanesePhrase;

        public string Kana => this.currentJapanesePhrase.Kana;
        public string Romaji => this.currentJapanesePhrase.Romaji;
        public List<string> Kanji => this.currentJapanesePhrase.Kanji;
        public string Meaning => this.currentJapanesePhrase.Meaning.First();

        public Main(IJapanesePhraseRepository japanesePhraseRepository)
        {
            this.japanesePhraseRepository = japanesePhraseRepository;
            this.UpdatePhrase();
        }

        public void UpdatePhrase()
        {
            this.currentJapanesePhrase = this.japanesePhraseRepository.GetRandomPhrase();
        }
    }
}
