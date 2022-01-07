namespace Wacton.Japangolin.UI.MVVM
{
    using System.ComponentModel;
    using System.Windows;
    using Caliburn.Micro;
    using Wacton.Japangolin.Domain.MVVM;

    public abstract class ViewModelBase : PropertyChangedBase
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
                ModelChangeNotifier.Subscribe(watchedModel, NotifyAllPropertyBindings);
            }
        }

        protected virtual void NotifyAllPropertyBindings()
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                NotifyOfPropertyChange(property.Name);
            }
        }
    }

    public abstract class ViewModelBase<T> : ViewModelBase
    {
        protected ViewModelBase(ModelChangeNotifier modelChangeNotifier, T watchedModel)
            : base(modelChangeNotifier, watchedModel)
        {
        }
    }
}