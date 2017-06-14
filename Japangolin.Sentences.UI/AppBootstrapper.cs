namespace Wacton.Japangolin.Sentences.UI
{
    using Ninject;

    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Sentences.Domain.Mains;
    using Wacton.Japangolin.Sentences.UI.Mains;
    using Wacton.Tovarisch.UI;

    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureApplication(IKernel kernel)
        {
            SetupKernelBindings(kernel);
        }

        public static void SetupKernelBindings(IKernel kernel)
        {
            kernel.Bind<IJapaneseDictionary>().To<JapaneseDictionary>().InSingletonScope();
            kernel.Bind<Main>().ToSelf().InSingletonScope();
            kernel.Bind<TranslationViewModel>().ToSelf().InSingletonScope();
        }
    }
}