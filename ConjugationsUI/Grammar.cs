namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System;
    using System.Linq;

    using ConjugationsUI;
    using Wacton.Tovarisch.Enum;

    public class Grammar : Enumeration
    {
        public static readonly Func<string, WordClass, string> Dictionary = (text, wordClass) => text;
        public static readonly Func<string, WordClass, string> Stem = (text, wordClass) => ConjugationFunctions2.GetStem(text, wordClass);
        public static readonly Func<string, WordClass, string> Te = (text, wordClass) => ConjugationFunctions2.GetTe(text, wordClass);

        public static readonly Grammar ThereExistsNonLiving = new Grammar("ThereExistsNonLiving", "{0}があります", Stem);
        public static readonly Grammar ThereExistsLiving = new Grammar("ThereExistsLiving", "{0}がいます", Stem);
        public static readonly Grammar Request = new Grammar("Request", "{0}ください", Te);
        public static readonly Grammar Permission = new Grammar("Permission", "{0}もいいです", Te);
        public static readonly Grammar Forbidden = new Grammar("Forbidden", "{0}はいけません", Te);
        public static readonly Grammar Joining = new Grammar("Joining", "{0}、{1}", Te, Dictionary);

        private readonly string format;
        private readonly Func<string, WordClass, string>[] conjugationFunctions;

        public string Details { get; private set; }
        public int RequiredWordDataCount { get; private set; }

        public Grammar(string displayName, string format, params Func<string, WordClass, string>[] conjugationFunctions) : base(displayName)
        {
            this.format = format;
            this.conjugationFunctions = conjugationFunctions;
            this.RequiredWordDataCount = this.format.Count(character => character.Equals('{')); // naive!

            this.Details = this.format;
            for (var i = 0; i < this.RequiredWordDataCount; i++)
            {
                var conjugationName = GetConjugationName(conjugationFunctions[i]);
                this.Details = this.Details.Replace("{" + i + "}", conjugationName);
            }

            if (this.conjugationFunctions.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }
        }

        public string Conjugate(params WordData[] wordDatas)
        {
            if (wordDatas.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }

            var conjugatedWords = wordDatas
                .Select((wordData, i) => this.conjugationFunctions[i](wordData.Text, wordData.Class))
                .ToArray();

            return string.Format(this.format, conjugatedWords);
        }

        private static string GetConjugationName(Func<string, WordClass, string> conjugationFunction)
        {
            if (conjugationFunction == Dictionary) { return "｛dict｝"; }
            if (conjugationFunction == Stem) { return "｛stem｝"; }
            if (conjugationFunction == Te) { return "｛～て｝"; }
            throw new InvalidOperationException();
        }

    }
}
