namespace Wacton.Desu
{
    using System.Collections.Generic;

    public interface IJapaneseDictionary
    {
        IEnumerable<JapaneseDictionaryEntry> GetEntries();
    }
}
