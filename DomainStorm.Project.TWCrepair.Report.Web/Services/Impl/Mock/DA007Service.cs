using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA007.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
    /// <summary>
    /// 當日流量曲線圖
    /// </summary>
    public class DA007Service : IGetService<DA007, string>
    {
        public Task<DA007> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA007> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA007 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<DA007> Query(QueryDA007 condition)
        {

            var result = new DA007();
            var today = DateTime.Today;
            for(int i = 0; i< 1440; i++)
            {
                result.PlotlyJson.Data.First().X.Add(today.ToString("HH:mm"));
                result.PlotlyJson.Data.First().Y.Add( ((int)(i / 10)).ToString());
                today = today.AddMinutes(1);
            }
            return result;
        }

        public Task<DA007[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA007[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA007[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
