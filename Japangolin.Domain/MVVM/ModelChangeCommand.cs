namespace Wacton.Japangolin.Domain.MVVM
{
    using System;
    using System.Threading.Tasks;

    /* 
     * NOTE:
     * pre-async/await, exceptions thrown on background threads were propagated using SynchronizationContext:
     * --- this.mainContext.Post(state => task.Wait(), null)
     * however, this seems to cause a TaskCanceledException when used with 'await'
     * post-async/await, exceptions will only be propagated if the calling method has used 'await' (as intended)
     */

    public abstract class ModelChangeCommand
    {
        private readonly ModelChangeNotifier modelChangeNotifier;
        private readonly object[] changedModels;

        protected ModelChangeCommand(ModelChangeNotifier modelChangeNotifier, params object[] changedModels)
        {
            this.modelChangeNotifier = modelChangeNotifier;
            this.changedModels = changedModels;
        }

        protected abstract void Action();

        /// <summary>
        /// <para> runs the command on the calling thread, then notifies of model change </para>
        /// <para> calling method will pause execution </para>
        /// </summary>
        public void ExecuteAndNotify()
        {
            var task = Execute();
            if (task.IsFaulted)
            {
                throw task.Exception ?? new AggregateException("Task faulted (but null exception)");
            }

            Notify();
        }

        /// <summary>
        /// <para> runs the command on a background thread, then notifies of model change </para>
        /// <para> calling method will pause execution if 'await' is used, otherwise execution will continue immediately </para>
        /// </summary>
        public async Task ExecuteAndNotifyAsync()
        {
            await Task.Run(Execute).ContinueWith(task => Notify(), TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        // not convinced that exceptions are caught when the action uses async commands
        private Task Execute()
        {
            try
            {
                Action();
            }
            catch (Exception exception)
            {
                return Task.FromException(exception);
            }

            return Task.CompletedTask;
        }

        private Task Notify()
        {
            modelChangeNotifier.Notify(changedModels);
            return Task.CompletedTask;
        }

        protected virtual string CommandName()
        {
            return GetType().Name;
        }
    }

    public abstract class ModelChangeCommand<T>
    {
        private readonly ModelChangeNotifier modelChangeNotifier;
        private readonly object[] changedModels;

        protected ModelChangeCommand(ModelChangeNotifier modelChangeNotifier, params object[] changedModels)
        {
            this.modelChangeNotifier = modelChangeNotifier;
            this.changedModels = changedModels;
        }

        protected abstract void Action(T parameter);

        /// <summary>
        /// <para> runs the command on the calling thread, then notifies of model change </para>
        /// <para> calling method will pause execution </para>
        /// </summary>
        public void ExecuteAndNotify(T parameter)
        {
            var task = Execute(parameter);
            if (task.IsFaulted)
            {
                throw task.Exception ?? new AggregateException("Task faulted (but null exception)");
            }

            Notify();
        }

        /// <summary>
        /// <para> runs the command on a background thread, then notifies of model change </para>
        /// <para> calling method will pause execution if 'await' is used, otherwise execution will continue immediately </para>
        /// </summary>
        public async Task ExecuteAndNotifyAsync(T parameter)
        {
            await Task.Run(() => Execute(parameter)).ContinueWith(task => Notify(), TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        // TODO: not convinced that exceptions are caught when the action uses async commands
        private Task Execute(T parameter)
        {
            try
            {
                Action(parameter);
            }
            catch (Exception exception)
            {
                return Task.FromException(exception);
            }

            return Task.CompletedTask;
        }

        private Task Notify()
        {
            modelChangeNotifier.Notify(changedModels);
            return Task.CompletedTask;
        }

        protected virtual string CommandName(T parameter)
        {
            return GetType().Name + ": " + parameter;
        }
    }
}