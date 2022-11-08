namespace Wacton.Japangolin.UI.MVVM;

using System;
using System.Windows.Input;
    
public class RelayCommand : ICommand
{
    private readonly Action<object> execute;
    private readonly Predicate<object> canExecute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public void Execute(object parameter) => execute(parameter);
    public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}

public class RelayCommand<T> : ICommand
{
    private readonly Action<T> execute;
    private readonly Predicate<T> canExecute;

    public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    private static T ToTyped(object parameter) => (T) parameter;
    public void Execute(object parameter) => execute(ToTyped(parameter));
    public bool CanExecute(object parameter) => canExecute == null || canExecute(ToTyped(parameter));

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
    
// for C# 8.0+ applications (with nullable reference types feature)
// public class RelayCommand : ICommand
// {
//     private readonly Action<object> execute;
//     private readonly Predicate<object?>? canExecute;
//
//     public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
//     {
//         this.execute = execute;
//         this.canExecute = canExecute;
//     }
//
//     public void Execute(object? parameter) => execute(parameter ?? null);
//     public bool CanExecute(object? parameter) => canExecute == null || canExecute(parameter);
//
//     public event EventHandler? CanExecuteChanged
//     {
//         add => CommandManager.RequerySuggested += value;
//         remove => CommandManager.RequerySuggested -= value;
//     }
// }
//
// public class RelayCommand<T> : ICommand
// {
//     private readonly Action<T> execute;
//     private readonly Predicate<T?>? canExecute;
//
//     public RelayCommand(Action<T> execute, Predicate<T?>? canExecute = null)
//     {
//         this.execute = execute;
//         this.canExecute = canExecute;
//     }
//
//     private static T ToTyped(object? parameter) => (T) parameter!;
//     public void Execute(object? parameter) => execute(ToTyped(parameter));
//     public bool CanExecute(object? parameter) => canExecute == null || canExecute(ToTyped(parameter));
//
//     public event EventHandler? CanExecuteChanged
//     {
//         add => CommandManager.RequerySuggested += value;
//         remove => CommandManager.RequerySuggested -= value;
//     }
// }