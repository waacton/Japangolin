namespace Wacton.Japangolin.UI.Mains;

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

    public bool HasSecondDetail => SecondDetail != null;
    public bool HasThirdDetail => ThirdDetail != null;

    public DetailViewModel(ModelChangeNotifier modelChangeNotifier) : base(modelChangeNotifier)
    {
    }

    public void Update(string firstDetail, string secondDetail = null, string thirdDetail = null)
    {
        FirstDetail = firstDetail;
        SecondDetail = secondDetail;
        ThirdDetail = thirdDetail;
        NotifyAboutAllProperties();
    }
}