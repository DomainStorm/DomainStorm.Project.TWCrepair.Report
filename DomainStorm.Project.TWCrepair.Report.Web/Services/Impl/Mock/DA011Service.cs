using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA011.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA053.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
	/// <summary>
	/// 檢漏系統-年度計畫-系統成果報告書-五.作業前後水壓比較表-總水頭分布圖(結合在 RA053裡的圖)
	/// </summary>
	public class DA011Service : IGetService<DA011, string>
	{
		private readonly IGetService<RA053, string> _ra053Service;
		public DA011Service(
			IGetService<RA053, string> ra053Service)
		{
			_ra053Service = ra053Service;

		}

		public Task<DA011> GetAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<DA011> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
		{
			return condition switch
			{
				QueryDA011 e => Query(e),
				_ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
			};
		}

		public class DA011Item
		{
			public DateTime Time { get; set; }
			public double? CH1Volumetric { get; set; }
		}

		public async Task<DA011> Query(QueryDA011 condition)
		{
			var result = new DA011();
			


			var before = result.PlotlyJson.Data.First();
			var after = result.PlotlyJson.Data.Last();

			before.X.Add("進1");
			before.Y.Add("20");
			after.X.Add("進1");
			after.Y.Add("25");

			before.X.Add("G5");
			before.Y.Add("18");
			after.X.Add("G5");
			after.Y.Add("23");

			before.X.Add("G4");
			before.Y.Add("16");
			after.X.Add("G4");
			after.Y.Add("19");


			return result;
		}

		public class SimpleWaterFlowCheckData
		{
			public DateTime Time { get; set; }
			public Double? CH1Volumetric { get; set; }
		}


		public Task<DA011[]> GetListAsync()
		{
			throw new NotImplementedException();
		}

		public Task<DA011[]> GetListAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<DA011[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
		{
			throw new NotImplementedException();
		}
	}
}
