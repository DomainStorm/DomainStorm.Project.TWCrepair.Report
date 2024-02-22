using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWC.Report.Web.ViewModel;
using DomainStorm.Project.TWC.Report.Web.Views.Dashboards;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.DA001.V1;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA002.V1;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock
{
    public class DA001Service : IGetService<DA001, string>
    {
        private readonly IGetService<Department, string> _departmentService;

        public DA001Service(
            IGetService<Department, string> departmentService
        )
        {
            _departmentService = departmentService;
        }

        public Task<DA001> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA001> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA001 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<DA001> Query(QueryDA001 condition)
        {
            return new DA001
            {
            };
        }

        public Task<DA001[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA001[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA001[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
