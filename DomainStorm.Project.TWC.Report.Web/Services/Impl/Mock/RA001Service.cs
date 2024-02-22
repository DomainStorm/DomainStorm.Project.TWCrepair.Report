using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWC.Report.Web.Views;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA001.V1;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Department.V1;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock
{
    public class RA001Service : IGetService<RA001, string>
    {
        private readonly IGetService<Department, string> _departmentService;

        public RA001Service(
            IGetService<Department, string> departmentService
        )
        {
            _departmentService = departmentService;
        }

        public Task<RA001> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA001> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryRA001 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<RA001> Query(QueryRA001 condition)
        {
            var report = new RA001
            {
                ApplyDateBegin = condition.ApplyDateBegin,
                ApplyDateEnd = condition.ApplyDateEnd
            };

            var sites = await _departmentService.GetAsync<QuerySite>(new QuerySite());

            foreach (var site in sites.Departments!)
            {
                report.Items.Add(new RA001_Item
                {
                    AnotherCode = site.AnotherCode,
                    Name = site.Name,
                    Count = 0
                });
            }
            report.Items.Add(new RA001_Item
            {
                AnotherCode = "",
                Name = "總計",
                Count = report.Items.Sum(x => x.Count)
            });
            return report;
        }

        public Task<RA001[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RA001[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA001[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
