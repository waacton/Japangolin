namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public interface INounPhrase
    {
        Conjugation Conjugation { get; }

        List<ITranslation> GetEnglishOrder();
        List<ITranslation> GetJapaneseOrder();
    }
}
