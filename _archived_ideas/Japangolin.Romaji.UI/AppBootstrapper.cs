namespace Wacton.Japangolin.Romaji.UI
{
    using Ninject;

    using Wacton.Japangolin.Romaji.Domain.JapanesePhrases;
    using Wacton.Japangolin.Romaji.Domain.Mains;
    using Wacton.Japangolin.Romaji.UI.Mains;
    using Wacton.Tovarisch.UI;

    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureApplication(IKernel kernel)
        {
            SetupKernelBindings(kernel);
        }

        public static void SetupKernelBindings(IKernel kernel)
        {
            kernel.Bind<IJapanesePhraseRepository>().To<JapanesePhraseRepository>().InSingletonScope();
            kernel.Bind<Main>().ToSelf().InSingletonScope();
        }
    }
}