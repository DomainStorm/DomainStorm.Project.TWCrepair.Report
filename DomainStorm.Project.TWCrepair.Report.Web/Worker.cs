using DomainStorm.Framework;
using DomainStorm.Framework.SqlDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DomainStorm.Project.TWCrepair.Report.Web
{
    public class Worker : IHealthzHostedService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<SqlDbOptions> _sqlOptions;


        public Worker(IServiceProvider serviceProvider, IOptions<SqlDbOptions> sqlOptions)
        {
            _serviceProvider = serviceProvider;
            _sqlOptions = sqlOptions;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_sqlOptions.Value.AutoMigrate)
            {
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
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
