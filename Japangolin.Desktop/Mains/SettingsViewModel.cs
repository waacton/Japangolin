namespace Wacton.Japangolin.Desktop.Mains;

using System.Windows.Input;
using Wacton.Japangolin.Core.Enums;
using Wacton.Japangolin.Core.Mains;
using Wacton.Japangolin.Core.Mutations;
using Wacton.Japangolin.Desktop.MVVM;

public class SettingsViewModel : ViewModelBase
{
    private readonly Settings settings;
    private readonly WordFilterMutation wordFilterMutation;

    public bool IsJLPTN5 => settings.WordFilter == WordFilter.JLPTN5;
        
    public ICommand ToggleFilterCommand { get; }

    public SettingsViewModel(Settings settings, WordFilterMutation wordFilterMutation, ModelWatcher modelWatcher)
        : base(modelWatcher, settings)
    {
        this.settings = settings;
        this.wordFilterMutation = wordFilterMutation;
        ToggleFilterCommand = new RelayCommand(_ => ToggleWordFilter());
    }

    private async void ToggleWordFilter()
    {
        var wordFilter = settings.WordFilter == WordFilter.None ? WordFilter.JLPTN5 : WordFilter.None;
        await wordFilterMutation.ExecuteAsync(wordFilter, changedObjects => ModelWatcher.Notify(changedObjects));
    }
}