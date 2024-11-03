using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EFCoreSqlite
{
    public class HostedService: BackgroundService
    {
        private readonly ILogger<HostedService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public HostedService(ILogger<HostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            _logger.LogInformation("Creating database if it doesn't exist");
            await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreatedAsync(stoppingToken);
        }
    }
}
