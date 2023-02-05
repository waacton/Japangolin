namespace Wacton.Japangolin.Core.Conjugation;

using System;

public class Conjugator
{
    public Func<string, string> Function { get; }
    public Hint Hint { get; }

    public Conjugator(Func<string, string> function, Hint hint)
    {
        Function = function;
        Hint = hint;
    }

    public string Conjugate(string text)
    {
        return Function(text);
    }
}