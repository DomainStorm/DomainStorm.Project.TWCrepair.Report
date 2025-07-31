using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA008.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
    /// <summary>
    /// 流量分析-(檢前總表/檢後總表)的流量曲線圖(結合在 RA041裡的圖)
    /// </summary>
    public class DA008Service : IGetService<DA008, string>
    {
        

        public Task<DA008> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA008> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA008 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class DA008Item
        {
            public DateTime Time { get; set; }
            public double? CH1Volumetric { get; set; }
        }

        public async Task<DA008> Query(QueryDA008 condition)
        {
            var result = new DA008();

            var today = DateTime.Today;
            for (int i = 0; i < 1440; i++)
            {
                result.PlotlyJson.Data.First().X.Add(today.ToString("HH:mm"));
                result.PlotlyJson.Data.First().Y.Add(((int)(i / 10)).ToString());
                today = today.AddMinutes(1);
            }
            return result;
        }

        public class SimpleWaterFlowCheckData
        {
            public DateTime Time { get; set; }
            public Double? CH1Volumetric { get; set; }
        }


        public Task<DA008[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA008[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA008[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
