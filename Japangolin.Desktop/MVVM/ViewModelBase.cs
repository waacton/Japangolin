namespace Wacton.Japangolin.Desktop.MVVM;

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    private static bool IsRunningFromXamlDesigner => DesignerProperties.GetIsInDesignMode(new DependencyObject());

    protected ModelWatcher ModelWatcher { get; }

    private ViewModelBase()
    {
        
    }

    protected ViewModelBase(ModelWatcher modelWatcher, params object[] watchedModels) : this()
    {
        // don't bother hooking up model watcher if running from xaml designer - probably null anyway
        if (IsRunningFromXamlDesigner)
        {
            return;
        }

        ModelWatcher = modelWatcher;
        foreach (var watchedModel in watchedModels)
        {
            ModelWatcher.Subscribe(watchedModel, NotifyAboutAllProperties);
        }
    }
        
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }

    protected void NotifyAboutAllProperties() => OnPropertyChanged(null);
}

public abstract class ViewModelBase<T> : ViewModelBase
{
    protected ViewModelBase(ModelWatcher modelWatcher, T watchedModel)
        : base(modelWatcher, watchedModel)
    {
    }
}