namespace Wacton.Japangolin.Domain.DesignTime
{
    using System.Collections.Generic;

    using Wacton.Japangolin.Domain.JapanesePhrases;
    using Wacton.Tovarisch.Collections;

    public class DesignTimeJapanesePhraseRepository : IJapanesePhraseRepository
    {
        private readonly JapanesePhrase designTimePhrase = new JapanesePhrase("ジャッパンゴリン", "jappangorin", "Japangolin".AsList(), new List<string>(), 65);

        public int PhraseCount => 1;

        public JapanesePhrase GetPhrase(int index)
        {
            return this.designTimePhrase;
        }

        public JapanesePhrase GetRandomPhrase()
        {
            return this.designTimePhrase;
        }

    }
}