using DomainStorm.Framework;
using DomainStorm.Framework.SqlDb;
using Microsoft.EntityFrameworkCore;

namespace DomainStorm.Project.TWCrepair.Report.Web
{
    public class Worker : IHealthzHostedService
    {

        private readonly IServiceProvider _serviceProvider;


        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (Environment.GetEnvironmentVariable("DomainStorm_AutoMigrate") == "false")
                return;

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<GetSession>()();
            await Task.Run(() =>
            {
                var pendingMigrations = context.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    context.Database.Migrate();
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
