namespace Wacton.Japangolin.Core.Mutations;

using Wacton.Japangolin.Core.Mains;

public class WordAndInflectionMutation : Mutation
{
    private readonly Main main;
        
    public WordAndInflectionMutation(Main main)
    {
        this.main = main;
    }
        
    protected override object[] Mutate()
    {
        main.UpdateWordAndInflection();
        return new object[] { main };
    }
}