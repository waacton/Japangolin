namespace Wacton.Japangolin.UI
{
    using Ninject;

    using Wacton.Japangolin.Domain.JapanesePhrases;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Japangolin.UI.Mains;
    using Wacton.Tovarisch.MVVM;

    public class AppBootstrapper : Bootstrapper<MainViewModel>
    {
        protected override void ConfigureApplication()
        {
            SetupKernelBindings(this.Kernel);
        }

        public static void SetupKernelBindings(IKernel kernel)
        {
            kernel.Bind<JapanesePhraseProvider>().ToSelf().InSingletonScope();
            kernel.Bind<Main>().ToSelf().InSingletonScope();
        }
    }
}