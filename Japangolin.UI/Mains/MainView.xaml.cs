namespace Wacton.Japangolin.UI.Mains
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            this.InitializeComponent();
            this.Romaji.Focus();
        }
    }
}
