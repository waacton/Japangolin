namespace Wacton.Desu
{
    using System.Collections.Generic;

    public interface IJapaneseDictionaryEntry
    {
        int Sequence { get; }
        IEnumerable<Kanji> Kanjis { get; }
        IEnumerable<Reading> Readings { get; }
        IEnumerable<Sense> Senses { get; }
    }
}
