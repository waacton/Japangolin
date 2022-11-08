namespace Wacton.Japangolin.Domain.Enums
{
    using Wacton.Desu.Enums;

    public class Formality : Enumeration
    {
        public static readonly Formality None = new Formality("None", -1);
        public static readonly Formality Long = new Formality("Long", 0);
        public static readonly Formality Short = new Formality("Short", 1);

        // ---

        public int Index { get; }

        public Formality(string displayName, int index)
            : base(displayName)
        {
            Index = index;
        }
    }
}
