namespace Wacton.Japangolin.Core.Enums;

using Wacton.Desu.Enums;

public class Polarity : Enumeration
{
    public static readonly Polarity None = new("None", -1);
    public static readonly Polarity Affirmative = new("Affirmative", 0);
    public static readonly Polarity Negative = new("Negative", 1);

    // ---

    public int Index { get; }

    private Polarity(string displayName, int index)
        : base(displayName)
    {
        Index = index;
    }
}