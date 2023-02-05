namespace Wacton.Japangolin.Core.Mains;

using Wacton.Japangolin.Core.Enums;

public class Settings
{
    public WordFilter WordFilter { get; private set; }

    public Settings()
    {
        WordFilter = WordFilter.JLPTN5;
    }

    internal void SetWordFilter(WordFilter wordFilter)
    {
        WordFilter = wordFilter;
    }
}