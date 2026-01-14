using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-二.檢修漏成果計算統計表
/// </summary>
public class RA050 : ReportDataModel
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
	/// 漏水復原率 (a.本期檢修前最小流量-b. 前期檢修後最小流量 )/ (i.兩次間隔年數 * e.兩期間隔總配水量 / d.兩期間隔天數) * 100;
	/// </summary>
	public decimal? LeackageRecover { get; set; }


	/// <summary>
	/// 漏水復原量 (a.本期檢修前最小流量- b. 前期檢修後最小流量)/ ((f.檢漏管長(KM) + h.每戶間隔數(KM) * g.檢修後用戶數) * i.兩次間隔年數)
	/// </summary>
	public decimal? LeackageRecoverAmount { get; set; }


	/// <summary>
	/// 實際檢漏效益(元)
	/// </summary>
	public int? RealBenefitAmount { get; set; }

	/// <summary>
	/// 夜間最小流量效益額(元) k*365*1*l-m
	/// </summary>
	public int? NightMinFlowBenefit { get; set; }


	/// <summary>
	/// 檢漏成本 (元/CMD)  m/j
	/// </summary>
	public decimal? CheckCostPerCmd { get; set; }


	/// <summary>
	/// 檢漏成本 (元/km)  m/n
	/// </summary>
	public decimal? CheckCostPerKm { get; set; }


	/// <summary>
	/// 作業進度( % )  p/o * 100 %
	/// </summary>
	public decimal? OperationProgress { get; set; }

	/// <summary>
	/// 最小流量率(檢修前)
	/// </summary>
	public decimal? MinFlowRateBefore { get; set; }

	/// <summary>
	/// 最小流量率(檢修後)
	/// </summary>
	public decimal? MinFlowRateAfter { get; set; }


	/// <summary>
	/// 容許漏水量檢修前 a/(f+h*g)*(2/s)^0.5
	/// </summary>
	public decimal? AllowLeakageWaterAmountBefore { get; set; }
	
	/// <summary>
	/// 容許漏水量檢修後 c/(f+h*g)*(2/t)^0.5
	/// </summary>
	public decimal? AllowLeakageWaterAmountAfter { get; set; }
	


	/// <summary>
	/// 檢漏速率  n.實際檢漏作業管長/ z.管線聽音人日
	/// </summary>
	public decimal? CheckSpeed { get; set; }


	/// <summary>
	/// 篩檢率   x.確認漏水件數/ x.確認漏水件數+ y.確認無漏件數
	/// </summary>
	public decimal? FiltRate { get; set; }


	/// <summary>
	/// 確認失敗率  (w.確認失敗件數(無漏及超限)/ (w.確認失敗件數(無漏及超限)+ v.漏水總件數)) * 100
	/// </summary>
	public decimal? ConfirmFailAmountRate { get; set; }


	/// <summary>
	/// 地下漏水發生率 u.地下漏水件數/ v.漏水總件數
	/// </summary>
	public decimal? UnderGroundLeakageAmountRate { get; set; }


	/// <summary>
	/// UARL不可避免之真正漏水量(liters/day) 檢修前  ((18 × f) + (0.8× ad) + (25 × ad/1000)) ×(Sx10)
	/// </summary>
	public decimal? UARLLeakageAmountBefore { get; set; }



	/// <summary>
	/// UARL不可避免之真正漏水量(liters/day) 檢修後  ((18 × f) + (0.8× ae) + (25 × ae/1000)) ×(Tx10)
	/// </summary>
	public decimal? UARLLeakageAmountAfter { get; set; }


	/// <summary>
	/// ILI設施漏水量指標 檢修前 (q-af-(q*2.15%))*1000/檢修前UARL
	/// </summary>
	public decimal ILILeakageIndexBefore {  get; set; }

	/// <summary>
	/// ILI設施漏水量指標 檢修後  (r-af-(r*2.15%))*1000/檢修後UARL
	/// </summary>
	public decimal ILILeakageIndexAfter { get; set; }

}

