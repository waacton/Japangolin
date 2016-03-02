namespace Wacton.Japangolin.Domain.JapanesePhrases
{
    public interface IJapanesePhraseRepository
    {
        int PhraseCount { get; }

        JapanesePhrase GetPhrase(int index);

        JapanesePhrase GetRandomPhrase();
    }
}
