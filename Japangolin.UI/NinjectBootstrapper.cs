namespace Wacton.Japangolin.UI
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Caliburn.Micro;
    using Ninject;

    // hooks up the Ninject's DI kernel into Caliburn's bootstrapper
    public abstract class NinjectBootstrapper<T> : BootstrapperBase
    {
        private IKernel kernel;

        protected NinjectBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<T>();
        }

        protected override void Configure()
        {
            kernel = new StandardKernel();

            // bind caliburn objects (potentially not needed?)
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            
            ConfigureApplication(kernel);
        }

        protected abstract void ConfigureApplication(IKernel kernel);

        protected override object GetInstance(Type service, string key)
        {
            if (service != null)
            {
                return kernel.Get(service);
            }

            throw new ArgumentNullException(nameof(service));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            kernel.Inject(instance);
        }
    }
}