namespace Wacton.Japangolin.Desktop;

using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Wacton.Desu.Japanese;
using Wacton.Japangolin.Core.Mains;
using Wacton.Japangolin.Core.Mutations;
using Wacton.Japangolin.Desktop.Mains;
using Wacton.Japangolin.Desktop.MVVM;
using Wacton.Japangolin.Desktop.Themes;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost host;

    public App()
    {
        InitialiseLogging();
        host = InitialiseApplication();
    }
        
    private static void InitialiseLogging()
    {
        Current.DispatcherUnhandledException += (_, args) => ProcessUnhandledException(args.Exception, "dispatcher");
        AppDomain.CurrentDomain.UnhandledException += (_, args) => ProcessUnhandledException((Exception) args.ExceptionObject, "app domain");
        TaskScheduler.UnobservedTaskException += (_, args) => ProcessUnhandledException(args.Exception, "unobserved task");
        
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext() // is this needed? https://github.com/serilog/serilog/wiki/Enrichment
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .WriteTo.Console()
            .WriteTo.File("log.txt", encoding: Encoding.UTF8)
            .CreateLogger();
    }
        
    private static IHost InitialiseApplication()
    {
        var japaneseEntries = JapaneseDictionary.ParseEntries().ToList();
            
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services => services
                .AddSingleton<Settings>()
                .AddSingleton<Main>(provider => new Main(japaneseEntries, provider.GetRequiredService<Settings>()))
                .AddSingleton<WordFilterMutation>()
                .AddSingleton<WordAndInflectionMutation>()
                .AddSingleton<ModelWatcher>()
                .AddSingleton<DetailViewModel>()
                .AddSingleton<NoDetailViewModel>()
                .AddSingleton<SettingsViewModel>()
                .AddSingleton<SnackbarViewModel>()
                .AddSingleton<MainViewModel>()
                .AddSingleton<ShellViewModel>())
            .UseSerilog()
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await host.StartAsync();
        SetTheme();
        ShowWindow();
        base.OnStartup(e);
    }

    private void SetTheme()
    {
        var vibrantColor = (Color)FindResource("VibrantStart"); // set in Gradients.xaml
        Stylist.SetVibrantTheme(vibrantColor);
    }

    private void ShowWindow()
    {
        var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        var shellViewModel = host.Services.GetRequiredService<ShellViewModel>();
        var shellView = new ShellView {DataContext = shellViewModel, Title = $"Wacton.Japangolin · {applicationVersion}"};
        shellView.Show();
    }

    private static readonly object UnhandledExceptionLock = new();
    private static bool hasDisplayedErrorMessage;
    private static void ProcessUnhandledException(Exception exception, string contextInfo)
    {
        lock (UnhandledExceptionLock)
        {
            var message = $"Unhandled exception via {contextInfo}: {exception.Message}";
            Log.Fatal(exception, message);

            if (hasDisplayedErrorMessage)
            {
                return;
            }
                
            MessageBox.Show(message, "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
            hasDisplayedErrorMessage = true;
            Current.Shutdown();
        }
    }
}