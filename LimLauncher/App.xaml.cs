using System.Reflection;
using System.Windows;
using LimLauncher.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LimLauncher;

/// <summary>
/// App.xaml 的交互逻辑
/// </summary>
public partial class App : Application
{
    Mutex mutex;
    private IConfiguration _configuration;
    public IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        this.Startup += new StartupEventHandler(App_Startup);
    }

    void App_Startup(object sender, StartupEventArgs e)
    {
        mutex = new Mutex(true, "ElectronicNeedleTherapySystem", out bool ret);
        if (!ret)
        {
            Environment.Exit(0);
        }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(path: "serilog.json", optional: false, reloadOnChange: true)
            .Build();
        _configuration = configuration;
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        Log.Logger.Information("App.OnStartup");
        using IHost host = Host.CreateDefaultBuilder(null)
            .ConfigureServices(services =>
            {
                services.AddTransient<MainWindow>();
                services.AddOptions();
                services.Configure<LauncherSettings>(_configuration.GetSection("LauncherSettings"));
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
                logging.AddSerilog();
            })
            .Build();
        UseContainer(host.Services);
    }

    private void UseContainer(IServiceProvider services)
    {
        var mainConsole = services.GetRequiredService<MainWindow>();
        mainConsole.ShowDialog();
    }

    
    protected override void OnExit(ExitEventArgs e)
    {
        Log.Logger.Information("App.OnExit");
        Log.CloseAndFlush();

        base.OnExit(e);
    }
}