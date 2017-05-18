namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System;
    using System.Collections.Generic;

    using Wacton.Tovarisch.Enum;

    public class SentenceBlockType : Enumeration
    {
        public static readonly SentenceBlockType Topic = new SentenceBlockType("Topic", "は", conjugation => EnglishTopicConjugations[conjugation]);
        public static readonly SentenceBlockType Object = new SentenceBlockType("Object", "を", conjugation => "the");

        public string JapaneseParticle { get; }

        private readonly Func<Conjugation, string> prepositionFunction;
        
        private static readonly Dictionary<Conjugation, string> EnglishTopicConjugations = 
            new Dictionary<Conjugation, string>
            {
                { Conjugation.LongPresentAffirmative, "is" },
                { Conjugation.LongPresentNegative, "is not" },
                { Conjugation.LongPastAffirmative, "was" },
                { Conjugation.LongPastNegative, "was not" },
                { Conjugation.LongFutureAffirmative, "will be" },
                { Conjugation.LongFutureNegative, "will not be" },
                { Conjugation.ShortPresentAffirmative, "is" },
                { Conjugation.ShortPresentNegative, "is not" },
                { Conjugation.ShortPastAffirmative, "was" },
                { Conjugation.ShortPastNegative, "was not" },
                { Conjugation.ShortFutureAffirmative, "will be" },
                { Conjugation.ShortFutureNegative, "will not be" }
            };

        public SentenceBlockType(string displayName, string japaneseParticle, Func<Conjugation, string> prepositionFunction) : base(displayName)
        {
            this.JapaneseParticle = japaneseParticle;
            this.prepositionFunction = prepositionFunction;
        }

        public string EnglishPreposition(Conjugation conjugation)
        {
            return this.prepositionFunction.Invoke(conjugation);
        }
    }
}
