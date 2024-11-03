using EFCoreSqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(path: "serilog.json", optional: false, reloadOnChange: true)
    .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
Log.Logger.Information("App.OnStartup");
try
{
    var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices(x =>
        {
            x.AddHostedService<HostedService>();
            x.AddDbContext<AppDbContext>(x => x.UseSqlite(configuration.GetConnectionString("Default")));
        })
        .Build();
    host.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly!");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}