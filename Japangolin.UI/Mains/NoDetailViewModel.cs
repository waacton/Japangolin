namespace Wacton.Japangolin.UI.Mains
{
    using Wacton.Japangolin.Domain.MVVM;

    public class NoDetailViewModel : DetailViewModel
    {
        public NoDetailViewModel(ModelChangeNotifier modelChangeNotifier) : base(modelChangeNotifier)
        {
        }
    }

    // --- design time ---

    public class DesignTimeNoDetailViewModel : NoDetailViewModel
    {
        public DesignTimeNoDetailViewModel() : base(null)
        {
        }
    }
}
