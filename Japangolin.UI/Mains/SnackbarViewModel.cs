namespace Wacton.Japangolin.UI.Mains
{
    using System.Timers;
    using Wacton.Tovarisch.MVVM;
    using Wacton.Tovarisch.UI.MVVM;

    public class SnackbarViewModel : ViewModelBase
    {
        private readonly Timer snackbarTimer = new Timer(3000);

        private bool isSnackbarActive;
        public bool IsSnackbarActive
        {
            get
            {
                return this.isSnackbarActive;
            }

            private set
            {
                this.isSnackbarActive = value;
                this.NotifyOfPropertyChange(nameof(this.IsSnackbarActive));
            }
        }

        public SnackbarViewModel(ModelChangeNotifier modelChangeNotifier)
            : base(modelChangeNotifier)
        {
            snackbarTimer.Elapsed += HideSnackbar;
            snackbarTimer.AutoReset = false;
        }

        public void TriggerSnackbar()
        {
            this.IsSnackbarActive = true;
            snackbarTimer.Start();
        }

        private void HideSnackbar(object sender, ElapsedEventArgs e)
        {
            this.IsSnackbarActive = false;
        }
    }

    // --- design time ---

    public class DesignTimeSnackbarViewModel : SnackbarViewModel
    {
        public new bool IsSnackbarActive => true;

        public DesignTimeSnackbarViewModel() : base(null)
        {
        }
    }
}