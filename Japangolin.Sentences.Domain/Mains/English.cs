namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public abstract class English
    {
        public string EnglishBase { get; }
        public Conjugation Conjugation { get; }

        public virtual string EnglishConjugated => this.EnglishBase;

        protected English(string englishBase, Conjugation conjugation)
        {
            this.EnglishBase = englishBase;
            this.Conjugation = conjugation;
        }
    }

    public class UnconjugatedEnglish : English
    {
        public UnconjugatedEnglish(string english) : base(english, Conjugation.None)
        {
        }
    }

    public class TopicEnglish : English
    {
        private static readonly Dictionary<Conjugation, string> Prepositions =
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

        public override string EnglishConjugated => Prepositions[this.Conjugation];

        public TopicEnglish(Conjugation conjugation) : base("is", conjugation)
        {
        }
    }
}
