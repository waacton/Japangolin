namespace Wacton.Japangolin.UI.Mains
{
    using Wacton.Japangolin.Domain.MVVM;
    using Wacton.Japangolin.UI.MVVM;

    public class ShellViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
        public SnackbarViewModel SnackbarViewModel { get; }

        public ShellViewModel(MainViewModel mainViewModel, 
            SettingsViewModel settingsViewModel, 
            SnackbarViewModel snackbarViewModel, 
            ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier)
        {
            this.MainViewModel = mainViewModel;
            this.SettingsViewModel = settingsViewModel;
            this.SnackbarViewModel = snackbarViewModel;
        }
    }
}