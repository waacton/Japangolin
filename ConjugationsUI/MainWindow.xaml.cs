﻿namespace ConjugationsUI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    using ConjugationsUI.Mains;

    using Wacton.Desu.Enums;
    using Wacton.Desu.Japanese;
    using Wacton.Desu.Romaji;
    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Extensions;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.Randomness;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // TODO: view model!
        public string MainEnglish => this.mainEntry.GetEnglish();
        public string ConjugationDescription { get; private set; }
        public string GrammarDescription { get; private set; }
        public string AuxEnglish => string.Join(":", this.auxEntries.Select(entry => entry.GetEnglish()));
        public string AnswerKana { get; private set; }

        public string InputKana { get; set; }
        private bool IsAnswerCorrect => this.InputKana != null && this.InputKana.Equals(this.AnswerKana);

        public bool HasConjugationDescription => this.ConjugationDescription != null;
        public bool HasGrammarDescription => this.GrammarDescription != null;
        public bool HasAuxEnglish => this.auxEntries.Count() > 0;

        public DetailViewModel DetailViewModel { get; }

        private List<IJapaneseEntry> japaneseEntries;

        private IJapaneseEntry mainEntry;
        private List<IJapaneseEntry> auxEntries = new List<IJapaneseEntry>();

        private List<WordClass> wordClasses = new List<WordClass> { WordClass.JapaneseNoun, WordClass.JapaneseAdjectiveNa, WordClass.JapaneseAdjectiveI, WordClass.JapaneseVerbIchidan, WordClass.JapaneseVerbGodan};
        private List<Tense> tenses = new List<Tense> { Tense.Present, Tense.Past };
        private List<Polarity> polarities = new List<Polarity> { Polarity.Affirmative, Polarity.Negative };
        private List<Formality> formalities = new List<Formality> { Formality.Long, Formality.Short };

        private Transliterator transliterator = new Transliterator();

        public MainWindow()
        {
            var japaneseDictionary = new JapaneseDictionary();

            var rawData = File.ReadAllLines("../../JLPTN5.csv");
            var jlptList = new List<string[]>();
            foreach (var data in rawData)
            {
                var splitData = data.Split(',');
                var kanji = splitData[0];
                var kana = splitData[1];

                if (!string.IsNullOrEmpty(kanji))
                {
                    jlptList.Add(new string[] { kanji, kana });
                }
            }

            this.japaneseEntries = japaneseDictionary.GetEntries()
                .Where(entry =>
                    jlptList.Any(jlpt => entry.Kanjis.Any(k => k.Text == jlpt[0]) &&
                                         entry.Readings.Any(r => r.Text == jlpt[1])))
                .ToList();

            this.DetailViewModel = new DetailViewModel(new ModelChangeNotifier());
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
            this.DetailViewModel.Update(this.mainEntry.GetEnglish(), this.mainEntry.GetKana(), this.mainEntry.GetKanji());
        }

        // TODO!
        public void DescriptionSelected()
        {
            if (this.HasConjugationDescription)
            {
                this.DetailViewModel.Update(this.ConjugationDescription, null, null);
            }
            else
            {
                this.DetailViewModel.Update(this.GrammarDescription, null, null);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.UpdateWord();
            this.Input.Focus(); // does not work in constructor due to some timing issue - attempt fix when refactor for view models
        }

        private void UpdateWord()
        {
            // TODO: make sure all JLPT N5 words are covered (manual check of sequence #s + preprocess?)
            // TODO: add valid word classes to grammars

            this.mainEntry = RandomSelection.SelectOne(japaneseEntries);
            this.ConjugationDescription = null;
            this.GrammarDescription = null;
            this.auxEntries.Clear();
            this.AnswerKana = null;
            this.InputKana = null;
            this.DetailViewModel.Update(null, null, null); // TODO: improve

            var mainWordData = this.GetWordData(this.mainEntry);
            if (mainWordData.Class == WordClass.None) // no conjugation
            {
                this.AnswerKana = mainWordData.Text;
            }
            else // use standalone or grammar conjugation
            {
                var useGrammar = RandomSelection.IsSuccessful(0.5);
                if (useGrammar)
                {
                    var grammar = RandomSelection.SelectOne(Grammar.GetAll<Grammar>());

                    var wordDatas = new WordData[grammar.RequiredWordDataCount];
                    wordDatas[0] = mainWordData;
                    if (grammar.RequiredWordDataCount > 1)
                    {
                        for (var i = 1; i < wordDatas.Length; i++)
                        {
                            IJapaneseEntry auxEntry = null;
                            WordData auxWordData = null;

                            var sameWordClass = false;
                            while (!sameWordClass)
                            {
                                auxEntry = RandomSelection.SelectOne(japaneseEntries);
                                auxWordData = this.GetWordData(auxEntry);
                                sameWordClass = auxWordData.Class == mainWordData.Class;
                            }

                            this.auxEntries.Add(auxEntry);
                            wordDatas[i] = auxWordData;
                        }
                    }

                    this.GrammarDescription = grammar.DisplayName;
                    this.AnswerKana = grammar.Conjugate(wordDatas);
                }
                else
                {
                    var tense = RandomSelection.SelectOne(tenses);
                    var polarity = RandomSelection.SelectOne(polarities);
                    var formality = RandomSelection.SelectOne(formalities);
                    this.ConjugationDescription = $"{tense}:{polarity}:{formality}";
                    this.AnswerKana = ConjugationFunctions2.Get(mainWordData.Text, mainWordData.Class, tense, polarity, formality);
                }
            }

            this.OnPropertyChanged(nameof(this.MainEnglish));
            this.OnPropertyChanged(nameof(this.ConjugationDescription));
            this.OnPropertyChanged(nameof(this.HasConjugationDescription));
            this.OnPropertyChanged(nameof(this.GrammarDescription));
            this.OnPropertyChanged(nameof(this.HasGrammarDescription));
            this.OnPropertyChanged(nameof(this.AuxEnglish));
            this.OnPropertyChanged(nameof(this.HasAuxEnglish));
            this.OnPropertyChanged(nameof(this.AnswerKana));
            this.OnPropertyChanged(nameof(this.InputKana));
        }

        private WordData GetWordData(IJapaneseEntry japaneseEntry)
        {
            var reading = japaneseEntry.Readings.First().Text;
            var partOfSpeech = japaneseEntry.GetPartsOfSpeech().First();
            var wordClass = GetWordClass(partOfSpeech);
            return new WordData { Text = reading, Class = wordClass };
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