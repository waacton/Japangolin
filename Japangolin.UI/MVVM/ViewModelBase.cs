namespace Wacton.Japangolin.UI.MVVM
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using Wacton.Japangolin.Domain.MVVM;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private static bool IsRunningFromXamlDesigner => DesignerProperties.GetIsInDesignMode(new DependencyObject());

        protected ModelChangeNotifier ModelChangeNotifier { get; }

        protected ViewModelBase(ModelChangeNotifier modelChangeNotifier, params object[] watchedModels)
        {
            // don't bother hooking up model change notifier if running from xaml designer - probably null anyway
            if (IsRunningFromXamlDesigner)
            {
                return;
            }

            ModelChangeNotifier = modelChangeNotifier;
            foreach (var watchedModel in watchedModels)
            {
                ModelChangeNotifier.Subscribe(watchedModel, NotifyAboutAllProperties);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        
        protected void SetField<T>(ref T field, T value, params string[] otherPropertiesToNotifyAbout)
        {
            var setPropertyResult = SetField(ref field, value);
            if (!setPropertyResult) return;

            foreach (var otherProperty in otherPropertiesToNotifyAbout)
            {
                OnPropertyChanged(otherProperty);
            }
        } 

        protected void NotifyAboutAllProperties() => OnPropertyChanged(null!);
    }

    public abstract class ViewModelBase<T> : ViewModelBase
    {
        protected ViewModelBase(ModelChangeNotifier modelChangeNotifier, T watchedModel)
            : base(modelChangeNotifier, watchedModel)
        {
        }
    }
}