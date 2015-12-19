namespace Wacton.Japangolin.UI
{
    using Ninject;

    using Wacton.Japangolin.Domain.JapanesePhrases;
    using Wacton.Japangolin.Domain.Mains;

    public class JapangolinBootstrapper
    {
        public static void SetupKernelBindings(IKernel kernel)
        {
            kernel.Bind<JapanesePhraseProvider>().ToSelf().InSingletonScope();
            kernel.Bind<Main>().ToSelf().InSingletonScope();
        }
    }
}
