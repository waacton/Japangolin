namespace Wacton.Japangolin.UI.Mains
{
    using Wacton.Tovarisch.MVVM;

    public class ShellViewModel : ViewModelBase
    {
        public static string WindowTitle { get; set; }
        public MainViewModel MainViewModel { get; }

        public ShellViewModel(MainViewModel mainViewModel, ModelChanger modelChanger)
            : base(modelChanger)
        {
            this.MainViewModel = mainViewModel;
        }
    }
}