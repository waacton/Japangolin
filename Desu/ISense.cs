﻿namespace Wacton.Desu
{
    using System.Collections.Generic;

    public interface ISense
    {
        IEnumerable<string> KanjiRestriction { get; }
        IEnumerable<string> ReadingRestriction { get; }
        IEnumerable<PartOfSpeech> PartsOfSpeech { get; }
        IEnumerable<string> CrossReferences { get; }
        IEnumerable<string> Antonyms { get; }
        IEnumerable<Field> Fields { get; }
        IEnumerable<Miscellaneous> Miscellanea { get; }
        IEnumerable<string> Informations { get; }
        IEnumerable<LoanwordGloss> LoanwordSources { get; }
        IEnumerable<Dialect> Dialects { get; }
        IEnumerable<Gloss> Glosses { get; }
    }
}
