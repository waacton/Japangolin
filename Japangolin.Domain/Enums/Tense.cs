using Wacton.Tovarisch.Enum;

namespace Wacton.Japangolin.Domain.Enums
{
    public class Tense : Enumeration
    {
        public static readonly Tense None = new Tense("None", -1);
        public static readonly Tense Present = new Tense("Present", 0);
        public static readonly Tense Past = new Tense("Past", 1);
        public static readonly Tense Future = new Tense("Future", 2);

        // ---

        public int Index { get; }

        public Tense(string displayName, int index)
            : base(displayName)
        {
            this.Index = index;
        }
    }
}
