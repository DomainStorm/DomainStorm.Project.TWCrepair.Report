using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA012.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA054.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
	/// <summary>
	/// 檢漏系統-年度計畫-系統成果報告書-六.最小流量比較表-最小流量比圖(結合在 RA054 裡的圖)
	/// </summary>
	public class DA012Service : IGetService<DA012, string>
	{
		private readonly IGetService<RA054, string> _ra054Service;
		public DA012Service(
			IGetService<RA054, string> ra054Service)
		{
			_ra054Service = ra054Service;

		}

		public Task<DA012> GetAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<DA012> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
		{
			return condition switch
			{
				QueryDA012 e => Query(e),
				_ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
			};
		}

		public class DA012Item
		{
			public DateTime Time { get; set; }
			public double? CH1Volumetric { get; set; }
		}

		public async Task<DA012> Query(QueryDA012 condition)
		{
			var result = new DA012();
			var ra054 = await _ra054Service.GetAsync<QueryRA054>(new QueryRA054
			{
				WorkSpaceId = condition.WorkSpaceId
			});


			foreach (var item in ra054.Items)
			{
				result.PlotlyJson.Data.First().X.Add(item.MeasureDate.ToString("yyyy/MM/dd"));
				var flow = ((int)(item.LowestFlow ?? 0M)).ToString();
				result.PlotlyJson.Data.First().Y.Add(flow);
				result.PlotlyJson.Data.First().Text.Add(flow);
			}
			return result;
		}

		public class SimpleWaterFlowCheckData
		{
			public DateTime Time { get; set; }
			public Double? CH1Volumetric { get; set; }
		}


		public Task<DA012[]> GetListAsync()
		{
			throw new NotImplementedException();
		}

		public Task<DA012[]> GetListAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<DA012[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
		{
			throw new NotImplementedException();
		}
	}
}
