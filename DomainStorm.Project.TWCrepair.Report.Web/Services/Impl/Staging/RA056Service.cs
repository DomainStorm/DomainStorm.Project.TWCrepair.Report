using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA056.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-七.系統暨成本費用工作報表
/// </summary>
public class RA056Service : IGetService<RA056, string>
{
    private readonly GetRepository<IRepository<Repository.Models.CheckSysAchievement>> _getCheckSysAchievementRepository;
	private readonly GetRepository<IRepository<Repository.Models.CheckDailyReportDetail>> _getkDailyReportDetailRepository;
	private readonly GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> _getkWorkSpaceRepository;
	private readonly GetRepository<IRepository<Repository.Models.HR.HRSalary>> _getSalaryRepository;
	private readonly GetRepository<IRepository<Repository.Models.CommonCost>> _getCommonCostRepository;


	public RA056Service(
       GetRepository<IRepository<Repository.Models.CheckSysAchievement>> getCheckSysAchievementRepository,
	   GetRepository<IRepository<Repository.Models.CheckDailyReportDetail>> getkDailyReportDetailRepository,
	   GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> getkWorkSpaceRepository,
	   GetRepository<IRepository<Repository.Models.HR.HRSalary>> getSalaryRepository,
	   GetRepository<IRepository<Repository.Models.CommonCost>> getCommonCostRepository
	   )
    {
        _getCheckSysAchievementRepository = getCheckSysAchievementRepository;
		_getkDailyReportDetailRepository = getkDailyReportDetailRepository;
		_getkWorkSpaceRepository = getkWorkSpaceRepository;
		_getSalaryRepository = getSalaryRepository;
		_getCommonCostRepository = getCommonCostRepository;
	}

    public Task<RA056> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA056> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA056 e => QueryRA056(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA056> QueryRA056(QueryRA056 condition)
    {
		RA056 result = new RA056();

		var checkAchivement = (await _getCheckSysAchievementRepository().GetListAsync(x => x.WorkSpaceId == condition.WorkSpaceId)).FirstOrDefault();
		if (checkAchivement == null )
		{
			return result;
		}

		result.DepartmentName = checkAchivement.DepartmentName;
		result.OperationStartDate = checkAchivement.OperationStartDate!.Value;
		result.OperationEndDate = checkAchivement.OperationEndDate!.Value;

		var workSpace = await _getkWorkSpaceRepository().GetAsync(condition.WorkSpaceId);
		foreach(var item in workSpace.DepartmentWorkSpaceItems)
		{
			//不區分小區
			if(result.SystemItems.Any(x => x.WaterSupplySystemId == item.WaterSupplySystemId ))
			{
				continue;
			}
			result.SystemItems.Add(new RA056_SystemItem
			{
				WaterSupplySystemId = item.WaterSupplySystemId,
				WaterSupplySystemName = item.WaterSupplySystemName
			});
		}

		//取得工作日統計
		var dailyReportDetails = (await _getkDailyReportDetailRepository().GetListAsync(x =>
				x.WorkSpaceId == checkAchivement.WorkSpaceId
				&& x.CheckDailyReport.ReportDate >= checkAchivement.OperationStartDate
				&& x.CheckDailyReport.ReportDate <= checkAchivement.OperationEndDate,
				x => new
				{
					x.CheckDailyReport.ReportTeamMemberUserId,
					x.CheckDailyReport.ReportDate,
					x.WaterSupplySystemId,
					x.WaterSupplySystemName,
					x.WorkDayOfCheck,
					x.WorkDayOfConfirm,
					x.WorkDayOfWaterPressure,
					x.WorkDayOfMeasurement,
					x.WorkDayOfOtherScene,
					x.WorkDayOfDataPrepare,
					x.WorkDayOfDrawingPrepare,
					x.WorkDayOfOthers,
					x.ConfirmOfSystemLeakage,
					x.ConfirmOfSystemNoLeakage,
					x.CheckOfDistributionPipe,
					x.CheckOfSupplyPipe,
					x.CheckOfStopcock,
					x.CheckOfGateValve,
					x.CheckOfHydrant,
					x.CheckOfListenDay,
					x.ChargeOfTravel,
					x.ChargeOfFuel,
					x.ChargeOfOverTime,
					x.WorkDayTotal     
				}
			)).ToList();


		//區處的總工時 (區處以整個年度統計)
		var departmentWorkDays = await _getkDailyReportDetailRepository().GetSumAsync(x => x.DepartmentId == checkAchivement.DepartmentId
		&& x.CheckDailyReport.ReportDate.Year == checkAchivement.Year,
			 x => (x.WorkDayOfCheck ?? 0M)  //不能直接用這個欄位( [notMapped] WorkDayTotal)
					+ (x.WorkDayOfConfirm ?? 0M)
					+ (x.WorkDayOfWaterPressure ?? 0M)
					+ (x.WorkDayOfMeasurement ?? 0M)
					+ (x.WorkDayOfOtherScene ?? 0M)
					+ (x.WorkDayOfDataPrepare ?? 0M)
					+ (x.WorkDayOfDrawingPrepare ?? 0M)
					+ (x.WorkDayOfOthers ?? 0M));




		//每人每月工時
		var userMonthSalaries = dailyReportDetails.GroupBy(x => new
		{
			x.ReportTeamMemberUserId,
			x.WaterSupplySystemId,
			x.ReportDate.Month
		})
			.Select(g => new UserMonthSalary  //不要用匿名型別, 後續要算薪資
			{
				ReportTeamMemberUserId = g.Key.ReportTeamMemberUserId,
				WaterSupplySystemId = g.Key.WaterSupplySystemId!.Value,
				Month = g.Key.Month,
				WorkDayTotal = g.Sum(values => values.WorkDayTotal)
			}).ToList();

		var salaryRepository = _getSalaryRepository();
		foreach (var userMonthSalary in userMonthSalaries)
		{
			var salaryDate = new DateTime(checkAchivement.Year, userMonthSalary.Month, 1);
			//找該user 的月薪
			var salary = (await salaryRepository.GetListAsync(x => x.UserId == userMonthSalary.ReportTeamMemberUserId
				&& x.DepartmentId == checkAchivement.DepartmentId //怕有兼職
				&& x.SalaryDate == salaryDate)).FirstOrDefault();
			if (salary != null && salary.Salary.HasValue)
			{
				//日薪
				var daySalary = ((decimal)salary.Salary.Value) / DateTime.DaysInMonth(salaryDate.Year, salaryDate.Month);
				//該人該月薪資
				userMonthSalary.Salary = (int)(daySalary * userMonthSalary.WorkDayTotal);
			}
		}


		//共同費用
		var occurDate = new DateTime(checkAchivement.Year, 1, 1);
		var commonCosts = await _getCommonCostRepository().GetListAsync(x => x.OccurDate == occurDate && x.DepartmentId == checkAchivement.DepartmentId,
			x => new
			{
				x.CostItem!.Name,
				x.Cost
			});




		//依月份統計
		for (int m = 1; m<= 12; m++)
		{
			var tempDate = DateTime.Parse($"{checkAchivement.Year}/{m}/01");
			if(tempDate < checkAchivement.OperationStartDate || tempDate > checkAchivement.OperationEndDate)
			{
				continue;
			}

			var item = result.MonthItems[m - 1];
			item.HasData = true;

			//工作日
			item.WorkDays[0] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.WorkDayOfCheck ?? 0);
			item.WorkDays[1] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.WorkDayOfConfirm ?? 0);
			item.WorkDays[2] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.WorkDayOfWaterPressure ?? 0);
			item.WorkDays[3] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.WorkDayOfMeasurement ?? 0);
			item.WorkDays[4] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.WorkDayOfOtherScene ?? 0);
			item.WorkDays[5] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.WorkDayOfDataPrepare ?? 0);
			item.WorkDays[6] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.WorkDayOfDrawingPrepare ?? 0);
			item.WorkDays[7] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.WorkDayOfCheck ?? 0);

			//檢漏作業
			item.Checks[0] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => (int)(x.CheckOfDistributionPipe ?? 0));
			item.Checks[1] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => (int)(x.CheckOfSupplyPipe ?? 0));
			item.Checks[2] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => (int)(x.CheckOfStopcock ?? 0));
			item.Checks[3] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => (int)(x.CheckOfGateValve ?? 0));
			item.Checks[4] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => (int)(x.CheckOfHydrant ?? 0));
			item.Checks[5] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => (int)(x.CheckOfListenDay ?? 0));

			//系統確認
			item.SysteConfirms[0] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.ConfirmOfSystemLeakage ?? 0);
			item.SysteConfirms[1] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.ConfirmOfSystemNoLeakage ?? 0);

			//管線隊費用
			var monthWorkDaysRatio = departmentWorkDays > 0 ?
				item.WorkDaysSum / departmentWorkDays :
				0;
			item.Costs[0] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.ChargeOfTravel ?? 0);
			item.Costs[1] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => x.ChargeOfFuel ?? 0);
			item.Costs[2] = userMonthSalaries.Where(x => x.Month == m).Sum(x => x.Salary);
			item.Costs[3] = (int)Math.Round((monthWorkDaysRatio * commonCosts.Where(x => x.Name == "其他事務費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);
			item.Costs[4] = (int)Math.Round((monthWorkDaysRatio * commonCosts.Where(x => x.Name == "車輛維護費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);
			item.Costs[5] = (int)Math.Round((monthWorkDaysRatio * commonCosts.Where(x => x.Name == "器具修理費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);
			item.Costs[6] = (int)Math.Round((monthWorkDaysRatio * commonCosts.Where(x => x.Name == "儀器折舊費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);
			item.Costs[7] = dailyReportDetails.Where(x => x.ReportDate.Month == m).Sum(x => (x.ChargeOfOverTime ?? 0));
			item.Costs[8] = (int)Math.Round((monthWorkDaysRatio * commonCosts.Where(x => x.Name == "檢漏工具費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);


		}


		//依系統統計
		foreach(var item in result.SystemItems)
		{
			//工作日
			item.WorkDays[0] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.WorkDayOfCheck ?? 0);
			item.WorkDays[1] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.WorkDayOfConfirm ?? 0);
			item.WorkDays[2] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.WorkDayOfWaterPressure ?? 0);
			item.WorkDays[3] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.WorkDayOfMeasurement ?? 0);
			item.WorkDays[4] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.WorkDayOfOtherScene ?? 0);
			item.WorkDays[5] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.WorkDayOfDataPrepare ?? 0);
			item.WorkDays[6] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.WorkDayOfDrawingPrepare ?? 0);
			item.WorkDays[7] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.WorkDayOfCheck ?? 0);

			//檢漏作業
			item.Checks[0] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => (int)(x.CheckOfDistributionPipe ?? 0));
			item.Checks[1] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => (int)(x.CheckOfSupplyPipe ?? 0));
			item.Checks[2] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => (int)(x.CheckOfStopcock ?? 0));
			item.Checks[3] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => (int)(x.CheckOfGateValve ?? 0));
			item.Checks[4] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => (int)(x.CheckOfHydrant ?? 0));
			item.Checks[5] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => (int)(x.CheckOfListenDay ?? 0));


			//系統確認
			item.SysteConfirms[0] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.ConfirmOfSystemLeakage ?? 0);
			item.SysteConfirms[1] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.ConfirmOfSystemNoLeakage ?? 0);

			//管線隊費用
			var systemWorkDaysRatio = departmentWorkDays > 0 ?
				item.WorkDaysSum / departmentWorkDays :
				0;
			item.Costs[0] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.ChargeOfTravel ?? 0);
			item.Costs[1] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.ChargeOfFuel ?? 0);
			item.Costs[2] = userMonthSalaries.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => x.Salary);
			item.Costs[3] = (int)Math.Round((systemWorkDaysRatio * commonCosts.Where(x => x.Name == "其他事務費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);
			item.Costs[4] = (int)Math.Round((systemWorkDaysRatio * commonCosts.Where(x => x.Name == "車輛維護費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);
			item.Costs[5] = (int)Math.Round((systemWorkDaysRatio * commonCosts.Where(x => x.Name == "器具修理費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);
			item.Costs[6] = (int)Math.Round((systemWorkDaysRatio * commonCosts.Where(x => x.Name == "儀器折舊費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);
			item.Costs[7] = dailyReportDetails.Where(x => x.WaterSupplySystemId == item.WaterSupplySystemId).Sum(x => (x.ChargeOfOverTime ?? 0));
			item.Costs[8] = (int)Math.Round((systemWorkDaysRatio * commonCosts.Where(x => x.Name == "檢漏工具費").Sum(x => x.Cost)), 0, MidpointRounding.AwayFromZero);
		}

		result.AppendSumItem();
		return result;
    }


	public class UserMonthSalary
	{
		public Guid ReportTeamMemberUserId { get; set; }
		public Guid WaterSupplySystemId { get; set; }
		public int Month { get; set; }
		public decimal WorkDayTotal { get; set; }
		public int Salary { get; set; } = 0;

	}
    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA056[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA056[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA056[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
