using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA053.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-五.作業前後水壓比較表
/// </summary>
public class RA053Service : IGetService<RA053, string>
{
    private readonly GetRepository<IRepository<Repository.Models.CheckSysAchievement>> _getCheckSysAchievementRepository;
	private readonly GetRepository<IRepository<Repository.Models.WaterPressureCheck>> _getPressureCheckRepository;
	private readonly IMapper _mapper;
    

    public RA053Service(
       GetRepository<IRepository<Repository.Models.CheckSysAchievement>> getCheckSysAchievementRepository,
	   GetRepository<IRepository<Repository.Models.WaterPressureCheck>> getPressureCheckRepository,
	   IMapper mapper
       )
    {
        _getCheckSysAchievementRepository = getCheckSysAchievementRepository;
		_getPressureCheckRepository = getPressureCheckRepository;
		_mapper = mapper;   
    }

    public Task<RA053> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA053> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA053 e => QueryRA053(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA053> QueryRA053(QueryRA053 condition)
    {
		RA053 result = new RA053();
			
        var checkAchivement = (await _getCheckSysAchievementRepository().GetListAsync(x => x.WorkSpaceId == condition.WorkSpaceId)).FirstOrDefault();
		if(checkAchivement == null)
		{
			return result;
		}
		result.WorkSpaceName = checkAchivement.WorkSpace.WorkSpaceName;

		//找壓力檢查
		var pressureChecks = (await _getPressureCheckRepository().GetListAsync(x => x.WorkSpaceId == checkAchivement!.WorkSpaceId
			  && x.MeasureDate >= checkAchivement.OperationStartDate
			  && x.MeasureDate <= checkAchivement.OperationEndDate
			  && (x.HighestPressureBeforeReport!.Value || x.HighestPressureAfterReport!.Value))).ToArray();


		var beforeChecks = pressureChecks.Where(x => x.HighestPressureBeforeReport ?? false);
		var beforeDates = beforeChecks.Select(x => x.MeasureDate).Distinct();
		if(!beforeDates.Any())
		{
			throw new Exception("作業期間內查無檢修成果報告前最大水壓日");
		}
		else if(beforeDates.Count() > 1)
		{
			throw new Exception($"作業期間內查到多個檢修成果報告前最大水壓日:{ string.Join(',' , beforeDates.Select(x => x.ToString("yyyy/MM/dd")))}");
		}
		result.BeforeDate = beforeDates.First();

		var afterChecks = pressureChecks.Where(x => x.HighestPressureAfterReport ?? false);
		var afterDates = beforeChecks.Select(x => x.MeasureDate).Distinct();
		if (!afterDates.Any())
		{
			throw new Exception("作業期間內查無檢修成果報告後最大水壓日");
		}
		else if (afterDates.Count() > 1)
		{
			throw new Exception($"作業期間內查到多個檢修成果報告後最大水壓日:{string.Join(',', afterDates.Select(x => x.ToString("yyyy/MM/dd")))}");
		}
		result.AfterDate = afterDates.First();


		//各地點的水壓作匯整
		foreach(var beforCheck in beforeChecks)
		{
			var item = new RA053_Item
			{
				Location = beforCheck.Location,
				LocationNumber = beforCheck.LocationNumber,
				HighestBefore = (decimal)Math.Round(beforCheck.HighestPressure?? 0, 2, MidpointRounding.AwayFromZero),
				AverageBefore = (decimal)Math.Round(beforCheck.AveragePressure?? 0, 2, MidpointRounding.AwayFromZero),
				LowestBefore = (decimal)Math.Round(beforCheck.LowestPressure?? 0, 2, MidpointRounding.AwayFromZero),
				TotalWaterBefore = beforCheck.TotalWater ?? 0
			};
			result.Items.Add(item);
		}

		foreach(var afterCheck in afterChecks)
		{
			var item = result.Items.FirstOrDefault(x => x.LocationNumber == afterCheck.LocationNumber);
			if(item == null)
			{
				item = new RA053_Item
				{
					Location = afterCheck.Location,
					LocationNumber = afterCheck.LocationNumber,
				};
				result.Items.Add(item);
			}

			item.HighestAfter = (decimal)Math.Round(afterCheck.HighestPressure ?? 0, 2, MidpointRounding.AwayFromZero);
			item.AverageAfter = (decimal)Math.Round(afterCheck.AveragePressure ?? 0, 2, MidpointRounding.AwayFromZero);
			item.LowestAfter = (decimal)Math.Round(afterCheck.LowestPressure ?? 0, 2, MidpointRounding.AwayFromZero);
			item.TotalWaterBefore = afterCheck.TotalWater ?? 0;
		}


		//分析項目
		var analysisItem1 = new RA053_Item
		{
			Location = "最高點水壓",
			HighestBefore = result.Items.Max(x => x.HighestBefore),
			AverageBefore = result.Items.Max(x => x.AverageBefore),
			LowestBefore = result.Items.Max(x => x.LowestBefore),
			HighestAfter = result.Items.Max(x => x.HighestAfter),
			AverageAfter = result.Items.Max(x => x.AverageAfter),
			LowestAfter = result.Items.Max(x => x.LowestAfter),
		};
		result.AnalysisItems.Add(analysisItem1);

		var analysisItem2 = new RA053_Item
		{
			Location = "平均水壓",
			HighestBefore = result.Items.Average(x => x.HighestBefore),
			AverageBefore = result.Items.Average(x => x.AverageBefore),
			LowestBefore = result.Items.Average(x => x.LowestBefore),
			HighestAfter = result.Items.Average(x => x.HighestAfter),
			AverageAfter = result.Items.Average(x => x.AverageAfter),
			LowestAfter = result.Items.Average(x => x.LowestAfter),
		};
		result.AnalysisItems.Add(analysisItem2);

		var analysisItem3 = new RA053_Item
		{
			Location = "最低點水壓",
			HighestBefore = result.Items.Min(x => x.HighestBefore),
			AverageBefore = result.Items.Min(x => x.AverageBefore),
			LowestBefore = result.Items.Min(x => x.LowestBefore),
			HighestAfter = result.Items.Min(x => x.HighestAfter),
			AverageAfter = result.Items.Min(x => x.AverageAfter),
			LowestAfter = result.Items.Min(x => x.LowestAfter),
		};
		result.AnalysisItems.Add(analysisItem3);







		return result;
    }

    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA053[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA053[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA053[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
