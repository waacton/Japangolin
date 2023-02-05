namespace Wacton.Japangolin.Core.Enums;

using Wacton.Desu.Enums;

public class Tense : Enumeration
{
    public static readonly Tense None = new("None", -1);
    public static readonly Tense Present = new("Present", 0);
    public static readonly Tense Past = new("Past", 1);

    // ---

    public int Index { get; }

    private Tense(string displayName, int index)
        : base(displayName)
    {
        Index = index;
    }
}