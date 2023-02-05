namespace Wacton.Japangolin.Desktop.Mains;

using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView : UserControl
{
    private MainViewModel MainViewModel => (MainViewModel) DataContext;
        
    public MainView()
    {
        InitializeComponent();

        SetUserInputLanguage();
    }

    private void SetUserInputLanguage()
    {
        var japaneseCultureInfo = new CultureInfo("ja-JP");
        if (!IsInputLanguageAvailable(japaneseCultureInfo))
        {
            return;
        }

        // should set input to Japanese on focus, restore previously language on lose focus
        InputLanguageManager.SetInputLanguage(Input, japaneseCultureInfo);
        InputLanguageManager.SetRestoreInputLanguage(Input, true);
    }

    private static bool IsInputLanguageAvailable(CultureInfo cultureInfo)
    {
        var availableInputLanguages = InputLanguageManager.Current.AvailableInputLanguages;
        if (availableInputLanguages == null)
        {
            return false;
        }

        if (!availableInputLanguages.Cast<CultureInfo>().Contains(cultureInfo))
        {
            return false;
        }

        return true;
    }

    private void WordTextBox_OnGotFocus(object sender, RoutedEventArgs e) => MainViewModel.WordSelected();
    private void InflectionTextBox_OnGotFocus(object sender, RoutedEventArgs e) => MainViewModel.InflectionSelected();
    private void Input_OnKeyUp(object sender, KeyEventArgs e) => MainViewModel.InputEntered();
}