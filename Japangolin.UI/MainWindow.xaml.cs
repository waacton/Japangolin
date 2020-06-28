namespace Wacton.Japangolin
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;

    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Mains;
    using Wacton.Japangolin.UI;
    using Wacton.Tovarisch.Enum;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.Randomness;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // TODO: view model!
        private Word word;
        public string WordText => this.word.English.ToLower();

        private Inflection inflection;
        private readonly List<Inflection> allInflections = Enumeration.GetAll<Inflection>().ToList();
        public string InflectionText { get; private set; }

        public string InputText { get; set; }

        private string answerKana;
        private string answerKanji;
        private string answer;
        public bool IsAnswerVisible { get; private set; }
        public string AnswerText => this.IsAnswerVisible ? this.answer : "Click to reveal the answer";

        public bool IsSnackbarActive { get; private set; }

        public DetailViewModel DetailViewModel { get; private set; }
        private readonly DetailViewModel detailViewModel;
        private readonly NoDetailViewModel noDetailViewModel;

        private readonly List<IJapaneseEntry> japaneseEntries;
        private readonly Timer snackbarTimer = new Timer(3000);

        public MainWindow()
        {
            var japaneseDictionary = new JapaneseDictionary();

            var rawData = File.ReadAllLines("../../JLPTN5_sequences.csv");
            var jlptSequenceNumbers = rawData.Select(data => int.Parse(data)).ToList();
            this.japaneseEntries = japaneseDictionary.GetEntries()
                .Where(entry => jlptSequenceNumbers.Contains(entry.Sequence))
                .ToList();

            this.detailViewModel = new DetailViewModel(new ModelChangeNotifier());
            this.noDetailViewModel = new NoDetailViewModel(new ModelChangeNotifier());
            this.Refresh();

            InitializeComponent();
            this.SetUserInputLanguage();

            snackbarTimer.Elapsed += HideSnackbar;
            snackbarTimer.AutoReset = false;
        }

        public void WordSelected()
        {
            var wordClassDetail = PascalCase.InsertSeparator(this.word.Class.ToString(), "-").ToLower();

            var isKanjiDifferent = this.word.Kanji != this.word.Kana;
            this.detailViewModel.Update(this.word.Kana, isKanjiDifferent ? this.word.Kanji : null, wordClassDetail);
            this.DetailViewModel = this.detailViewModel;
            this.OnPropertyChanged(nameof(this.DetailViewModel));
        }

        public void InflectionSelected()
        {
            var hint = this.inflection.GetHint(this.word);
            this.detailViewModel.Update(hint);
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

            return this.InputText == this.answerKana || this.InputText == this.answerKanji;
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
            this.word = GetRandomWord();
            this.inflection = RandomSelection.SelectOne(this.allInflections);
            this.InflectionText = this.inflection.PrettyDisplay();

            (this.answerKana, this.answerKanji) = this.inflection.Conjugate(this.word);
            var isKanjiDifferent = this.answerKanji != this.answerKana;
            this.answer = isKanjiDifferent ?  $"{this.answerKana} · {this.answerKanji}" : $"{this.answerKana}";
        }

        private Word GetRandomWord()
        {
            var isValid = false;
            Word word = null;

            while (!isValid)
            {
                var entry = RandomSelection.SelectOne(japaneseEntries);
                word = entry.ParseToWord();
                isValid = word.Class != WordClass.Unknown;
            }

            return word;
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

        private void SetUserInputLanguage()
        {
            var japaneseCultureInfo = new CultureInfo("ja-JP");

            var availableInputLanguages = InputLanguageManager.Current.AvailableInputLanguages;
            if (availableInputLanguages == null)
            {
                return;
            }

            if (availableInputLanguages.Cast<CultureInfo>().Contains(japaneseCultureInfo))
            {
                InputLanguageManager.SetInputLanguage(this.Input, japaneseCultureInfo);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
