using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA010.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA053.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
	/// <summary>
	/// 檢漏系統-年度計畫-系統成果報告書-五.作業前後水壓比較表-水壓比較圖(結合在 RA053裡的圖)
	/// </summary>
	public class DA010Service : IGetService<DA010, string>
	{
		private readonly IGetService<RA053, string> _ra053Service;
		public DA010Service(
			IGetService<RA053, string> ra053Service)
		{
			_ra053Service = ra053Service;

		}

		public Task<DA010> GetAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<DA010> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
		{
			return condition switch
			{
				QueryDA010 e => Query(e),
				_ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
			};
		}

		public class DA010Item
		{
			public DateTime Time { get; set; }
			public double? CH1Volumetric { get; set; }
		}

		public async Task<DA010> Query(QueryDA010 condition)
		{
			var result = new DA010();
			var ra053 = await _ra053Service.GetAsync<QueryRA053>(new QueryRA053
			{
				WorkSpaceId = condition.WorkSpaceId
			});


			if (ra053.AnalysisItems.Count == 3)
			{
				var before = result.PlotlyJson.Data.First();
				var after = result.PlotlyJson.Data.Last();

				before.Y = after.Y = new List<string> { "最高點水壓", "最高點平均水壓", "平均點平均水壓", "最低點平均水壓", "最低點水壓" };

				before.X = new List<string>
				{
					ra053.AnalysisItems[0].HighestBefore.ToString(),
					ra053.AnalysisItems[1].HighestBefore.ToString(),
					ra053.AnalysisItems[1].AverageBefore.ToString(),
					ra053.AnalysisItems[1].LowestBefore.ToString(),
					ra053.AnalysisItems[2].LowestBefore.ToString(),
				};

				after.X = new List<string>
				{
					ra053.AnalysisItems[0].HighestAfter.ToString(),
					ra053.AnalysisItems[1].HighestAfter.ToString(),
					ra053.AnalysisItems[1].AverageAfter.ToString(),
					ra053.AnalysisItems[1].LowestAfter.ToString(),
					ra053.AnalysisItems[2].LowestAfter.ToString(),
				};
			}
			return result;
		}

		public class SimpleWaterFlowCheckData
		{
			public DateTime Time { get; set; }
			public Double? CH1Volumetric { get; set; }
		}


		public Task<DA010[]> GetListAsync()
		{
			throw new NotImplementedException();
		}

		public Task<DA010[]> GetListAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<DA010[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
		{
			throw new NotImplementedException();
		}
	}
}
