namespace Wacton.Japangolin.Conjugation
{
    using System.Collections.Generic;

    public interface IModifier
    {
        string Variation { get; }
        int RequiredWordDataCount { get; }
        bool IsHighLevel { get; }

        List<List<WordClass>> GetRequiredWordClasses();
        string Conjugate(params WordData[] wordDatas);
        string Information(params WordData[] wordDatas);
    }
}
