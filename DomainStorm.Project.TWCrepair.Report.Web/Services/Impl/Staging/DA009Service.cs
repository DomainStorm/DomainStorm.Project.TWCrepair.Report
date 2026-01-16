using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA009.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA052.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
	/// <summary>
	/// 檢漏系統-年度計畫-系統成果報告書-四.最小流量率(修正值)比較表(結合在 RA052裡的圖)
	/// </summary>
	public class DA009Service : IGetService<DA009, string>
    {
		private readonly IGetService<RA052, string> _ra052Service;
		public DA009Service(
			IGetService<RA052, string> ra052Service)
        {
			_ra052Service = ra052Service;

		}

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
			var ra052 = await _ra052Service.GetAsync<QueryRA052>(new QueryRA052
			{
				WorkSpaceId = condition.WorkSpaceId
			});

			var before = Math.Round(ra052.MinFlowBeforeRate2 ?? 0, 0, MidpointRounding.AwayFromZero);
			var after = Math.Round(ra052.MinFlowAfterRate2 ?? 0, 0, MidpointRounding.AwayFromZero);

			result.PlotlyJson.Data.First().X.Add(after.ToString());
			result.PlotlyJson.Data.Last().X.Add(before.ToString());
			

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
