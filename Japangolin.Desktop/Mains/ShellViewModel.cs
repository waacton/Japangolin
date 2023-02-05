namespace Wacton.Japangolin.Desktop.Mains;

using Wacton.Japangolin.Desktop.MVVM;

public class ShellViewModel : ViewModelBase
{
    public MainViewModel MainViewModel { get; }
    public SettingsViewModel SettingsViewModel { get; }
    public SnackbarViewModel SnackbarViewModel { get; }

    public ShellViewModel(MainViewModel mainViewModel, 
        SettingsViewModel settingsViewModel, 
        SnackbarViewModel snackbarViewModel, 
        ModelWatcher modelWatcher)
        : base(modelWatcher)
    {
        MainViewModel = mainViewModel;
        SettingsViewModel = settingsViewModel;
        SnackbarViewModel = snackbarViewModel;
    }
}