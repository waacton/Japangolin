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

        private readonly Main main;
        private readonly UpdateJapanesePhraseCommand updateJapanesePhraseCommand;

        public string Kana => this.main.Kana;
        public string Romaji { get; set; }
        public string Kanji => this.main.Kanji.Any() ? this.main.Kanji.First() : "<no kanji available>";
        public string Meaning => this.main.Meaning;

        private bool IsRomajiCorrect => this.Romaji != null && this.Romaji.Equals(this.main.Romaji);

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

        public MainViewModel(Main main, UpdateJapanesePhraseCommand updateJapanesePhraseCommand, ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier, main)
        {
            this.main = main;
            this.updateJapanesePhraseCommand = updateJapanesePhraseCommand;
            this.Feedback = Feedback.None;
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
    }

    public class DesignTimeMainViewModel : MainViewModel
    {
        public new string Kana => "ジャッパンゴリン";
        public new string Romaji { get; set; } = "jappangorin";
        public new string Kanji => "日本蜥蜴";
        public new string Meaning => "Japangolin";
        public new Feedback Feedback => Feedback.Good;

        public DesignTimeMainViewModel() : base(null, null, null)
        {
        }
    }
}