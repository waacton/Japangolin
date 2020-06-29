namespace Wacton.Japangolin.UI
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;

    using Wacton.Japangolin.Domain;
    using Wacton.Japangolin.UI.Mains;
    using Wacton.Tovarisch.MVVM;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // TODO: view model!
        private readonly Main main = new Main();

        public string WordText => main.Word.English.ToLower();
        public string InflectionText { get; private set; }
        public string InputText { get; set; }
        public bool IsAnswerVisible { get; private set; }
        public string AnswerText => this.GetAnswerText();
        public bool IsSnackbarActive { get; private set; }

        public DetailViewModel DetailViewModel { get; private set; }
        private readonly DetailViewModel detailViewModel;
        private readonly NoDetailViewModel noDetailViewModel;

        private readonly Timer snackbarTimer = new Timer(3000);

        public MainWindow()
        {
            this.detailViewModel = new DetailViewModel(new ModelChangeNotifier());
            this.noDetailViewModel = new NoDetailViewModel(new ModelChangeNotifier());
            this.Refresh();

            snackbarTimer.Elapsed += HideSnackbar;
            snackbarTimer.AutoReset = false;

            InitializeComponent();

            this.SetUserInputLanguage();
        }

        public void WordSelected()
        {
            var wordClassDetail = PascalCase.InsertSeparator(main.Word.Class.ToString(), "-").ToLower();

            var isKanjiDifferent = main.Word.Kanji != main.Word.Kana;
            this.detailViewModel.Update(main.Word.Kana, isKanjiDifferent ? main.Word.Kanji : null, wordClassDetail);
            this.DetailViewModel = this.detailViewModel;
            this.OnPropertyChanged(nameof(this.DetailViewModel));
        }

        public void InflectionSelected()
        {
            this.detailViewModel.Update(main.Hint);
            this.DetailViewModel = this.detailViewModel;
            this.OnPropertyChanged(nameof(this.DetailViewModel));
        }

        public void InputEntered(KeyEventArgs e)
        {
            if (this.IsInputCorrect())
            {
                this.Refresh();

                this.IsSnackbarActive = true;
                this.OnPropertyChanged(nameof(this.IsSnackbarActive));

                snackbarTimer.Start();
            }
        }

        private void HideSnackbar(object sender, ElapsedEventArgs e)
        {
            this.IsSnackbarActive = false;
            this.OnPropertyChanged(nameof(this.IsSnackbarActive));
        }

        private bool IsInputCorrect()
        {
            if (this.InputText == null)
            {
                return false;
            }

            return this.InputText == main.AnswerKana || this.InputText == main.AnswerKanji;
        }

        private void SkipButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Refresh();
            this.Input.Focus(); // does not work in constructor due to some timing issue - attempt fix when refactor for view models
        }

        private void ViewAnswerButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.IsAnswerVisible = true;
            this.OnPropertyChanged(nameof(this.IsAnswerVisible));
            this.OnPropertyChanged(nameof(this.AnswerText));
        }

        private void Refresh()
        {
            this.Reset();
            this.UpdateWordAndInflection();
            this.NotifyViewOfChanges();
        }

        private void Reset()
        {
            this.InflectionText = null;
            this.InputText = null;
            this.DetailViewModel = this.noDetailViewModel;
            this.IsAnswerVisible = false;
        }

        private void UpdateWordAndInflection()
        {
            main.UpdateWordAndInflection(); // TODO: move to command
            
            this.InflectionText = main.Inflection.PrettyDisplay();
        }

        private void NotifyViewOfChanges()
        {
            this.OnPropertyChanged(nameof(this.WordText));
            this.OnPropertyChanged(nameof(this.InflectionText));
            this.OnPropertyChanged(nameof(this.InputText));
            this.OnPropertyChanged(nameof(this.DetailViewModel));
            this.OnPropertyChanged(nameof(this.IsAnswerVisible));
            this.OnPropertyChanged(nameof(this.AnswerText));
        }

        private string GetAnswerText()
        {
            if (!this.IsAnswerVisible)
            {
                return "Click to reveal the answer";
            }

            var isKanjiDifferent = main.AnswerKanji != main.AnswerKana;
            return isKanjiDifferent ? $"{main.AnswerKana} · {main.AnswerKanji}" : $"{main.AnswerKana}";
        }

        private void SetUserInputLanguage()
        {
            var japaneseCultureInfo = new CultureInfo("ja-JP");
            if (!this.IsInputLanguageAvailable(japaneseCultureInfo))
            {
                return;
            }

            // should set input to Japanese on focus, restore previously language on lose focus
            InputLanguageManager.SetInputLanguage(this.Input, japaneseCultureInfo);
            InputLanguageManager.SetRestoreInputLanguage(this.Input, true);
        }

        private bool IsInputLanguageAvailable(CultureInfo cultureInfo)
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
