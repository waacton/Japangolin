namespace Wacton.Japangolin.UI
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;
    using NLog;
    using Wacton.Japangolin.UI.Mains;
    using Wacton.Japangolin.UI.Themes;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
       public App()
        {
            LogManager.Setup().LoadConfiguration(builder => {
                builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
                builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile("log.txt");
            });
            
            Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
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
            var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            ShellViewModel.WindowTitle = $"Wacton.Japangolin · {applicationVersion}";
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
