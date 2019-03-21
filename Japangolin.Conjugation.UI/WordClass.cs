namespace Wacton.Japangolin.Conjugation
{
    using System;
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public class WordClass : Enumeration
    {
        public static readonly WordClass None = new WordClass("None", ConjugationFunctions.Defaults, ConjugationInformations.Defaults);
        public static readonly WordClass JapaneseNoun = new WordClass("JapaneseNoun", ConjugationFunctions.JapaneseNoun, ConjugationInformations.JapaneseNoun);
        public static readonly WordClass JapaneseVerbIchidan = new WordClass("JapaneseVerbIchidan", ConjugationFunctions.JapaneseVerbIchidan, ConjugationInformations.JapaneseVerbIchidan);
        public static readonly WordClass JapaneseVerbGodan = new WordClass("JapaneseVerbGodan", ConjugationFunctions.JapaneseVerbGodan, ConjugationInformations.JapaneseVerbGodan);
        public static readonly WordClass JapaneseAdjectiveI = new WordClass("JapaneseAdjectiveI", ConjugationFunctions.JapaneseAdjectiveI, ConjugationInformations.JapaneseAdjectiveI);
        public static readonly WordClass JapaneseAdjectiveNa = new WordClass("JapaneseAdjectiveNa", ConjugationFunctions.JapaneseAdjectiveNa, ConjugationInformations.JapaneseAdjectiveNa);
        public static readonly WordClass EnglishTopicPrepositionsWithoutVerb = new WordClass("EnglishTopicPrepositionsWithoutVerb", ConjugationFunctions.EnglishTopicPrepositionsWithoutVerb, ConjugationInformations.Defaults);
        public static readonly WordClass EnglishTopicPrepositionsWithVerb = new WordClass("EnglishTopicPrepositionsWithVerb", ConjugationFunctions.EnglishTopicPrepositionsWithVerb, ConjugationInformations.Defaults);

        private readonly Dictionary<Conjugation, Func<string, string>> conjugationFunctions;
        private readonly Dictionary<Conjugation, Func<string>> conjugationInformations;

        public WordClass(
            string displayName, 
            Dictionary<Conjugation, Func<string, string>> conjugationFunctions, 
            Dictionary<Conjugation, Func<string>> conjugationInformations) 
            : base(displayName)
        {
            this.conjugationFunctions = conjugationFunctions;
            this.conjugationInformations = conjugationInformations;
        }

        public string GetConjugation(string word, Conjugation conjugation)
        {
            return this.conjugationFunctions[conjugation].Invoke(word);
        }

        public string GetConjugationInformation(Conjugation conjugation)
        {
            return this.conjugationInformations[conjugation].Invoke();
        }
    }
}
