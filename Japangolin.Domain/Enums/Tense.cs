namespace Wacton.Japangolin.Domain.Enums
{
    using Wacton.Desu.Enums;

    public class Tense : Enumeration
    {
        public static readonly Tense None = new Tense("None", -1);
        public static readonly Tense Present = new Tense("Present", 0);
        public static readonly Tense Past = new Tense("Past", 1);

        // ---

        public int Index { get; }

        public Tense(string displayName, int index)
            : base(displayName)
        {
            this.Index = index;
        }
    }
}
