namespace Wacton.Japangolin.UI
{
    using Ninject;

    using Wacton.Japangolin.Domain.DesignTime;
    using Wacton.Japangolin.Domain.JapanesePhrases;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Japangolin.UI.Mains;
    using Wacton.Tovarisch.MVVM;

    public class AppBootstrapper : Bootstrapper<MainViewModel>
    {
        protected override void ConfigureApplication(IKernel kernel)
        {
            SetupKernelBindings(kernel);
        }

        public static void SetupKernelBindings(IKernel kernel)
        {
            kernel.Bind<IJapanesePhraseProvider>().To<JapanesePhraseProvider>().InSingletonScope();
            kernel.Bind<Main>().ToSelf().InSingletonScope();
        }

        public static void SetupKernelBindingsForDesignTime(IKernel kernel)
        {
            SetupKernelBindings(kernel);
            kernel.Rebind<IJapanesePhraseProvider>().To<DesignTimeJapanesePhraseProvider>().InSingletonScope();
        }
    }
}