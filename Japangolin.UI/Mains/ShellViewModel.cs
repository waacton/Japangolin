namespace Wacton.Japangolin.UI.Mains
{
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class ShellViewModel : ViewModelBase
    {
        public static string WindowTitle { get; set; }
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

    // --- design time ---

    public class DesignTimeShellViewModel : ShellViewModel
    {
        public DesignTimeShellViewModel() : base(new DesignTimeMainViewModel(), new DesignTimeSettingsViewModel(), new DesignTimeSnackbarViewModel(), null)
        {
        }
    }
}