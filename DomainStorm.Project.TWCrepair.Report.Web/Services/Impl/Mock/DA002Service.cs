using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA002.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
    public class DA002Service : IGetService<DA002, string>
    {
        public Task<DA002> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA002> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA002 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public Task<DA002> Query(QueryDA002 condition)
        {

            var fixForms = new List<DA002_Item>
            {
                new DA002_Item
                {
                    CheckCaseNo = "11300001",
                    FixCaseNo = "11300001",
                    ModifyTime = DateTime.Now,
                    ModifyNotes = "更換水閥完成"
                },
                new DA002_Item
                {
                    CheckCaseNo = "11300002",
                    FixCaseNo = "11300006",
                    ModifyTime = DateTime.Now,
                    ModifyNotes = "漏水位置開挖完成"
                },
            };

            var result = new DA002();
            result.PlotlyJson.Data.First().Cells.Values.Add(fixForms.Select(x => x.CheckCaseNo ?? "").ToList());
            result.PlotlyJson.Data.First().Cells.Values.Add(fixForms.Select(x => x.FixCaseNo ?? "").ToList());
            result.PlotlyJson.Data.First().Cells.Values.Add(fixForms.Select(x => x.ModifyTime.ToString("yyyy/MM/dd")).ToList());
            result.PlotlyJson.Data.First().Cells.Values.Add(fixForms.Select(x => x.ModifyNotes ?? "").ToList());
            return Task.FromResult(result);
        }

        

        public Task<DA002[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA002[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA002[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
