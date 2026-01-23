using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA055.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-六A.各計量點水量比較表
/// </summary>
public class RA055Service : IGetService<RA055, string>
{
    private readonly GetRepository<IRepository<Repository.Models.CheckSysAchievement>> _getCheckSysAchievementRepository;
	private readonly GetRepository<IRepository<Repository.Models.WaterFlowCheck>> _getFlowCheckRepository;
	private readonly IMapper _mapper;
    

    public RA055Service(
       GetRepository<IRepository<Repository.Models.CheckSysAchievement>> getCheckSysAchievementRepository,
	   GetRepository<IRepository<Repository.Models.WaterFlowCheck>> getFlowCheckRepository,
	   IMapper mapper
       )
    {
        _getCheckSysAchievementRepository = getCheckSysAchievementRepository;
		_getFlowCheckRepository = getFlowCheckRepository;
		_mapper = mapper;   
    }

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
		RA055 result = new RA055();

		var checkAchivement = (await _getCheckSysAchievementRepository().GetListAsync(x => x.WorkSpaceId == condition.WorkSpaceId)).FirstOrDefault();
		if (checkAchivement == null )
		{
			return result;
		}

		result.LowestFlowDateBefore = checkAchivement.LowestFlowDateBefore;
		result.LowestFlowDateAfter = checkAchivement.LowestFlowDateAfter;


		//找流量檢查
		var FlowChecks = (await _getFlowCheckRepository().GetListAsync(x => x.WorkSpaceId == condition.WorkSpaceId
			  && ((x.MeasureDate == checkAchivement.LowestFlowDateBefore && x.BeforeOrAfter.Name == "檢修前")
				   || (x.MeasureDate == checkAchivement.LowestFlowDateAfter && x.BeforeOrAfter.Name == "檢修後"))))
			 .ToArray();
			 

		
		foreach (var flowCheck in FlowChecks.Where(x => x.BeforeOrAfter.Name =="檢修前"))
		{
			//只處理同一地點有檢修前及檢修後的資料
			var afterFlowCheck = FlowChecks.Where(x => x.BeforeOrAfter.Name == "檢修後" && x.LocationNumber == flowCheck.LocationNumber).FirstOrDefault();
			if (afterFlowCheck == null)
			{
				continue;
			}

			var item = new RA055_Item
			{
				Location = flowCheck.Location,
				MinFlowBefore = (decimal) Math.Round( flowCheck.LowestFlowCmd ?? 0 , 2, MidpointRounding.AwayFromZero),
				DayDistributeAmountBefore = (decimal)Math.Round((flowCheck.LastTotal ?? 0) - (flowCheck.FirstTotal?? 0), 2, MidpointRounding.AwayFromZero),
				MinFlowAfter = (decimal)Math.Round(afterFlowCheck.LowestFlowCmd ?? 0, 2, MidpointRounding.AwayFromZero),
				DayDistributeAmountAfter = (decimal)Math.Round((afterFlowCheck.LastTotal ?? 0) - (afterFlowCheck.FirstTotal ?? 0), 2, MidpointRounding.AwayFromZero),
			};

			result.Items.Add(item);
		}

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
