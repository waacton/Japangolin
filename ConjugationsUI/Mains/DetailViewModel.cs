namespace ConjugationsUI.Mains
{
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class DetailViewModel : ViewModelBase
    {
        public string FirstDetail { get; private set; }
        public string SecondDetail { get; private set; }
        public bool HasSecondDetail => this.SecondDetail != null;

        public DetailViewModel(ModelChangeNotifier modelChangeNotifier) : base(modelChangeNotifier)
        {
        }

        public void Update(string firstDetail, string secondDetail = null)
        {
            this.FirstDetail = firstDetail;
            this.SecondDetail = secondDetail;
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
