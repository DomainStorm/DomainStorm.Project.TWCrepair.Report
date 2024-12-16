using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA003.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Framework.Caching;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
    public class DA003Service : IGetService<DA003, string>
    {
        

        public Task<DA003> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA003> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA003 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<DA003> Query(QueryDA003 condition)
        {

            var result = new DA003();
            var today = DateTime.Today;
            for(int i = 0; i< 1440; i++)
            {
                result.PlotlyJson.Data.First().X.Add(today.ToString("HH:mm"));
                result.PlotlyJson.Data.First().Y.Add( ((int)(i / 10)).ToString());
                today = today.AddMinutes(1);
            }
            return result;
        }

        public Task<DA003[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA003[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA003[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
