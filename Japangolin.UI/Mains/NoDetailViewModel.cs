namespace Wacton.Japangolin.UI.Mains
{
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

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
