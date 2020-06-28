namespace Wacton.Japangolin.Grammar
{
    using System;
    using System.Collections.Generic;

    public static class WordClassesExtensions
    {
        public static WordClassConjugator Link(this List<WordClass> wordClasses, Func<WordClass, Conjugator> conjugator)
        {
            return new WordClassConjugator(wordClasses, conjugator);
        }
    }
}
