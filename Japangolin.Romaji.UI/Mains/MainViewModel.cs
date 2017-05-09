namespace Wacton.Japangolin.Romaji.UI.Mains
{
    using System;
    using System.Linq;
    using System.Windows.Input;

    using Wacton.Japangolin.Romaji.Domain.Commands;
    using Wacton.Japangolin.Romaji.Domain.Mains;
    using Wacton.Tovarisch.Delegates;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class MainViewModel : ViewModelBase
    {
        public static string Title { get; set; }

        private readonly RomajiMain romajiMain;
        private readonly UpdateJapanesePhraseCommand updateJapanesePhraseCommand;

        private readonly SentenceMain sentenceMain;
        private readonly UpdateSentenceCommand updateSentenceCommand;

        public string EnglishSentence => this.sentenceMain.EnglishSentence;
        public string Help => this.sentenceMain.Help;
        public string KanaSentence => this.sentenceMain.KanaSentence;
        public string KanjiSentence => this.sentenceMain.KanjiSentence;

        public string Kana => this.romajiMain.Kana;
        public string Romaji { get; set; }
        public string Kanji => this.romajiMain.Kanji.Any() ? this.romajiMain.Kanji.First() : "<no kanji available>";
        public string Meaning => this.romajiMain.Meaning;

        private bool IsRomajiCorrect => this.Romaji != null && this.Romaji.Equals(this.romajiMain.Romaji);

        private Feedback feedback;
        public Feedback Feedback
        {
            get
            {
                return this.feedback;
            }
            set
            {
                this.feedback = value;
                this.NotifyOfPropertyChange(nameof(this.Feedback));
            }
        }

        private bool isFanfare;
        public bool IsFanfare
        {
            get
            {
                return this.isFanfare;
            }
            set
            {
                this.isFanfare = value;
                this.NotifyOfPropertyChange(nameof(this.IsFanfare));
            }
        }

        public MainViewModel(RomajiMain romajiMain, UpdateJapanesePhraseCommand updateJapanesePhraseCommand, SentenceMain sentenceMain, UpdateSentenceCommand updateSentenceCommand, ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier, romajiMain, sentenceMain)
        {
            this.romajiMain = romajiMain;
            this.updateJapanesePhraseCommand = updateJapanesePhraseCommand;
            this.Feedback = Feedback.None;

            this.sentenceMain = sentenceMain;
            this.updateSentenceCommand = updateSentenceCommand;
        }

        public void RomajiEntered(KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Enter))
            {
                return;
            }

            if (this.IsFanfare)
            {
                this.ResetPhrase();
                return;
            }

            if (this.IsRomajiCorrect)
            {
                this.PhraseEnteredCorrectly();
            }
            else
            {
                this.PhraseEnteredIncorrectly();
            }
        }

        private void PhraseEnteredIncorrectly()
        {
            this.Feedback = Feedback.Bad;
            Action action = this.ClearBadFeedback;
            action.InvokeAfterDelay(TimeSpan.FromSeconds(1));
        }

        private void PhraseEnteredCorrectly()
        {
            this.IsFanfare = true;
            this.Feedback = Feedback.Good;
        }

        private void ClearBadFeedback()
        {
            if (this.Feedback.Equals(Feedback.Bad))
            {
                this.Feedback = Feedback.None;
            }
        }

        public void ResetPhrase()
        {
            this.Romaji = string.Empty;
            this.updateJapanesePhraseCommand.ExecuteAndNotify();
            this.IsFanfare = false;
            this.Feedback = Feedback.None;
        }

        public void NextSentence()
        {
            this.updateSentenceCommand.ExecuteAndNotify();
        }
    }

    public class DesignTimeMainViewModel : MainViewModel
    {
        public new string Kana => "ジャッパンゴリン";
        public new string Romaji { get; set; } = "jappangorin";
        public new string Kanji => "日本蜥蜴";
        public new string Meaning => "Japangolin";
        public new Feedback Feedback => Feedback.Good;

        public DesignTimeMainViewModel() : base(null, null, null, null, null)
        {
        }
    }
}