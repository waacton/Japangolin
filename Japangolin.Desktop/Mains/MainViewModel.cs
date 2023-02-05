namespace Wacton.Japangolin.Desktop.Mains;

using System.Windows.Input;
using Wacton.Japangolin.Core.Mains;
using Wacton.Japangolin.Core.Mutations;
using Wacton.Japangolin.Core.Utils;
using Wacton.Japangolin.Desktop.MVVM;

public class MainViewModel : ViewModelBase
{
    private readonly Main main;
    private readonly WordAndInflectionMutation wordAndInflectionMutation;
    private readonly DetailViewModel detailViewModel;
    private readonly NoDetailViewModel noDetailViewModel;
    private readonly SnackbarViewModel snackbarViewModel;

    // domain-specific properties
    public string WordText => main.Word.English.ToLower();
    public string InflectionText => main.Inflection.PrettyDisplay();
    public string AnswerText => GetAnswerText();

    // view-specific properties
    private string inputText;
    public string InputText
    {
        get => inputText;
        set => SetField(ref inputText, value);
    }

    private DetailViewModel currentDetailViewModel;
    public DetailViewModel CurrentDetailViewModel
    {
        get => currentDetailViewModel;
        set => SetField(ref currentDetailViewModel, value);
    }

    private bool isAnswerVisible;
    public bool IsAnswerVisible
    {
        get => isAnswerVisible;
        private set
        {
            SetField(ref isAnswerVisible, value);
            OnPropertyChanged(nameof(AnswerText));
        }
    }
        
    public ICommand SkipCommand { get; }
    public ICommand ShowAnswerCommand { get; }

    public MainViewModel(Main main,
        WordAndInflectionMutation wordAndInflectionMutation,
        DetailViewModel detailViewModel,
        NoDetailViewModel noDetailViewModel,
        SnackbarViewModel snackbarViewModel,
        ModelWatcher modelWatcher)
        : base(modelWatcher, main)
    {
        this.main = main;
        this.wordAndInflectionMutation = wordAndInflectionMutation;
        this.detailViewModel = detailViewModel;
        this.noDetailViewModel = noDetailViewModel;
        this.snackbarViewModel = snackbarViewModel;
        SkipCommand = new RelayCommand(_ => UpdateWordAndInflection());
        ShowAnswerCommand = new RelayCommand(_ => IsAnswerVisible = true);

        UpdateWordAndInflection();
    }

    private void UpdateWordAndInflection()
    {
        ResetView();
        wordAndInflectionMutation.Execute(changedObjects => ModelWatcher.Notify(changedObjects));
    }

    private void ResetView()
    {
        InputText = null;
        CurrentDetailViewModel = noDetailViewModel;
        IsAnswerVisible = false;
    }

    public void WordSelected()
    {
        var wordClassDetail = StringUtils.PascalCase(main.Word.Class.ToString(), "-").ToLower();
        var isKanjiDifferent = main.Word.Kanji != main.Word.Kana;
        detailViewModel.Update(main.Word.Kana, isKanjiDifferent ? main.Word.Kanji : null, wordClassDetail);
        CurrentDetailViewModel = detailViewModel;
    }

    public void InflectionSelected()
    {
        var wordClassDetail = StringUtils.PascalCase(main.Word.Class.ToString(), "-").ToLower();
        detailViewModel.Update(main.Hint.BaseForm, main.Hint.Modification, wordClassDetail);
        CurrentDetailViewModel = detailViewModel;
    }

    public void InputEntered()
    {
        if (!IsInputCorrect())
        {
            return;
        }
            
        UpdateWordAndInflection();
        snackbarViewModel.TriggerSnackbar();
    }
        
    private bool IsInputCorrect()
    {
        if (InputText == null)
        {
            return false;
        }

        return InputText == main.AnswerKana || InputText == main.AnswerKanji;
    }

    private string GetAnswerText()
    {
        if (!IsAnswerVisible)
        {
            return "Click to reveal the answer";
        }

        var isKanjiDifferent = main.AnswerKanji != main.AnswerKana;
        return isKanjiDifferent ? $"{main.AnswerKana} · {main.AnswerKanji}" : $"{main.AnswerKana}";
    }
}