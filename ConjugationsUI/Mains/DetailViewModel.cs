namespace ConjugationsUI.Mains
{
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class DetailViewModel : ViewModelBase
    {
        public string English { get; private set; }
        public string Kana { get; private set; }
        public string Kanji { get; private set; }
        public bool HasKanji => this.Kanji != this.Kana;

        public DetailViewModel(ModelChangeNotifier modelChangeNotifier) : base(modelChangeNotifier)
        {
        }

        public void Update(string english, string kana, string kanji)
        {
            this.English = english;
            this.Kana = kana;
            this.Kanji = kanji;
            this.NotifyAllPropertyBindings();
        }
    }

    public class DesignTimeDetailViewModel : DetailViewModel
    {
        public DesignTimeDetailViewModel() : base(null)
        {
        }
    }
}
