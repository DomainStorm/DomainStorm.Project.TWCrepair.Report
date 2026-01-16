using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA009.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
	/// <summary>
	/// 檢漏系統-年度計畫-系統成果報告書-四.最小流量率(修正值)比較表(結合在 RA052裡的圖)
	/// </summary>
	public class DA009Service : IGetService<DA009, string>
    {
		

        public Task<DA009> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA009> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA009 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class DA009Item
        {
            public DateTime Time { get; set; }
            public double? CH1Volumetric { get; set; }
        }

        public async Task<DA009> Query(QueryDA009 condition)
        {
            var result = new DA009();
			

			result.PlotlyJson.Data.First().X.Add("64");
			result.PlotlyJson.Data.Last().X.Add("69");
			return result;
        }

        public class SimpleWaterFlowCheckData
        {
            public DateTime Time { get; set; }
            public Double? CH1Volumetric { get; set; }
        }


        public Task<DA009[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA009[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA009[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
