namespace Wacton.Japangolin.Core.Enums;

using Wacton.Desu.Enums;

public class Formality : Enumeration
{
    public static readonly Formality None = new("None", -1);
    public static readonly Formality Long = new("Long", 0);
    public static readonly Formality Short = new("Short", 1);

    // ---

    public int Index { get; }

    private Formality(string displayName, int index)
        : base(displayName)
    {
        Index = index;
    }
}