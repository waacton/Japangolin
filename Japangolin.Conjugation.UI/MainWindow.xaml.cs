namespace Wacton.Japangolin.Conjugation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Desu.Romaji;
    using Wacton.Japangolin.Conjugation.Mains;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.Randomness;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // TODO: view model!
        private List<WordData> wordDatas;
        public List<string> Words => this.wordDatas.Select(word => word.English.ToLower()).ToList();
        public string SelectedWord { get; set; } // used by the XAML, but not yet accessed here
        public string WordsTitle => this.Words.Count == 1 ? "Word" : "Words";

        private List<GrammarBase> allGrammars = GetAllGrammars();
        private GrammarBase grammar;
        public string GrammarEnglish { get; private set; }
        public string GrammarVariation { get; private set; }

        private string answerKana;
        private string answerKanji;
        public List<string> Answers { get; private set; }
        public string AnswerJapanese { get; set; }
        public bool HasGrammar => this.GrammarEnglish != null;
        public bool IsAnswerFocused { get; set; }
        public string AnswerTooltip => this.IsAnswerFocused ? "Double click to toggle kanji" : "㊙️";

        public string InputJapanese { get; set; }
        private bool IsAnswerCorrect => this.InputJapanese != null && this.Answers.Contains(this.InputJapanese);

        public DetailViewModel DetailViewModel { get; private set; }
        private DetailViewModel detailViewModel;
        private NoDetailViewModel noDetailViewModel;

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

        public void GrammarSelected()
        {
            var grammarJapanese = this.grammar.Information(this.wordDatas.ToArray());

            this.detailViewModel.Update(grammarJapanese);
            this.DetailViewModel = this.detailViewModel;
            this.OnPropertyChanged(nameof(this.DetailViewModel));
        }

        public void WordSelected(string selectedWord)
        {
            var selectedWordData = this.wordDatas.Single(word => word.English.ToLower() == selectedWord);
            var wordClassDetail = this.pascalCaseRegex.Replace(selectedWordData.Class.ToString(), "-").ToLower();

            this.detailViewModel.Update(selectedWordData.Kana, selectedWordData.Kanji, wordClassDetail);
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

        private WordData GetRandomEntry(List<WordClass> validWordClasses)
        {
            WordData wordData = null;

            var isValidWordClass = false;
            while (!isValidWordClass)
            {
                var entry = RandomSelection.SelectOne(japaneseEntries);
                wordData = GetWordData(entry);
                isValidWordClass = validWordClasses.Contains(wordData.Class);
            }

            return wordData;
        }

        private void Refresh()
        {
            // TODO: make sure all JLPT N5 words are covered (manual check of sequence #s + preprocess?)

            this.GrammarEnglish = null;
            this.Answers = null;
            this.IsAnswerFocused = false;
            this.InputJapanese = null;
            this.DetailViewModel = this.noDetailViewModel;

            this.grammar = RandomSelection.SelectOne(this.allGrammars);

            this.wordDatas = new List<WordData>();
            for (var i = 0; i < this.grammar.RequiredWordDataCount; i++)
            {
                var requiredWordClasses = this.grammar.GetRequiredWordClasses(i);
                wordDatas.Add(GetRandomEntry(requiredWordClasses));
            }

            this.GrammarEnglish = this.pascalCaseRegex.Replace(this.grammar.DisplayName, " ").ToLower();
            this.GrammarVariation = this.grammar.Variation;

            this.Answers = this.grammar.Conjugate(wordDatas.ToArray());
            this.answerKana = this.grammar.ConjugateAllKana(wordDatas.ToArray());
            this.answerKanji = this.grammar.ConjugateAllKanji(wordDatas.ToArray());
            this.AnswerJapanese = this.answerKana;

            this.OnPropertyChanged(nameof(this.GrammarEnglish));
            this.OnPropertyChanged(nameof(this.GrammarVariation));
            this.OnPropertyChanged(nameof(this.Words));
            this.OnPropertyChanged(nameof(this.WordsTitle));
            this.OnPropertyChanged(nameof(this.AnswerJapanese));
            this.OnPropertyChanged(nameof(this.IsAnswerFocused));
            this.OnPropertyChanged(nameof(this.AnswerTooltip));
            this.OnPropertyChanged(nameof(this.HasGrammar)); // TODO: needed?
            this.OnPropertyChanged(nameof(this.InputJapanese));
            this.OnPropertyChanged(nameof(this.DetailViewModel));
        }

        private WordData GetWordData(IJapaneseEntry japaneseEntry)
        {
            var kana = japaneseEntry.GetKana();
            var kanji = japaneseEntry.GetKanji();
            var english = japaneseEntry.GetEnglish();
            var partOfSpeech = japaneseEntry.GetPartsOfSpeech().First();
            var wordClass = GetWordClass(partOfSpeech);
            return new WordData { Kana = kana, Kanji = kanji, English = english, Class = wordClass };
        }

        public static readonly List<PartOfSpeech> Nouns = new List<PartOfSpeech> { PartOfSpeech.NounCommon, PartOfSpeech.NounAdverbial, PartOfSpeech.NounNo, PartOfSpeech.NounSuru, PartOfSpeech.NounTemporal };
        public static readonly List<PartOfSpeech> AdjectivesNa = new List<PartOfSpeech> { PartOfSpeech.AdjectiveNa };
        public static readonly List<PartOfSpeech> AdjectivesI = new List<PartOfSpeech> { PartOfSpeech.AdjectiveI };
        public static readonly List<PartOfSpeech> VerbsRu = new List<PartOfSpeech> { PartOfSpeech.Verb1 };
        public static readonly List<PartOfSpeech> VerbsU = new List<PartOfSpeech> { PartOfSpeech.Verb5U, PartOfSpeech.Verb5Tsu, PartOfSpeech.Verb5Ru, PartOfSpeech.Verb5Mu, PartOfSpeech.Verb5Bu, PartOfSpeech.Verb5Nu, PartOfSpeech.Verb5Ku, PartOfSpeech.Verb5Gu, PartOfSpeech.VerbSu };

        private WordClass GetWordClass(PartOfSpeech partOfSpeech)
        {
            if (partOfSpeech == null) { throw new InvalidOperationException(); }
            if (Nouns.Contains(partOfSpeech)) { return WordClass.Noun; }
            if (AdjectivesNa.Contains(partOfSpeech)) { return WordClass.AdjectiveNa; }
            if (AdjectivesI.Contains(partOfSpeech)) { return WordClass.AdjectiveI; }
            if (VerbsRu.Contains(partOfSpeech)) { return WordClass.VerbRu; }
            if (VerbsU.Contains(partOfSpeech)) { return WordClass.VerbU; }
            return WordClass.Unknown; // TODO: handle this better
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

        private static List<GrammarBase> GetAllGrammars()
        {
            var grammars = new List<GrammarBase>();
            grammars.AddRange(GrammarForm.GetAll<GrammarForm>());
            grammars.AddRange(GrammarPhrase.GetAll<GrammarPhrase>());
            return grammars;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
