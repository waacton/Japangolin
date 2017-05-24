namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public interface INounPhrase
    {
        Conjugation Conjugation { get; }

        List<IGolin> GolinEnglish();
        List<IGolin> GolinJapanese();
    }
}
