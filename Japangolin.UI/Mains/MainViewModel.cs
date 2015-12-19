namespace Wacton.Japangolin.UI.Mains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Media;

    using Wacton.Japangolin.Domain;
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

        private Dictionary<Feedback, string> feedbackTexts = SetupFeedbackTexts();
        private Dictionary<Feedback, SolidColorBrush> feedbackBrushes = SetupFeedbackBrushes();

        private SolidColorBrush feedbackBrush;
        public SolidColorBrush FeedbackBrush
        {
            get
            {
                return this.feedbackBrush;
            }
            set
            {
                this.feedbackBrush = value;
                this.NotifyOfPropertyChange(() => this.FeedbackBrush);
            }
        }

        private string feedbackText;
        public string FeedbackText
        {
            get
            {
                return this.feedbackText;
            }
            set
            {
                this.feedbackText = value;
                this.NotifyOfPropertyChange(() => this.FeedbackText);
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
                this.NotifyOfPropertyChange(() => this.IsFanfare);
            }
        }

        public MainViewModel(Main mainModel, UpdateJapanesePhraseCommand updateJapanesePhraseCommand, CommandInvoker commandInvoker)
            : base(commandInvoker, mainModel)
        {
            this.mainModel = mainModel;
            this.updateJapanesePhraseCommand = updateJapanesePhraseCommand;
            this.UpdateFeedback(Feedback.None);
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
            this.UpdateFeedback(Feedback.Bad);
            Action<Feedback> action = this.UpdateFeedback;
            action.InvokeAfterDelay(Feedback.None, TimeSpan.FromSeconds(1));
        }

        private void PhraseEnteredCorrectly()
        {
            this.IsFanfare = true;
            this.UpdateFeedback(Feedback.Good);
        }

        public void ResetPhrase()
        {
            this.Romaji = string.Empty;
            this.updateJapanesePhraseCommand.ExecuteAndNotify();
            this.IsFanfare = false;
            this.UpdateFeedback(Feedback.None);
        }

        private void UpdateFeedback(Feedback feedback)
        {
            this.FeedbackText = this.feedbackTexts[feedback];
            this.FeedbackBrush = this.feedbackBrushes[feedback];
        }

        private static Dictionary<Feedback, string> SetupFeedbackTexts()
        {
            return new Dictionary<Feedback, string>
                   {
                       { Feedback.None, string.Empty },
                       { Feedback.Bad, "馬鹿 [○・｀Д´・○]" },
                       { Feedback.Good, "万歳！！！ ヽ(=^･ω･^=)丿" }
                   };
        }

        private static Dictionary<Feedback, SolidColorBrush> SetupFeedbackBrushes()
        {
            return new Dictionary<Feedback, SolidColorBrush>
                   {
                       { Feedback.None, Brushes.White },
                       { Feedback.Bad, Brushes.Tomato },
                       { Feedback.Good, Brushes.MediumSeaGreen }
                   };
        }
    }
}