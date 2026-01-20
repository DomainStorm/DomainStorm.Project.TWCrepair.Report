using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA054.V1;

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
    

    public RA054Service(
       GetRepository<IRepository<Repository.Models.CheckSysAchievement>> getCheckSysAchievementRepository,
	   GetRepository<IRepository<Repository.Models.WaterFlowCheck>> getFlowCheckRepository,
	   GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZoneItem>> getSetAllZoneItemRepository,
	   IMapper mapper
       )
    {
        _getCheckSysAchievementRepository = getCheckSysAchievementRepository;
		_getFlowCheckRepository = getFlowCheckRepository;
		_getSetAllZoneItemRepository = getSetAllZoneItemRepository;
		_mapper = mapper;   
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


		//找流量檢查
		var FlowChecks = (await _getFlowCheckRepository().GetListAsync(x => x.WorkSpaceId == condition.WorkSpaceId
			  && (x.LowestFlowAfterReport!.Value || x.LowestFlowBeforeReport!.Value)))
			 .ToArray()
			 .OrderByDescending(x => x.MeasureDate);

		var setAllZoneItemRepository = _getSetAllZoneItemRepository();
		foreach (var flowCheck in FlowChecks)
		{
			var item = new RA054_Item
			{
				MeasureDate = flowCheck.MeasureDate,
				LowestFlow = Math.Round((decimal)flowCheck.LowestFlow!,2 , MidpointRounding.AwayFromZero),
				DistributeAmount = Math.Round((decimal)((flowCheck.LastTotal ?? 0) - (flowCheck.FirstTotal ?? 0)), 2, MidpointRounding.AwayFromZero),
				BeforeOrAfter = (flowCheck.LowestFlowAfterReport ?? false) ? "檢修後" : "檢修前",
			};

			var setAllZoneItem = (await setAllZoneItemRepository.GetListAsync(
				x => x.Month == flowCheck.MeasureDate.Month
				&& x.DataType == Repository.Models.YearPlan.YearPlanSetAllZoneDataType.Customer
				&& x.YearPlanSetAllZone.Year == flowCheck.MeasureDate.Year
				&& x.YearPlanSetAllZone.WorkSpace.Id == condition.WorkSpaceId
				&& x.YearPlanSetAllZone.SiteId == flowCheck.SiteId
				&& x.YearPlanSetAllZone.WaterSupplySystemId == flowCheck.WaterSupplySystemId
				&& x.YearPlanSetAllZone.SmallRegionId == flowCheck.SmallRegionId
				)).FirstOrDefault();
			if(setAllZoneItem != null)
			{
				item.CustomerAmount = (int)setAllZoneItem.Amount;
			}

			result.Items.Add(item);
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
