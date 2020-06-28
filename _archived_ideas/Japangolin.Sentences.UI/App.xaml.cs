namespace Wacton.Japangolin.Sentences.UI
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;

    using Wacton.Japangolin.Sentences.UI.Mains;
    using Wacton.Japangolin.Sentences.UI.Themes;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            Stylist.SetStyle(Swatch.Grey, Swatch.Red, false);
            base.OnStartup(e);
        }

        private static void SetWindowTitle()
        {
            var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;
            ShellViewModel.WindowTitle = $"Wacton.Japangolin.Sentences ({applicationVersion})";
        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Fatal(e.Exception, "Unhandled exception (dispatcher)");
        }

        private static void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal((Exception)e.ExceptionObject, "Unhandled exception (domain)");
        }

        private static void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.Fatal(e.Exception, "Unhandled exception (unobserved task)");
        }
    }
}
