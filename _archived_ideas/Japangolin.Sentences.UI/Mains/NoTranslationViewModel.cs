namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using Wacton.Tovarisch.MVVM;

    public class NoTranslationViewModel : TranslationViewModel
    {
        public NoTranslationViewModel(ModelChangeNotifier modelChangeNotifier) : base(modelChangeNotifier)
        {
        }
    }

    public class DesignTimeNoTranslationViewModel : NoTranslationViewModel
    {
        public DesignTimeNoTranslationViewModel() : base(null)
        {
        }
    }
}
