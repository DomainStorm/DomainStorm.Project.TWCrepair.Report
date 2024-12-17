using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA004.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
    public class DA004Service : IGetService<DA004, string>
    {
        public Task<DA004> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA004> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA004 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        

        public Task<DA004> Query(QueryDA004 condition)
        {
            var result = new DA004();
            
            var before = result.PlotlyJson.Data.Last();
            before.X.Add("5.46");
            before.X.Add("1.85");
            before.X.Add("1.49");
            before.X.Add("1.07");
            before.X.Add("0.2");
            before.Text = before.X;


            var after = result.PlotlyJson.Data.First();
            after.X.Add("4.55");
            after.X.Add("1.7");
            after.X.Add("1.43");
            after.X.Add("1.13");
            after.X.Add("0.55");
            after.Text = after.X;

            return Task.FromResult(result);
        }

        

        public Task<DA004[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA004[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA004[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
