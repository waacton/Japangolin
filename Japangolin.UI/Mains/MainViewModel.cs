namespace Wacton.Japangolin.UI.Mains
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;

    using Wacton.Japangolin.Domain.Commands;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Tovarisch.Delegates;
    using Wacton.Tovarisch.MVVM;

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

        public MainViewModel(Main main, UpdateJapanesePhraseCommand updateJapanesePhraseCommand, ModelChanger modelChanger)
            : base(modelChanger, main)
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
}