using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWC.Report.Web.Views;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA999.V1;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock
{
    public class RA999Service : IGetService<RA999, string>
    {
        private readonly IGetService<Department, string> _departmentService;

        public RA999Service(
            IGetService<Department, string> departmentService
        )
        {
            _departmentService = departmentService;
        }

        public Task<RA999> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA999> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryRA999 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<RA999> Query(QueryRA999 condition)
        {
            throw new NotImplementedException();
        }

        public Task<RA999[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RA999[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA999[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
