namespace Wacton.Japangolin
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;

    using Wacton.Japangolin.UI.Mains;
    using Wacton.Tovarisch.Logging;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            SetWindowTitle();
        }

        private static void SetWindowTitle()
        {
            var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;
            ShellViewModel.WindowTitle = $"Wacton.Japangolin ({applicationVersion})";
        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Default.Error(e.Exception, "Unhandled exception (dispatcher)");
        }

        private static void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Default.Error((Exception)e.ExceptionObject, "Unhandled exception (domain)");
        }

        private static void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.Default.Error(e.Exception, "Unhandled exception (unobserved task)");
        }
    }
}
