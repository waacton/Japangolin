namespace Wacton.Japangolin
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;

    using Wacton.Desu.Japanese;
    using Wacton.Desu.Romaji;
    using Wacton.Japangolin.Mains;
    using Wacton.Japangolin.UI;
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
        private readonly List<Inflection> allInflections = GetAllInflections();
        public string InflectionText { get; private set; }

        private string answerKana;
        private string answerKanji;
        public List<string> Answers { get; private set; }
        public string AnswerJapanese { get; set; }
        public bool HasGrammar => this.InflectionText != null;
        public bool IsAnswerFocused { get; set; }
        public string AnswerTooltip => this.IsAnswerFocused ? "Double click to toggle kanji" : "㊙️";

        public string InputJapanese { get; set; }
        private bool IsAnswerCorrect => this.InputJapanese != null && this.Answers.Contains(this.InputJapanese);

        public DetailViewModel DetailViewModel { get; private set; }
        private readonly DetailViewModel detailViewModel;
        private readonly NoDetailViewModel noDetailViewModel;

        private List<IJapaneseEntry> japaneseEntries;
        private Transliterator transliterator = new Transliterator();
        private Regex pascalCaseRegex = new Regex(@"(?!^)(?=[A-Z])");

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
        }

        public void InputEntered(KeyEventArgs e)
        {
            if (this.IsAnswerCorrect)
            {
                this.Refresh();
            }
        }

        public void AnswerGotFocus()
        {
            this.IsAnswerFocused = true;
            this.OnPropertyChanged(nameof(this.IsAnswerFocused));
            this.OnPropertyChanged(nameof(this.AnswerTooltip));
        }

        public void AnswerLostFocus()
        {
            this.IsAnswerFocused = false;
            this.OnPropertyChanged(nameof(this.IsAnswerFocused));
            this.OnPropertyChanged(nameof(this.AnswerTooltip));
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Refresh();
            this.Input.Focus(); // does not work in constructor due to some timing issue - attempt fix when refactor for view models
        }

        private void AnswerTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var isAnswerKana = this.AnswerJapanese == this.answerKana;
            this.AnswerJapanese = isAnswerKana ? this.answerKanji : this.answerKana;
            this.OnPropertyChanged(nameof(this.AnswerJapanese));
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

        private void Refresh()
        {
            this.Reset();
            this.UpdateWordAndInflection();
            this.NotifyViewOfChanges();
        }

        private void Reset()
        {
            this.InflectionText = null;
            this.Answers = null;
            this.IsAnswerFocused = false;
            this.InputJapanese = null;
            this.DetailViewModel = this.noDetailViewModel;
        }

        private void UpdateWordAndInflection()
        {
            this.word = GetRandomWord();
            this.inflection = RandomSelection.SelectOne(this.allInflections);
            this.InflectionText = this.inflection.PrettyDisplay();

            (this.answerKana, this.answerKanji) = this.inflection.Conjugate(this.word);
            this.AnswerJapanese = this.answerKana;
        }

        private void NotifyViewOfChanges()
        {
            this.OnPropertyChanged(nameof(this.WordText));
            this.OnPropertyChanged(nameof(this.InflectionText));
            this.OnPropertyChanged(nameof(this.AnswerJapanese));
            this.OnPropertyChanged(nameof(this.IsAnswerFocused));
            this.OnPropertyChanged(nameof(this.AnswerTooltip));
            this.OnPropertyChanged(nameof(this.HasGrammar)); // TODO: needed?
            this.OnPropertyChanged(nameof(this.InputJapanese));
            this.OnPropertyChanged(nameof(this.DetailViewModel));
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
                InputLanguageManager.SetInputLanguage(this.Input, japaeseCultureInfo);
            }
        }

        private static List<Inflection> GetAllInflections()
        {
            var inflections = new List<Inflection>();
            inflections.AddRange(Inflection.GetAll<Inflection>());
            return inflections;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
