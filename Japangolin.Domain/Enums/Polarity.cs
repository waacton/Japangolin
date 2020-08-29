using Wacton.Tovarisch.Enum;

namespace Wacton.Japangolin.Domain.Enums
{
    public class Polarity : Enumeration
    {
        public static readonly Polarity None = new Polarity("None", -1);
        public static readonly Polarity Affirmative = new Polarity("Affirmative", 0);
        public static readonly Polarity Negative = new Polarity("Negative", 1);

        // ---

        public int Index { get; }

        public Polarity(string displayName, int index)
            : base(displayName)
        {
            this.Index = index;
        }
    }
}
