namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using MaterialDesignThemes.Wpf;

    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;

    using Wacton.Japangolin.Sentences.Domain.Commands;
    using Wacton.Japangolin.Sentences.Domain.Conjugations;
    using Wacton.Japangolin.Sentences.Domain.Golins;
    using Wacton.Japangolin.Sentences.Domain.Mains;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class MainViewModel : ViewModelBase
    {
        private readonly Main main;
        private readonly UpdateSentenceCommand updateSentenceCommand;

        private Sentence CurrentSentence => this.main.Sentence;

        public List<IGolin> GolinEnglish => this.CurrentSentence.GolinEnglish();
        public List<IGolin> GolinJapanese => this.CurrentSentence.GolinJapanese();

        public string EnglishSentence => this.CurrentSentence.GetEnglish();
        public string KanaSentence => this.CurrentSentence.GetKana();
        public string KanjiSentence => this.CurrentSentence.GetKanji();

        private IGolin selectedGolin;
        public IGolin SelectedGolin
        {
            get
            {
                return this.selectedGolin;
            }
            set
            {
                this.selectedGolin = value;
                this.NotifyOfPropertyChange(nameof(this.SelectedGolin));
                this.UpdateTranslationViewModel(this.selectedGolin);
            }
        }

        private readonly TranslationViewModel translationViewModel;
        private readonly NoTranslationViewModel noTranslationViewModel;

        private bool HasSelectedGolin => this.SelectedGolin != null;
        public TranslationViewModel TranslationViewModel => this.HasSelectedGolin ? this.translationViewModel : this.noTranslationViewModel;

        private string userInput;
        public string UserInput
        {
            get { return this.userInput; }
            set
            {
                this.userInput = value;
                this.NotifyOfPropertyChange(nameof(this.UserInput));
                this.NotifyOfPropertyChange(nameof(this.HasUserInput));
            }
        }

        public bool HasUserInput => !string.IsNullOrEmpty(this.UserInput);

        private bool isCompleted;
        public bool IsCompleted
        {
            get
            {
                return this.isCompleted;
            }
            set
            {
                this.isCompleted = value;
                this.NotifyOfPropertyChange(nameof(this.IsCompleted));
            }
        }

        public ISnackbarMessageQueue SnackbarMessageQueue { get; }

        private bool isCheatModeEnabled;
        public bool IsCheatModeEnabled
        {
            get
            {
                return this.isCheatModeEnabled;
            }
            set
            {
                this.isCheatModeEnabled = value;
                this.NotifyOfPropertyChange(nameof(this.IsCheatModeEnabled));
            }
        }

        public MainViewModel(
            Main main,
            UpdateSentenceCommand updateSentenceCommand,
            TranslationViewModel translationViewModel,
            NoTranslationViewModel noTranslationViewModel,
            ISnackbarMessageQueue snackbarMessageQueue,
            ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier, main)
        {
            this.main = main;
            this.updateSentenceCommand = updateSentenceCommand;
            this.translationViewModel = translationViewModel;
            this.noTranslationViewModel = noTranslationViewModel;
            this.SnackbarMessageQueue = snackbarMessageQueue;
        }

        public void CheckSentence()
        {
            if (this.UserInput == this.KanaSentence || this.UserInput == this.KanjiSentence)
            {
                this.IsCompleted = true;
                this.ShowSnackbarMessage($"(☞ﾟヮﾟ)☞ correct! 「{this.UserInput}」");
            }
            else if (this.UserInput.ToLower() == "japangolin")
            {
                this.CheatCodeEntered();
            }
            else
            {
                this.ShowSnackbarMessage($"(╯°□°)╯︵ ┻━┻ incorrect.  「{this.UserInput}」");
            }
        }

        public void GiveUpSentence()
        {
            this.IsCompleted = true;
        }

        public void NextSentence()
        {
            this.updateSentenceCommand.ExecuteAndNotify();
            this.UserInput = string.Empty;
            this.SelectedGolin = null;
            this.IsCompleted = false;
        }

        public void CheatCodeEntered()
        {
            this.IsCheatModeEnabled = true;
            this.ShowSnackbarMessage("↑ ↑ ↓ ↓ ← → ← → b a");
        }

        public void CopyAnswer()
        {
            Clipboard.SetText(this.KanjiSentence);
        }

        public void GoogleTranslate()
        {
            var url = $"https://translate.google.com/#ja/en/{this.KanjiSentence}";
            Process.Start(url);
        }

        private void UpdateTranslationViewModel(IGolin golin)
        {
            this.translationViewModel.Update(golin);
            this.NotifyOfPropertyChange(nameof(this.TranslationViewModel));
        }

        private void ShowSnackbarMessage(string message)
        {
            this.SnackbarMessageQueue.Enqueue(message);
        }
    }

    public class DesignTimeMainViewModel : MainViewModel
    {
        private readonly English english = new English("Japangolin");
        private readonly Japanese japanese = new Japanese("ジャパンゴリン", "日本蜥蜴", Conjugation.LongPresentAffirmative, ConjugationFunctions.JapaneseNoun);

        public new List<IGolin> GolinEnglish => new List<IGolin> { new DesignTimeGolin(this.english, this.japanese) };
        public new string KanaSentence => "ジャパンゴリン";
        public new string KanjiSentence => "日本蜥蜴";

        public DesignTimeMainViewModel() : base(null, null, new DesignTimeTranslationViewModel(), new DesignTimeNoTranslationViewModel(), null, null)
        {
        }
    }

    public class DesignTimeGolin : Golin
    {
        public DesignTimeGolin(English english, Japanese nounJapanese) : base(english, nounJapanese, new List<string> { "DesignTimeInformation" })
        {
        }
    }
}