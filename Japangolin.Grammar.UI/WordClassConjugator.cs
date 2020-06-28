namespace Wacton.Japangolin.Grammar
{
    using System;
    using System.Collections.Generic;

    public class WordClassConjugator
    {
        public List<WordClass> WordClasses { get; private set; }
        public Func<WordClass, Conjugator> Conjugator { get; private set; }

        public WordClassConjugator(List<WordClass> wordClasses, Func<WordClass, Conjugator> conjugator)
        {
            this.WordClasses = wordClasses;
            this.Conjugator = conjugator;
        }

        public string Conjugate(string text, WordClass wordClass)
        {
            if (!this.WordClasses.Contains(wordClass))
            {
                throw new InvalidOperationException($"Cannot conjugate {text} - this conjugator is not expecting {wordClass}");
            }

            return this.Conjugator(wordClass).Conjugate(text);
        }

        public Conjugator GetConjugator(WordClass wordClass)
        {
            if (!this.WordClasses.Contains(wordClass))
            {
                throw new InvalidOperationException($"This conjugator is not expecting {wordClass}");
            }

            return this.Conjugator(wordClass);
        }

        public override string ToString() => string.Join(", ", this.WordClasses);
    }
}
