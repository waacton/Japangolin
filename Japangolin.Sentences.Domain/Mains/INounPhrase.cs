namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public interface INounPhrase
    {
        List<IGolin> GolinEnglish();
        List<IGolin> GolinJapanese();
    }
}
