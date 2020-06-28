namespace Wacton.Japangolin.Grammar
{
    using System;

    public class Conjugator
    {
        public Func<string, string> Function { get; private set; }
        public string DetailedInfo { get; private set; }
        public string AbstractInfo { get; private set; }

        public Conjugator(Func<string, string> function, string detailedInfo, string abstractInfo)
        {
            this.Function = function;
            this.DetailedInfo = detailedInfo;
            this.AbstractInfo = abstractInfo;
        }

        public string Conjugate(string text)
        {
            return this.Function(text);
        }
    }
}
