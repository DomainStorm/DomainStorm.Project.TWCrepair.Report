using DomainStorm.Framework.Services;
using DomainStorm.Project.TWC.Report.Web.ViewModel;
using DomainStorm.Project.TWC.Report.Web.Views;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA002.V1;
using static DomainStorm.Project.TWC.Web.CommandModel.Department.V1;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock
{
    public class RA002Service : IGetService<RA002, string>
    {
        private readonly IGetService<Department, string> _departmentService;

        public RA002Service(
            IGetService<Department, string> departmentService
        )
        {
            _departmentService = departmentService;
        }

        public Task<RA002> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA002> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            if (condition is QueryRA002 queryRA002)
            {
                var report = new RA002
                {
                    Year = queryRA002.Year
                };

                return Task.FromResult(report);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
            }
            //return condition switch
            //{
            //    QueryRA002 e => Query(e),
            //    _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            //};
        }

        public async Task<RA002> Query(QueryRA002 condition)
        {
            var report = new RA002
            {
                Year= condition.Year
            };

            var sites = await _departmentService.GetAsync<QuerySite>(new QuerySite());

            foreach (var site in sites.Departments!)
            {
                report.Items.Add(new RA002_Item
                {
                    AnotherCode = site.AnotherCode,
                    Name = site.Name,
                    C1 = 0,
                    C2 = 0,
                    C3 = 0,
                    C4 = 0,
                    C5 = 0,
                    C6 = 0,
                    C7 = 0,
                    C8 = 0,
                    C9 = 0,
                    C10 = 0,
                    C11 = 0,
                    C12 = 0,
                    Total = 0,
                });
            }

            report.Items.Add(new RA002_Item
            {
                AnotherCode = "",
                Name = "總計",
                C1 = 0,
                C2 = 0,
                C3 = 0,
                C4 = 0,
                C5 = 0,
                C6 = 0,
                C7 = 0,
                C8 = 0,
                C9 = 0,
                C10 = 0,
                C11 = 0,
                C12 = 0,
                Total = 0,
            });

            return report;
        }

        public Task<RA002[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RA002[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA002[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
