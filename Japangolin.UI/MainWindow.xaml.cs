namespace Japangolin.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    using Japangolin.Domain;

    using Wacton.Tovarisch.Randomness;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<TransliteratedKana> transliteratedKana;
        private TransliteratedKana currentTransliteratedKana;

        public string Romaji { get; private set; }

        public MainWindow()
        {
            this.transliteratedKana = PhraseProvider.GetTransliteratedKana();

            this.InitializeComponent();
            this.UserRomaji.Focus();
            this.UpdatePhrase();
        }

        private void UpdatePhrase()
        {
            this.currentTransliteratedKana = RandomSelection.SelectOne(this.transliteratedKana);
            this.Kana.Text = this.currentTransliteratedKana.Kana;
            this.Romaji = this.currentTransliteratedKana.Romaji;
            this.Kanji.Text = this.currentTransliteratedKana.Kanji.Any() ? this.currentTransliteratedKana.Kanji.First() : "<no kanji entry>";
            this.Meaning.Text = this.currentTransliteratedKana.Meaning;
            this.UserRomaji.Text = string.Empty;
            this.CheckPhrase();
        }

        private bool CheckPhrase()
        {
            if (this.UserRomaji.Text.Equals(this.Romaji))
            {
                this.Feedback.Text = "万歳！！！ ヽ(=^･ω･^=)丿";
                this.Feedback.Foreground = Brushes.ForestGreen;
                this.Kanji.Visibility = Visibility.Visible;
                this.Meaning.Visibility = Visibility.Visible;
                return true;
                //this.GoJapangolin.IsEnabled = true;
            }
            else
            {
                this.Feedback.Text = "馬鹿 [○・｀Д´・○]";
                this.Feedback.Foreground = Brushes.Red;
                this.Kanji.Visibility = Visibility.Hidden;
                this.Meaning.Visibility = Visibility.Hidden;
                return false;
                //this.GoJapangolin.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.UpdatePhrase();
        }

        private void RomajiEntered(object sender, KeyEventArgs e)
        {
            var isPhraseCorrect = this.CheckPhrase();

            if (e.Key.Equals(Key.Enter) && isPhraseCorrect)
            {
                this.UpdatePhrase();
            }
        }
    }
}
