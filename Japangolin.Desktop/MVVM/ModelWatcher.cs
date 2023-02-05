namespace Wacton.Japangolin.Desktop.MVVM;

using System;
using System.Collections.Generic;
using System.Linq;

public sealed class ModelWatcher
{
    private readonly List<(WeakReference modelReference, Action onModelChanged)> subscriptions = new();

    public void Subscribe(object watchedModel, Action onModelChanged)
    {
        var modelReference = new WeakReference(watchedModel);
        var subscription = (modelReference, onModelChanged);
        subscriptions.Add(subscription);
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
        var subscriptionsToNotify = subscriptions.Where(x => x.modelReference.Target == changedModel).ToList();
        foreach (var subscription in subscriptionsToNotify)
        {
            subscription.onModelChanged();
        }
    }
}