namespace Wacton.Japangolin.UI.Mains
{
    using System.Windows.Input;
    using Wacton.Japangolin.Domain.Actions;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Japangolin.Domain.MVVM;
    using Wacton.Japangolin.Domain.Utils;
    using Wacton.Japangolin.UI.MVVM;

    public class MainViewModel : ViewModelBase
    {
        private readonly Main main;
        private readonly UpdateWordAndInflectionAction updateWordAndInflectionAction;
        private readonly DetailViewModel detailViewModel;
        private readonly NoDetailViewModel noDetailViewModel;
        private readonly SnackbarViewModel snackbarViewModel;

        // domain-specific properties
        public string WordText => main.Word.English.ToLower();
        public string InflectionText => main.Inflection.PrettyDisplay();
        public string AnswerText => this.GetAnswerText();

        // view-specific properties
        private string inputText;
        public string InputText
        {
            get => this.inputText;
            set => SetField(ref inputText, value);
        }

        private DetailViewModel currentDetailViewModel;
        public DetailViewModel CurrentDetailViewModel
        {
            get => this.currentDetailViewModel;
            set => SetField(ref currentDetailViewModel, value);
        }

        private bool isAnswerVisible;
        public bool IsAnswerVisible
        {
            get => this.isAnswerVisible;
            private set
            {
                SetField(ref isAnswerVisible, value);
                OnPropertyChanged(nameof(AnswerText));
            }
        }
        
        public ICommand SkipCommand { get; }
        public ICommand ShowAnswerCommand { get; }

        public MainViewModel(Main main,
            UpdateWordAndInflectionAction updateWordAndInflectionAction,
            DetailViewModel detailViewModel,
            NoDetailViewModel noDetailViewModel,
            SnackbarViewModel snackbarViewModel,
            ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier, main)
        {
            this.main = main;
            this.updateWordAndInflectionAction = updateWordAndInflectionAction;
            this.detailViewModel = detailViewModel;
            this.noDetailViewModel = noDetailViewModel;
            this.snackbarViewModel = snackbarViewModel;
            SkipCommand = new RelayCommand(_ => UpdateWordAndInflection());
            ShowAnswerCommand = new RelayCommand(_ => IsAnswerVisible = true);

            this.UpdateWordAndInflection();
        }

        private void UpdateWordAndInflection()
        {
            this.ResetView();
            this.updateWordAndInflectionAction.ExecuteAndNotify();
        }

        private void ResetView()
        {
            this.InputText = null;
            this.CurrentDetailViewModel = this.noDetailViewModel;
            this.IsAnswerVisible = false;
        }

        public void WordSelected()
        {
            var wordClassDetail = StringUtils.PascalCase(main.Word.Class.ToString(), "-").ToLower();
            var isKanjiDifferent = main.Word.Kanji != main.Word.Kana;
            this.detailViewModel.Update(main.Word.Kana, isKanjiDifferent ? main.Word.Kanji : null, wordClassDetail);
            this.CurrentDetailViewModel = this.detailViewModel;
        }

        public void InflectionSelected()
        {
            var wordClassDetail = StringUtils.PascalCase(main.Word.Class.ToString(), "-").ToLower();
            this.detailViewModel.Update(main.Hint.BaseForm, main.Hint.Modification, wordClassDetail);
            this.CurrentDetailViewModel = this.detailViewModel;
        }

        public void InputEntered()
        {
            if (!this.IsInputCorrect())
            {
                return;
            }
            
            this.UpdateWordAndInflection();
            this.snackbarViewModel.TriggerSnackbar();
        }
        
        private bool IsInputCorrect()
        {
            if (this.InputText == null)
            {
                return false;
            }

            return this.InputText == main.AnswerKana || this.InputText == main.AnswerKanji;
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
}