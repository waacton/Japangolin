namespace Wacton.Japangolin.Domain.Conjugation
{
    using System;

    public class Conjugator
    {
        public Func<string, string> Function { get; private set; }
        public Hint Hint { get; private set; }

        public Conjugator(Func<string, string> function, Hint hint)
        {
            this.Function = function;
            this.Hint = hint;
        }

        public string Conjugate(string text)
        {
            return this.Function(text);
        }
    }
}
