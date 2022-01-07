namespace Wacton.Japangolin.UI.Mains
{
    using System.Windows;
    using System.Windows.Media;
    using Wacton.Japangolin.Domain.MVVM;
    using Wacton.Japangolin.UI.MVVM;

    public class DetailViewModel : ViewModelBase
    {
        /* 
         * not actually using the Japanese fonts that have been embedded in the resources
         * as the font renders in a different size and with different positioning
         * and fixing the UI jank requires too many hacks (like customising font size and padding whenever font changes)
         * (leaving the code here for future reference)
         */
        protected readonly FontFamily LatinFontFamily = (FontFamily)Application.Current.FindResource("MaterialDesignFont");
        protected readonly FontFamily JapaneseFontFamily = (FontFamily)Application.Current.FindResource("NotoSansJP");

        public string FirstDetail { get; private set; }
        public string SecondDetail { get; private set; }
        public string ThirdDetail { get; private set; }

        public bool HasSecondDetail => this.SecondDetail != null;
        public bool HasThirdDetail => this.ThirdDetail != null;

        public DetailViewModel(ModelChangeNotifier modelChangeNotifier) : base(modelChangeNotifier)
        {
        }

        public void Update(string firstDetail, string secondDetail = null, string thirdDetail = null)
        {
            this.FirstDetail = firstDetail;
            this.SecondDetail = secondDetail;
            this.ThirdDetail = thirdDetail;
            this.NotifyAllPropertyBindings();
        }
    }

    // --- design time ---

    public class DesignTimeDetailViewModel : DetailViewModel
    {
        public new string FirstDetail => "ジャパンゴリン";
        public new string SecondDetail => "Japangolin";
        public new string ThirdDetail => "by Wacton";

        public new bool HasSecondDetail => true;
        public new bool HasThirdDetail => true;

        public DesignTimeDetailViewModel() : base(null)
        {
        }
    }
}
