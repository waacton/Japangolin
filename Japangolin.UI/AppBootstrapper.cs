namespace Wacton.Japangolin.UI
{
    using System.Linq;
    using Ninject;
    using Wacton.Desu.Japanese;
    using Wacton.Japangolin.Domain.Mains;
    using Wacton.Japangolin.Domain.MVVM;
    using Wacton.Japangolin.UI.Mains;

    public class AppBootstrapper : NinjectBootstrapper<ShellViewModel>
    {
        protected override void ConfigureApplication(IKernel kernel)
        {
            SetupKernelBindings(kernel);
        }

        public static void SetupKernelBindings(IKernel kernel)
        {
            var japaneseEntries = JapaneseDictionary.ParseEntries().ToList();
            kernel.Bind<Settings>().To<Settings>().InSingletonScope();
            kernel.Bind<Main>().ToSelf().InSingletonScope().WithConstructorArgument(japaneseEntries); // ninject knows which argument based on type
            
            kernel.Bind<ModelChangeNotifier>().ToSelf().InSingletonScope();
            kernel.Bind<DetailViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<NoDetailViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<SettingsViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<SnackbarViewModel>().ToSelf().InSingletonScope();
        }
    }
}