namespace Wacton.Japangolin
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            // hook up unhandled exception handling
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            global::Wacton.Japangolin.Startup.Go();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
