using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA054.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.DepartmentWorkSpace.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-六.最小流量比較表
/// </summary>
public class RA054Service : IGetService<RA054, string>
{
    private readonly GetRepository<IRepository<Repository.Models.CheckSysAchievement>> _getCheckSysAchievementRepository;
	private readonly GetRepository<IRepository<Repository.Models.WaterFlowCheck>> _getFlowCheckRepository;
	private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZoneItem>> _getSetAllZoneItemRepository;
	private readonly IMapper _mapper;
	private readonly IGetService<DepartmentWorkSpace, Guid> _departmentWorkSpaceService;


	public RA054Service(
       GetRepository<IRepository<Repository.Models.CheckSysAchievement>> getCheckSysAchievementRepository,
	   GetRepository<IRepository<Repository.Models.WaterFlowCheck>> getFlowCheckRepository,
	   GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZoneItem>> getSetAllZoneItemRepository,
	   IMapper mapper,
	   IGetService<DepartmentWorkSpace, Guid> departmentWorkSpaceService
	   )
    {
        _getCheckSysAchievementRepository = getCheckSysAchievementRepository;
		_getFlowCheckRepository = getFlowCheckRepository;
		_getSetAllZoneItemRepository = getSetAllZoneItemRepository;
		_mapper = mapper;
		_departmentWorkSpaceService = departmentWorkSpaceService;

	}

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
		RA054 result = new RA054();

		


		var historyWorkSpaces = await _departmentWorkSpaceService.GetListAsync<QueryHistoryDepartmentWorkSpace>(new QueryHistoryDepartmentWorkSpace
		{
			WorkSpaceId = condition.WorkSpaceId
		});

		var historyWorkSapceIds = historyWorkSpaces.Select(x => x.Id).ToList();
		historyWorkSapceIds.Add(condition.WorkSpaceId);

		var achievements = (await _getCheckSysAchievementRepository().GetListAsync(x =>
				historyWorkSapceIds.Contains(x.WorkSpaceId)))
			.OrderByDescending(x => x.Year);

		foreach(var achievement in achievements)
		{
			//同一年度的先處後檢修後
			if(achievement.LowestFlowDateAfter.HasValue)
			{
				var item = new RA054_Item
				{
					MeasureDate = achievement.LowestFlowDateAfter.Value,
					LowestFlow = achievement.MinFlowAfter,
					DistributeAmount = achievement.DayDistributeAmountAfter,
					CustomerAmount = achievement.CustomerAmountAfter ?? 0,
					BeforeOrAfter = "檢修後"
				};
				result.Items.Add(item);
			}

			//再處理檢修前
			if (achievement.LowestFlowDateBefore.HasValue)
			{
				var item = new RA054_Item
				{
					MeasureDate = achievement.LowestFlowDateBefore.Value,
					LowestFlow = achievement.MinFlowBefore,
					DistributeAmount = achievement.DayDistributeAmountBefore,
					CustomerAmount = achievement.CustomerAmountBefore ?? 0,
					BeforeOrAfter = "檢修前"
				};
				result.Items.Add(item);
			}
		}
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
