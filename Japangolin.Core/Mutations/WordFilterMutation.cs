namespace Wacton.Japangolin.Core.Mutations;

using Wacton.Japangolin.Core.Enums;
using Wacton.Japangolin.Core.Mains;

public class WordFilterMutation : Mutation<WordFilter>
{
    private readonly Settings settings;
        
    public WordFilterMutation(Settings settings)
    {
        this.settings = settings;
    }
        
    protected override object[] Mutate(WordFilter wordFilter)
    {
        settings.SetWordFilter(wordFilter);
        return new object[] { settings };
    }
}