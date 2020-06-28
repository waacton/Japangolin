namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using System.Windows.Input;

    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class ShellViewModel : ViewModelBase
    {
        private readonly KonamiCode konamiCode = new KonamiCode();

        public static string WindowTitle { get; set; }
        public MainViewModel MainViewModel { get; }

        public ShellViewModel(MainViewModel mainViewModel, ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier)
        {
            this.MainViewModel = mainViewModel;
        }

        public void KeypressDetected(KeyEventArgs e)
        {
            if (this.MainViewModel.IsCheatModeEnabled)
            {
                return;
            }

            this.konamiCode.PressKey(e.Key);
            if (this.konamiCode.IsComplete)
            {
                this.MainViewModel.CheatCodeEntered();
            }
        }
    }

    public class DesignTimeShellViewModel : ShellViewModel
    {
        public DesignTimeShellViewModel() : base(new DesignTimeMainViewModel(), null)
        {
        }
    }
}