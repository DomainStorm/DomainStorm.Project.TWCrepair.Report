using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA055.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-六A.各計量點水量比較表
/// </summary>
public class RA055Service : IGetService<RA055, string>
{
    

    public Task<RA055> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA055> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA055 e => QueryRA055(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA055> QueryRA055(QueryRA055 condition)
    {
		RA055 result = new RA055
		{
			LowestFlowDateBefore = DateTime.Today,
			LowestFlowDateAfter = DateTime.Today.AddDays(1)
		};


		result.Items.Add(new RA055_Item
		{
			Location = "台中市東勢區貽福橋西側60HP",
			MinFlowBefore = 4289,
			DayDistributeAmountBefore = 5233,
			MinFlowAfter = 2965,
			DayDistributeAmountAfter = 4091
		});

		result.Items.Add(new RA055_Item
		{
			Location = "台中市東勢區成功橋東側70HP",
			MinFlowBefore = 5021,
			DayDistributeAmountBefore = 5864,
			MinFlowAfter = 4157,
			DayDistributeAmountAfter = 5266
		});

		result.SumItem = new RA055_Item
		{
			MinFlowBefore = result.Items.Sum(x => x.MinFlowBefore),
			DayDistributeAmountBefore = result.Items.Sum(x => x.DayDistributeAmountBefore),
			MinFlowAfter = result.Items.Sum(x => x.MinFlowAfter),
			DayDistributeAmountAfter = result.Items.Sum(x => x.DayDistributeAmountAfter),
		};

		return result;
    }

    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA055[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA055[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA055[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
