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
    using Wacton.Japangolin.Conjugation.Extensions;
    using Wacton.Japangolin.Conjugation.Mains;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.Randomness;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // TODO: view model!

        public string MainEnglish => this.mainEntry.English.ToLower();
        public string ModifierEnglish { get; private set; }
        public string ModifierJapanese { get; private set; }
        public string ModifierVariation { get; private set; }
        public string AnswerKana { get; private set; }
        public bool HasModifier => this.ModifierEnglish != null;

        public string InputKana { get; set; }
        private bool IsAnswerCorrect => this.InputKana != null && this.InputKana.Equals(this.AnswerKana);

        public DetailViewModel DetailViewModel { get; private set; }
        private DetailViewModel detailViewModel;
        private NoDetailViewModel noDetailViewModel;

        private List<IJapaneseEntry> japaneseEntries;

        private WordData mainEntry;

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
            this.UpdateWord();

            InitializeComponent();
            this.SetUserInputLanguage();
        }

        public void InputEntered(KeyEventArgs e)
        {
            if (this.IsAnswerCorrect)
            {
                this.UpdateWord();
            }
        }

        public void MainEnglishSelected()
        {
            this.detailViewModel.Update(this.mainEntry.Kana, this.mainEntry.Kanji);
            this.DetailViewModel = detailViewModel;
            this.OnPropertyChanged(nameof(this.DetailViewModel));
        }

        // TODO!
        public void DescriptionSelected()
        {
            this.detailViewModel.Update(this.ModifierJapanese);

            this.DetailViewModel = detailViewModel;
            this.OnPropertyChanged(nameof(this.DetailViewModel));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.UpdateWord();
            this.Input.Focus(); // does not work in constructor due to some timing issue - attempt fix when refactor for view models
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

        private void UpdateWord()
        {
            // TODO: make sure all JLPT N5 words are covered (manual check of sequence #s + preprocess?)
            // TODO: add valid word classes to grammars

            this.ModifierEnglish = null;
            this.ModifierJapanese = null;
            this.AnswerKana = null;
            this.InputKana = null;
            this.DetailViewModel = this.noDetailViewModel;

            var grammar = RandomSelection.SelectOne(Grammar.GetAll<Grammar>());
            var requiredWordClasses = grammar.GetRequiredWordClasses();

            this.mainEntry = GetRandomEntry(requiredWordClasses[0]);
            var wordDatas = new WordData[grammar.RequiredWordDataCount];
            wordDatas[0] = this.mainEntry;

            if (grammar.RequiredWordDataCount > 1)
            {
                for (var i = 1; i < wordDatas.Length; i++)
                {
                    wordDatas[i] = GetRandomEntry(requiredWordClasses[i]);
                }
            }

            this.ModifierEnglish = this.pascalCaseRegex.Replace(grammar.DisplayName, " ").ToLower();
            this.ModifierJapanese = grammar.Information(wordDatas);
            this.ModifierVariation = grammar.Variation;

            if (grammar.RequiredWordDataCount > 1)
            {
                this.ModifierEnglish += $"{Environment.NewLine}+  {string.Join(", ", wordDatas.Skip(1).Select(wordData => wordData.English))}";
                this.ModifierJapanese += $"{Environment.NewLine}＋　{string.Join("、", wordDatas.Skip(1).Select(wordData => wordData.Kana))}";
            }

            this.AnswerKana = grammar.Conjugate(wordDatas);

            this.OnPropertyChanged(nameof(this.MainEnglish));
            this.OnPropertyChanged(nameof(this.ModifierEnglish));
            this.OnPropertyChanged(nameof(this.ModifierJapanese));
            this.OnPropertyChanged(nameof(this.ModifierVariation));
            this.OnPropertyChanged(nameof(this.AnswerKana));
            this.OnPropertyChanged(nameof(this.HasModifier));
            this.OnPropertyChanged(nameof(this.InputKana));
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
            if (Nouns.Contains(partOfSpeech)) { return WordClass.JapaneseNoun; }
            if (AdjectivesNa.Contains(partOfSpeech)) { return WordClass.JapaneseAdjectiveNa; }
            if (AdjectivesI.Contains(partOfSpeech)) { return WordClass.JapaneseAdjectiveI; }
            if (VerbsRu.Contains(partOfSpeech)) { return WordClass.JapaneseVerbIchidan; }
            if (VerbsU.Contains(partOfSpeech)) { return WordClass.JapaneseVerbGodan; }
            return WordClass.None; // TODO: handle this better
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
