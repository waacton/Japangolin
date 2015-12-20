namespace Wacton.Japangolin.Domain.DesignTime
{
    using System.Collections.Generic;

    using Wacton.Japangolin.Domain.JapanesePhrases;
    using Wacton.Tovarisch.Collections;

    public class DesignTimeJapanesePhraseProvider : IJapanesePhraseProvider
    {
        public JapanesePhrase GetRandomPhrase()
        {
            return new JapanesePhrase("ジャッパンゴリン", "jappangorin", "Japangolin".AsList(), new List<string>(), 65);
        }
    }
}