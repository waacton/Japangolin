namespace Wacton.Japangolin
{
    using System.Reflection;

    using Wacton.Japangolin.Domain;
    using Wacton.Japangolin.UI;
    using Wacton.Tovarisch.MVVM;

    public static class Startup
    {
        public static void Go()
        {
            // get the version number to display on the main window title
            var assembly = Assembly.GetExecutingAssembly();
            var version = AssemblyName.GetAssemblyName(assembly.Location).Version.ToString();

            // create the view to display to the user
            // the data context is the view model tree that contains the model
            var domainBootstrapper = new DomainBootstrapper();
            //var domainModel = domainBootstrapper.BuildDomainModel();

            // TODO: move view model creation to view model bootstrapper
            //var viewModelBootstrapper = new ViewModelBootstrapper();
            var viewModel = new JapangolinViewModel(new CommandInvoker(new ModelChangeNotifier()));
            //viewModel.Refresh();

            // TODO: this is what currently creates and holds the model, needs separating - move to domain bootstrapper
            var mainView = new MainWindow { DataContext = viewModel };
            //mainView.Title += string.Format(" ({0})", version);

            //// display the window to the user!
            mainView.Show();
        }
    }
}
