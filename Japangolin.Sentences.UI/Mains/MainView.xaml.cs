namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using System.Linq;
    using System.Globalization;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        public MainView()
        {
            this.InitializeComponent();

            this.SetUserInputLanguage();
        }

        private void SetUserInputLanguage()
        {
            var japaeseCultureInfo = new CultureInfo("ja-JP");

            var availableInputLanguages = InputLanguageManager.Current.AvailableInputLanguages;
            if (availableInputLanguages == null)
            {
                return;
            }

            if (availableInputLanguages.Cast<CultureInfo>().Contains(japaeseCultureInfo))
            {
                InputLanguageManager.SetInputLanguage(this.UserInputTextBox, japaeseCultureInfo);
            }
        }
    }
}
