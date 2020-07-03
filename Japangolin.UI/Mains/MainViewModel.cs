namespace Wacton.Japangolin.UI.Mains
{
    using System.Timers;
    using System.Windows.Input;
    using Wacton.Japangolin.Domain.Commands;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Japangolin.Domain.Utils;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class MainViewModel : ViewModelBase
    {
        private readonly Main main;
        private readonly UpdateCommand updateCommand;
        private readonly DetailViewModel detailViewModel;
        private readonly NoDetailViewModel noDetailViewModel;
        private readonly Timer snackbarTimer = new Timer(3000);

        // domain-specific properties
        public string WordText => main.Word.English.ToLower();
        public string InflectionText => main.Inflection.PrettyDisplay();
        public string AnswerText => this.GetAnswerText();

        // view-specific properties
        private string inputText;
        public string InputText
        {
            get
            {
                return this.inputText;
            }

            set
            {
                this.inputText = value;
                this.NotifyOfPropertyChange(nameof(this.InputText));
            }
        }

        private DetailViewModel currentDetailViewModel;
        public DetailViewModel CurrentDetailViewModel
        {
            get
            {
                return this.currentDetailViewModel;
            }

            private set
            {
                this.currentDetailViewModel = value;
                this.NotifyOfPropertyChange(nameof(this.CurrentDetailViewModel));
            }
        }

        private bool isAnswerVisible;
        public bool IsAnswerVisible
        {
            get
            {
                return this.isAnswerVisible;
            }

            private set
            {
                this.isAnswerVisible = value;
                this.NotifyOfPropertyChange(nameof(this.IsAnswerVisible));
                this.NotifyOfPropertyChange(nameof(this.AnswerText));
            }
        }

        private bool isSnackbarActive;
        public bool IsSnackbarActive
        {
            get
            {
                return this.isSnackbarActive;
            }

            private set
            {
                this.isSnackbarActive = value;
                this.NotifyOfPropertyChange(nameof(this.IsSnackbarActive));
            }
        }

        public MainViewModel(Main main,
            UpdateCommand updateCommand,
            DetailViewModel detailViewModel,
            NoDetailViewModel noDetailViewModel,
            ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier, main)
        {
            this.main = main;
            this.updateCommand = updateCommand;
            this.detailViewModel = detailViewModel;
            this.noDetailViewModel = noDetailViewModel;

            this.ResetView();

            snackbarTimer.Elapsed += HideSnackbar;
            snackbarTimer.AutoReset = false;
        }

        private void UpdateWordAndInflection()
        {
            this.ResetView();
            this.updateCommand.ExecuteAndNotify();
        }

        private void ResetView()
        {
            this.InputText = null;
            this.CurrentDetailViewModel = this.noDetailViewModel;
            this.IsAnswerVisible = false;
        }

        public void WordSelected()
        {
            var wordClassDetail = PascalCase.InsertSeparator(main.Word.Class.ToString(), "-").ToLower();
            var isKanjiDifferent = main.Word.Kanji != main.Word.Kana;
            this.detailViewModel.Update(main.Word.Kana, isKanjiDifferent ? main.Word.Kanji : null, wordClassDetail);
            this.CurrentDetailViewModel = this.detailViewModel;
        }

        public void InflectionSelected()
        {
            this.detailViewModel.Update(main.Hint);
            this.CurrentDetailViewModel = this.detailViewModel;
        }

        public void InputEntered(KeyEventArgs e)
        {
            if (this.IsInputCorrect())
            {
                this.UpdateWordAndInflection();

                this.IsSnackbarActive = true;
                snackbarTimer.Start();
            }
        }
        private bool IsInputCorrect()
        {
            if (this.InputText == null)
            {
                return false;
            }

            return this.InputText == main.AnswerKana || this.InputText == main.AnswerKanji;
        }

        public void Skip()
        {
            this.UpdateWordAndInflection();
        }

        public void ViewAnswer()
        {
            this.IsAnswerVisible = true;
            this.NotifyOfPropertyChange(nameof(this.AnswerText));
        }

        private string GetAnswerText()
        {
            if (!this.IsAnswerVisible)
            {
                return "Click to reveal the answer";
            }

            var isKanjiDifferent = main.AnswerKanji != main.AnswerKana;
            return isKanjiDifferent ? $"{main.AnswerKana} · {main.AnswerKanji}" : $"{main.AnswerKana}";
        }

        private void HideSnackbar(object sender, ElapsedEventArgs e)
        {
            this.IsSnackbarActive = false;
        }
    }

    // --- design time ---

    public class DesignTimeMainViewModel : MainViewModel
    {
        public new string WordText => "ジャパンゴリン";
        public new string InflectionText => "ジャパンゴリン";
        public new string AnswerText => "ジャパンゴリン";
        public new DetailViewModel CurrentDetailViewModel => new DesignTimeDetailViewModel();
        public new bool IsAnswerVisible => true;

        public DesignTimeMainViewModel() : base(null, null, new DesignTimeDetailViewModel(), new DesignTimeNoDetailViewModel(), null)
        {
        }
    }
}