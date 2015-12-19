namespace Wacton.Japangolin.UI.DesignTime
{
    using Ninject;

    using Wacton.Japangolin.UI.Mains;

    public class DesignTimeViewModelLocator
    {
        private static readonly IKernel Kernel;

        static DesignTimeViewModelLocator()
        {
            if (Kernel != null)
            {
                return;
            }

            Kernel = new StandardKernel();
            JapangolinBootstrapper.SetupKernelBindings(Kernel);
        }

        public static MainViewModel MainViewModel => Kernel.Get<MainViewModel>();

        private static void SetupDesignTimeEnvironment()
        {
            MainViewModel.ResetPhrase();
            MainViewModel.Romaji = "ワカトン";
            MainViewModel.NotifyOfPropertyChange(() => MainViewModel.Romaji);
        }
    }
}
