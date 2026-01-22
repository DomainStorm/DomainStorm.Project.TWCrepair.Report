using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA054.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-六.最小流量比較表
/// </summary>
public class RA054Service : IGetService<RA054, string>
{
    private readonly GetRepository<IRepository<Repository.Models.CheckSysAchievement>> _getCheckSysAchievementRepository;
	private readonly GetRepository<IRepository<Repository.Models.WaterFlowCheck>> _getFlowCheckRepository;
	private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZoneItem>> _getSetAllZoneItemRepository;
	private readonly IMapper _mapper;
    

   

    public Task<RA054> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA054> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA054 e => QueryRA054(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA054> QueryRA054(QueryRA054 condition)
    {
		RA054 result = new RA054
		{
			Items = new List<RA054_Item>
			{
				new RA054_Item
				{
					MeasureDate = DateTime.Parse("2022/9/23"),
					LowestFlow = 18915.33M,
					DistributeAmount = 24522.66M,
					CustomerAmount = 29975
				},
				new RA054_Item
				{
					MeasureDate = DateTime.Parse("2022/2/24"),
					LowestFlow = 23111.11M,
					DistributeAmount = 28367.73M,
					CustomerAmount = 29639
				}
			}
		};

		return result;
    }

    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA054[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA054[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA054[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
