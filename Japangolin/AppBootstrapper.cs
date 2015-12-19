namespace Wacton.Japangolin
{
    using Wacton.Japangolin.UI;
    using Wacton.Japangolin.UI.Mains;
    using Wacton.Tovarisch.MVVM;

    public class AppBootstrapper : Bootstrapper<MainViewModel>
    {
        protected override void ConfigureApplication()
        {
            JapangolinBootstrapper.SetupKernelBindings(this.Kernel);
        }
    }
}
