using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Wacton.Desu.Enums;
using Wacton.Desu.Japanese;
using Wacton.Desu.Romaji;
using Wacton.Japangolin.Sentences.Domain.Conjugations;
using Wacton.Japangolin.Sentences.Domain.Extensions;
using Wacton.Japangolin.Sentences.Domain.Golins;
using Wacton.Tovarisch.Randomness;

namespace ConjugationsUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string English => this.currentEntry.GetEnglish();
        public string KanjiBase => this.currentEntry.GetKanji();
        public string KanaBase => this.currentEntry.GetKana();
        public string RomajiBase => this.transliterator.GetRomaji(this.KanaBase);

        private List<IJapaneseEntry> japaneseEntries;
        private IJapaneseEntry currentEntry;

        private List<WordClass> wordClasses = new List<WordClass> { WordClass.JapaneseNoun, WordClass.JapaneseAdjectiveNa, WordClass.JapaneseAdjectiveI, WordClass.JapaneseVerbIchidan, WordClass.JapaneseVerbGodan};
        private List<Tense> tenses = new List<Tense> { Tense.Present, Tense.Past };
        private List<Polarity> polarities = new List<Polarity> { Polarity.Affirmative, Polarity.Negative };
        private List<Formality> formalities = new List<Formality> { Formality.Long, Formality.Short };

        private List<Conjugation> conjugations = Conjugation.KnownConjugations().ToList();
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

            this.UpdateWord();

            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.UpdateWord();
        }

        private void UpdateWord()
        {
            this.currentEntry = RandomSelection.SelectOne(japaneseEntries);
            var reading = this.currentEntry.Readings.First().Text;

            var partOfSpeech = this.currentEntry.GetPartsOfSpeech().First();
            var wordClass = GetWordClass(partOfSpeech);

            // TODO: randomly choose either conjugation or grammar
            var tense = RandomSelection.SelectOne(tenses);
            var polarity = RandomSelection.SelectOne(polarities);
            var formality = RandomSelection.SelectOne(formalities);
            var conjugation = ConjugationFunctions2.Get(reading, wordClass, tense, polarity, formality);

            var grammar = "ください";
            var conjugationForGrammar = ConjugationFunctions2.GetTe(reading, wordClass);
            var fullGrammar = string.Concat(conjugationForGrammar, grammar);

            this.OnPropertyChanged(nameof(this.English));
            this.OnPropertyChanged(nameof(this.KanjiBase));
            this.OnPropertyChanged(nameof(this.KanaBase));
            this.OnPropertyChanged(nameof(this.RomajiBase));
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
            throw new InvalidOperationException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
