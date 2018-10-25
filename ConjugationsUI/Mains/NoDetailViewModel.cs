namespace ConjugationsUI.Mains
{
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class NoDetailViewModel : DetailViewModel
    {
        public NoDetailViewModel(ModelChangeNotifier modelChangeNotifier) : base(modelChangeNotifier)
        {
        }
    }

    public class DesignTimeNoTranslationViewModel : NoDetailViewModel
    {
        public DesignTimeNoTranslationViewModel() : base(null)
        {
        }
    }
}
