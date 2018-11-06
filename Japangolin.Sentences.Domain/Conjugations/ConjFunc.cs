namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System;

    public class ConjFunc
    {
        public Func<string, string> ConjugationFunc { get; private set; }
        public string Information { get; private set; }

        public ConjFunc(Func<string, string> conjugationFunc, string information)
        {
            this.ConjugationFunc = conjugationFunc;
            this.Information = information;
        }

        public string Conjugate(string text)
        {
            return this.ConjugationFunc(text);
        }
    }
}
