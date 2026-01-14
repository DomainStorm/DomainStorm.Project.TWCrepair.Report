using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-三.檢修漏成果計算資料表
/// </summary>
public class RA051 : ReportDataModel
{
	public string DepartmentName { get; set; }

	/// <summary>
	/// 年度(西元年)
	/// </summary>
	public int Year { get; set; }


	/// <summary>
	/// 工作區
	/// </summary>
	public string? WorkSpaceName { get; set; }


	/// <summary>
	/// a.本期檢修前最小流量
	/// </summary>
	public decimal? MinFlowBefore { get; set; }


	/// <summary>
	/// b.前期檢修後最小流量
	/// </summary>
	public decimal? LastMinFlowAfter { get; set; }

	/// <summary>
	///c.本期檢修後最小流量
	/// </summary>
	public decimal? MinFlowAfter { get; set; }

	/// <summary>
	/// 最小流量率(檢修後)的量測日期起
	/// </summary>
	public DateTime? MinFlowRateAfterBeginDate { get; set; }

	/// <summary>
	/// 最小流量率(檢修後)的量測日期訖
	/// </summary>
	public DateTime? MinFlowRateAfterEndDate { get; set; }


	/// <summary>
	/// 最小流量率(檢修後)的量測日期
	/// </summary>
	public DateTime? DateOfMinFlowRateAfter { get; set; }

	/// <summary>
	/// d.兩期間隔天數
	/// </summary>
	public int? IntervalDays { get; set; }

	/// <summary>
	/// e.兩期間隔總配水量
	/// </summary>
	public int? IntervalWaterAmount { get; set; }

	/// <summary>
	/// f.檢漏管長(km)
	/// </summary>
	public decimal? PlanPipeLength { get; set; }

	/// <summary>
	/// g.檢修後用戶數
	/// </summary>
	public int? CustomerAmountAfter { get; set; }

	/// <summary>
	/// h.每戶間隔數(KM)
	/// </summary>
	public decimal? DistanceBetweenHouses { get; set; } = 0.005M;

	/// <summary>
	/// i.兩次間隔年數
	/// </summary>
	public decimal? InervalYears { get; set; }

	/// <summary>
	/// j.(實除)檢修漏水量
	/// </summary>
	public decimal? RealLeakageWaterAmount { get; set; }

	/// <summary>
	/// k.最小流量差異數
	/// </summary>
	public decimal? MinFlowDifference { get; set; }

	/// <summary>
	///l. 生產成本(元/噸)
	/// </summary>
	public decimal? ProductionCostPerT { get; set; }



	/// <summary>
	/// m.成果作業費用
	/// </summary>
	public int? AchievementExpense { get; set; }


	/// <summary>
	/// n.實際檢漏作業管長(KM)
	/// </summary>
	public decimal? RealPipeLength { get; set; }


	/// <summary>
	/// o.計劃作業人日
	/// </summary>
	public decimal? PlanPersonDay { get; set; }

	/// <summary>
	/// p.實際作業人日
	/// </summary>
	public decimal? RealPersonDay { get; set; }

	/// <summary>
	/// q.檢修前日配水量
	/// </summary>
	public decimal? DayDistributeAmountBefore { get; set; }

	/// <summary>
	/// 檢修前日配水量的檢測日期起
	/// </summary>
	public DateTime? DayDistributeAmountBeforeBeginDate { get; set; }
	/// <summary>
	/// 檢修前日配水量的檢測日期訖
	/// </summary>
	public DateTime? DayDistributeAmountBeforeEndDate { get; set; }



	/// <summary>
	/// r.檢修後日配水量
	/// </summary>
	public decimal? DayDistributeAmountAfter { get; set; }

	/// <summary>
	/// 檢修後日配水量的檢測日期起
	/// </summary>

	public DateTime? DayDistributeAmountAfterBeginDate { get; set; }
	/// <summary>
	/// 檢修後日配水量的檢測日期訖
	/// </summary>
	public DateTime? DayDistributeAmountAfterEndDate { get; set; }



	/// <summary>
	/// s.檢修前平均水壓
	/// </summary>
	public decimal? AveragePressureBefore { get; set; }

	/// <summary>
	/// 檢修前平均水壓的日期起
	/// </summary>
	public DateTime? AveragePressureBeforeBeginDate { get; set; }
	/// <summary>
	/// 檢修前平均水壓的日期訖
	/// </summary>
	public DateTime? AveragePressureBeforeEndDate { get; set; }



	/// <summary>
	/// t.檢修後平均水壓
	/// </summary>
	public decimal? AveragePressureAfter { get; set; }



	/// <summary>
	/// 檢修後平均水壓的日期起
	/// </summary>
	public DateTime? AveragePressureAfterBeginDate { get; set; }
	/// <summary>
	/// 檢修後平均水壓的日期訖
	/// </summary>
	public DateTime? AveragePressureAfterEndDate { get; set; }


	/// <summary>
	/// u.實際地下漏水件數
	/// </summary>
	public int? RealUnderGroundLeakageAmount { get; set; }


	/// <summary>
	/// v.實際漏水總件數
	/// </summary>
	public int? RealLeakageAmount { get; set; }


	/// <summary>
	/// w.確認失敗件數(無漏及超限)
	/// </summary>
	public int? ConfirmFailAmount { get; set; }


	/// <summary>
	/// x.確認漏水件數
	/// </summary>
	public int? ConfirmLeakageAmount { get; set; }

	/// <summary>
	/// y.確認無漏件數
	/// </summary>

	public int? ConfirmNoLeakageAmount { get; set; }


	/// <summary>
	/// z.管線聽音人日
	/// </summary>
	public decimal? ListenDay { get; set; }


	/// <summary>
	/// aa. 計畫檢修漏水量
	/// </summary>
	public decimal? PlanLeakageWaterAmount { get; set; }

	/// <summary>
	/// ab.實際檢漏戶數
	/// </summary>
	public int? RealCustomerAmount { get; set; }

	/// <summary>
	/// ac.檢修前用戶數
	/// </summary>
	public int? CustomerAmountBefore { get; set; }

	/// <summary>
	/// ad. 檢修前用戶接水點數
	/// </summary>
	public int? CustomerWaterPointBefore { get; set; }

	/// <summary>
	/// ae. 檢修後用戶接水點數
	/// </summary>
	public int? CustomerWaterPointAfter { get; set; }


	/// <summary>
	/// af.最近一年日平均售水量
	/// </summary>
	public decimal? LastYearAverageDaySaleWater { get; set; }



	
}

