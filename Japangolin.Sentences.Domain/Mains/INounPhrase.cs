namespace Wacton.Japangolin.Sentences.Domain.Mains
{
    using System.Collections.Generic;

    public interface INounPhrase
    {
        List<ITranslation> GetEnglishOrder();
        List<ITranslation> GetJapaneseOrder();
    }
}
