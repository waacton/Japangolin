namespace Wacton.Japangolin.Desktop.Mains;

using System.Timers;
using Wacton.Japangolin.Desktop.MVVM;

public class SnackbarViewModel : ViewModelBase
{
    private readonly Timer snackbarTimer = new(3000);

    private bool isSnackbarActive;
    public bool IsSnackbarActive
    {
        get => isSnackbarActive;
        private set => SetField(ref isSnackbarActive, value);
    }

    public SnackbarViewModel(ModelWatcher modelWatcher)
        : base(modelWatcher)
    {
        snackbarTimer.Elapsed += HideSnackbar;
        snackbarTimer.AutoReset = false;
    }

    public void TriggerSnackbar()
    {
        IsSnackbarActive = true;
        snackbarTimer.Start();
    }

    private void HideSnackbar(object sender, ElapsedEventArgs e)
    {
        IsSnackbarActive = false;
    }
}