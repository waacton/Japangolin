namespace Wacton.Japangolin.UI.Mains
{
    using System.Timers;
    using Wacton.Japangolin.Domain.MVVM;
    using Wacton.Japangolin.UI.MVVM;

    public class SnackbarViewModel : ViewModelBase
    {
        private readonly Timer snackbarTimer = new Timer(3000);

        private bool isSnackbarActive;
        public bool IsSnackbarActive
        {
            get => this.isSnackbarActive;
            private set => SetField(ref isSnackbarActive, value);
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
}