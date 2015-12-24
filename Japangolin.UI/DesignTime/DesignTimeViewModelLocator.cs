namespace Wacton.Japangolin.UI.DesignTime
{
    using Ninject;

    using Wacton.Japangolin.UI.Mains;

    public class DesignTimeViewModelLocator
    {
        private static readonly IKernel Kernel;

        public static ShellViewModel ShellViewModel => Kernel.Get<ShellViewModel>();
        public static MainViewModel MainViewModel => ShellViewModel.MainViewModel;

        static DesignTimeViewModelLocator()
        {
            if (Kernel != null)
            {
                return;
            }

            Kernel = new StandardKernel();
            AppBootstrapper.SetupKernelBindingsForDesignTime(Kernel);
            
            SetupDesignTimeEnvironment();
        }

        private static void SetupDesignTimeEnvironment()
        {
            // nothing required currently
        }
    }
}
