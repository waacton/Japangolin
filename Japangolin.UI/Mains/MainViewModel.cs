namespace Wacton.Japangolin.UI.Mains
{
    using System;
    using System.Linq;
    using System.Windows.Input;

    using Wacton.Japangolin.Domain.DomainCommands;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Tovarisch.Delegates;
    using Wacton.Tovarisch.MVVM;

    public class MainViewModel : ViewModelBase
    {
        private readonly Main mainModel;
        private readonly UpdateJapanesePhraseCommand updateJapanesePhraseCommand;

        public string Kana => this.mainModel.Kana;
        public string Romaji { get; set; }
        public string Kanji => this.mainModel.Kanji.Any() ? this.mainModel.Kanji.First() : "<no kanji available>";
        public string Meaning => this.mainModel.Meaning;

        private bool IsRomajiCorrect => this.Romaji != null && this.Romaji.Equals(this.mainModel.Romaji);

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

        public MainViewModel(Main mainModel, UpdateJapanesePhraseCommand updateJapanesePhraseCommand, CommandInvoker commandInvoker)
            : base(commandInvoker, mainModel)
        {
            this.mainModel = mainModel;
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
            action.InvokeAfterDelay(TimeSpan.FromSeconds(0.75));
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