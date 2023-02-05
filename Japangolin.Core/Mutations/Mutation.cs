namespace Wacton.Japangolin.Core.Mutations;

using System;
using System.Threading.Tasks;

public abstract class Mutation
{
    protected abstract object[] Mutate();

    public void Execute() => Execute(_ => { });
    public void Execute(Action<object[]> onMutation)
    {
        var changedObjects = Mutate();
        onMutation(changedObjects);
    }
        
    public async Task ExecuteAsync() => await ExecuteAsync(_ => { });
    public async Task ExecuteAsync(Action<object[]> onMutation)
    {
        var changedObjects = await Task.Run(Mutate);
        onMutation(changedObjects);
    }
}

public abstract class Mutation<T>
{
    protected abstract object[] Mutate(T parameter);

    public void Execute(T parameter) => Execute(parameter, _ => { });
    public void Execute(T parameter, Action<object[]> onMutation)
    {
        var changedObjects = Mutate(parameter);
        onMutation(changedObjects);
    }
        
    public async Task ExecuteAsync(T parameter) => await ExecuteAsync(parameter, _ => { });
    public async Task ExecuteAsync(T parameter, Action<object[]> onMutation)
    {
        var changedObjects = await Task.Run(() => Mutate(parameter));
        onMutation(changedObjects);
    }
}