namespace Wacton.Japangolin.Domain.MVVM
{
    using System.Threading.Tasks;

    public abstract class ModelChangeAction
    {
        private readonly ModelChangeNotifier modelChangeNotifier;
        private readonly object[] changedModels;

        protected ModelChangeAction(ModelChangeNotifier modelChangeNotifier, params object[] changedModels)
        {
            this.modelChangeNotifier = modelChangeNotifier;
            this.changedModels = changedModels;
        }

        protected abstract void Action();

        public void ExecuteAndNotify()
        {
            Action();
            modelChangeNotifier.Notify(changedModels);
        }
        
        public async Task ExecuteAndNotifyAsync()
        {
            await Task.Run(Action);
            modelChangeNotifier.Notify(changedModels);
        }
    }

    public abstract class ModelChangeAction<T>
    {
        private readonly ModelChangeNotifier modelChangeNotifier;
        private readonly object[] changedModels;

        protected ModelChangeAction(ModelChangeNotifier modelChangeNotifier, params object[] changedModels)
        {
            this.modelChangeNotifier = modelChangeNotifier;
            this.changedModels = changedModels;
        }

        protected abstract void Action(T parameter);
        
        public void ExecuteAndNotify(T parameter)
        {
            Action(parameter);
            modelChangeNotifier.Notify(changedModels);
        }
        
        public async Task ExecuteAndNotifyAsync(T parameter)
        {
            await Task.Run(() => Action(parameter));
            modelChangeNotifier.Notify(changedModels);
        }
    }
}