namespace Wacton.Japangolin.Conjugation
{
    using System;
    using System.Collections.Generic;

    public class WordClassConjugator
    {
        public List<WordClass> WordClasses { get; private set; }
        public Func<WordClass, Conjugator> ConjugatorByWordClass { get; private set; }

        public WordClassConjugator(List<WordClass> wordClasses, Func<WordClass, Conjugator> conjugatorByWordClass)
        {
            this.WordClasses = wordClasses;
            this.ConjugatorByWordClass = conjugatorByWordClass;
        }

        public string Conjugate(string text, WordClass wordClass)
        {
            if (!this.WordClasses.Contains(wordClass))
            {
                throw new InvalidOperationException($"Cannot conjugate {text} - this conjugator is not expecting {wordClass}");
            }

            return this.ConjugatorByWordClass(wordClass).Conjugate(text);
        }

        public Conjugator GetConjugator(WordClass wordClass)
        {
            if (!this.WordClasses.Contains(wordClass))
            {
                throw new InvalidOperationException($"This conjugator is not expecting {wordClass}");
            }

            return this.ConjugatorByWordClass(wordClass);
        }

        public override string ToString() => string.Join(" | ", this.WordClasses);
    }
}
