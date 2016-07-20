namespace Wacton.Japangolin.UI.Mains
{
    using Wacton.Tovarisch.MVVM;

    public class ShellViewModel : ViewModelBase
    {
        public static string WindowTitle { get; set; }
        public MainViewModel MainViewModel { get; }

        public ShellViewModel(MainViewModel mainViewModel, ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier)
        {
            this.MainViewModel = mainViewModel;
        }
    }

    public class DesignTimeShellViewModel : ShellViewModel
    {
        public DesignTimeShellViewModel() : base(new DesignTimeMainViewModel(), null)
        {
        }
    }
}