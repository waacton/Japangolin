namespace Wacton.Japangolin.Domain.MVVM
{
    using System;
    using System.Collections.Generic;

    public class ModelChangeNotifier
    {
        private readonly List<(WeakReference watchedModelRef, Action actionWhenChanged)> modelChangeSubscriptions
            = new List<(WeakReference watchedModelRef, Action actionWhenChanged)>();

        public void Subscribe(object watchedModel, Action actionWhenChanged)
        {
            var subscription = (watchedModelRef: new WeakReference(watchedModel), actionWhenChanged: actionWhenChanged);
            modelChangeSubscriptions.Add(subscription);
        }

        public void Notify(IEnumerable<object> changedModels)
        {
            foreach (var changedModel in changedModels)
            {
                Notify(changedModel);
            }
        }

        public void Notify(object changedModel)
        {
            foreach (var modelChangeSubscription in modelChangeSubscriptions)
            {
                var watchedModel = modelChangeSubscription.watchedModelRef.Target;
                if (changedModel == watchedModel)
                {
                    modelChangeSubscription.actionWhenChanged();
                }
            }
        }
    }
}