namespace Wacton.Japangolin.UI.Mains
{
    using System;
    using System.Windows.Input;
    using Wacton.Japangolin.Domain.Commands;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Japangolin.Domain.MVVM;
    using Wacton.Japangolin.Domain.Utils;
    using Wacton.Japangolin.UI.MVVM;

    public class MainViewModel : ViewModelBase
    {
        private readonly Main main;
        private readonly UpdateWordAndInflectionCommand updateWordAndInflectionCommand;
        private readonly DetailViewModel detailViewModel;
        private readonly NoDetailViewModel noDetailViewModel;

        // domain-specific properties
        public string WordText => main.Word.English.ToLower();
        public string InflectionText => main.Inflection.PrettyDisplay();
        public string AnswerText => this.GetAnswerText();

        // view-specific properties
        private string inputText;
        public string InputText
        {
            get { return this.inputText; }
            set
            {
                this.inputText = value;
                this.NotifyOfPropertyChange(nameof(this.InputText));
            }
        }

        private DetailViewModel currentDetailViewModel;
        public DetailViewModel CurrentDetailViewModel
        {
            get { return this.currentDetailViewModel; }
            private set
            {
                this.currentDetailViewModel = value;
                this.NotifyOfPropertyChange(nameof(this.CurrentDetailViewModel));
            }
        }

        private bool isAnswerVisible;
        public bool IsAnswerVisible
        {
            get { return this.isAnswerVisible; }
            private set
            {
                this.isAnswerVisible = value;
                this.NotifyOfPropertyChange(nameof(this.IsAnswerVisible));
                this.NotifyOfPropertyChange(nameof(this.AnswerText));
            }
        }

        public SnackbarViewModel SnackbarViewModel { get; }

        public MainViewModel(Main main,
            UpdateWordAndInflectionCommand updateWordAndInflectionCommand,
            DetailViewModel detailViewModel,
            NoDetailViewModel noDetailViewModel,
            SnackbarViewModel snackbarViewModel,
            ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier, main)
        {
            this.main = main;
            this.updateWordAndInflectionCommand = updateWordAndInflectionCommand;
            this.detailViewModel = detailViewModel;
            this.noDetailViewModel = noDetailViewModel;
            this.SnackbarViewModel = snackbarViewModel;

            this.UpdateWordAndInflection();
        }

        private void UpdateWordAndInflection()
        {
            this.ResetView();
            this.updateWordAndInflectionCommand.ExecuteAndNotify();
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
            var wordClassDetail = PascalCase.InsertSeparator(main.Word.Class.ToString(), "-").ToLower();
            this.detailViewModel.Update(main.Hint.BaseForm, main.Hint.Modification, wordClassDetail);
            this.CurrentDetailViewModel = this.detailViewModel;
        }

        public void InputEntered(KeyEventArgs e)
        {
            if (this.IsInputCorrect())
            {
                this.UpdateWordAndInflection();
                this.SnackbarViewModel.TriggerSnackbar();
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
    }

    // --- design time ---

    public class DesignTimeMainViewModel : MainViewModel
    {
        public new string WordText => "ジャパンゴリン";
        public new string InflectionText => "ジャパンゴリン";
        public new string AnswerText => "ジャパンゴリン";
        public new DetailViewModel CurrentDetailViewModel => new DesignTimeDetailViewModel();
        public new bool IsAnswerVisible => true;

        public DesignTimeMainViewModel() : base(null, null, new DesignTimeDetailViewModel(), new DesignTimeNoDetailViewModel(), new DesignTimeSnackbarViewModel(), null)
        {
        }
    }
}