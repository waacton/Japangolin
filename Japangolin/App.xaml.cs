namespace Wacton.Japangolin
{
    using System;
    using System.Reflection;
    using System.Windows;

    using Wacton.Japangolin.UI.Mains;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // hook up unhandled exception handling
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            SetWindowTitle();
        }

        private static void SetWindowTitle()
        {
            var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;
            MainViewModel.Title = $"Wacton.Japangolin ({applicationVersion})";
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
