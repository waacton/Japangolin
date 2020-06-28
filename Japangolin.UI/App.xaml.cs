namespace Wacton.Japangolin.UI
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;

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
            var vibrantColor = (Color)this.FindResource("VibrantStart"); // set in Gradients.xaml
            Stylist.SetVibrantTheme(vibrantColor);
            base.OnStartup(e);
        }

        private static void SetWindowTitle()
        {
            var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;
            //ShellViewModel.WindowTitle = $"Wacton.Japangolin ({applicationVersion})";
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
