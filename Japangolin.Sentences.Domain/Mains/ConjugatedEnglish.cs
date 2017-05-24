namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public class ConjugatedEnglish
    {
        public string EnglishBase { get; }
        public Conjugation Conjugation { get; }

        public virtual string EnglishConjugated { get; }

        public ConjugatedEnglish(string englishBase, Conjugation conjugation)
        {
            this.EnglishBase = englishBase;
            this.Conjugation = conjugation;
            this.EnglishConjugated = this.EnglishBase;
        }
    }

    public class TopicEnglish : ConjugatedEnglish
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

    public class NounEnglish : ConjugatedEnglish
    {
        public NounEnglish(string english, Conjugation conjugation) : base(english, conjugation)
        {
        }
    }
}
