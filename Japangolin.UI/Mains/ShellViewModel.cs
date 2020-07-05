namespace Wacton.Japangolin.UI.Mains
{
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class ShellViewModel : ViewModelBase
    {
        public static string WindowTitle { get; set; }
        public MainViewModel MainViewModel { get; }
        public SnackbarViewModel SnackbarViewModel { get; }

        public ShellViewModel(MainViewModel mainViewModel, SnackbarViewModel snackbarViewModel, ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier)
        {
            this.MainViewModel = mainViewModel;
            this.SnackbarViewModel = snackbarViewModel;
        }
    }

    // --- design time ---

    public class DesignTimeShellViewModel : ShellViewModel
    {
        public DesignTimeShellViewModel() : base(new DesignTimeMainViewModel(), new DesignTimeSnackbarViewModel(), null)
        {
        }
    }
}