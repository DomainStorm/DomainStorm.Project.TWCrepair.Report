using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA006.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
    public class DA006Service : IGetService<DA006, string>
    {
        public Task<DA006> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA006> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA006 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<DA006> Query(QueryDA006 condition)
        {

            var result = new DA006();
            var today = DateTime.Today;
            for(int i = 0; i< 1440; i++)
            {
                result.PlotlyJson.Data.First().X.Add(today.ToString("HH:mm"));
                result.PlotlyJson.Data.First().Y.Add( ((int)(i / 10)).ToString());
                today = today.AddMinutes(1);
            }
            return result;
        }

        public Task<DA006[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA006[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA006[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
