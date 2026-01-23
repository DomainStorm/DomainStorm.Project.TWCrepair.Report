using System.ComponentModel.DataAnnotations.Schema;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-六A.各計量點水量比較表
/// </summary>
public class RA055 : ReportDataModel
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


	public DateTime? LowestFlowDateBefore { get; set; }

	public DateTime? LowestFlowDateAfter { get; set; }

	public RA055_Item SumItem { get; set; }

	public List<RA055_Item> Items { get; set; } = new List<RA055_Item>();

	

}

public class RA055_Item
{
	/// <summary>
	/// 地點
	/// </summary>
	public string Location { get; set; }

	/// <summary>
	/// 檢修前最小流量
	/// </summary>
	public decimal MinFlowBefore { get; set; }

	/// <summary>
	/// 檢修前日配水量
	/// </summary>
	public decimal DayDistributeAmountBefore { get; set; }

	/// <summary>
	/// 檢修前最小率
	/// </summary>
	public decimal MinRateBefore
	{
		get
		{
			if ( DayDistributeAmountBefore > 0)
			{
				//不要 * 100 , style 有套白分比的公式
				return Math.Round( MinFlowBefore / DayDistributeAmountBefore, 4, MidpointRounding.AwayFromZero);
			}
			else
				return 0M;
			
		}
	}



	/// <summary>
	/// 檢修後小流量
	/// </summary>
	public decimal MinFlowAfter { get; set; }

    /// <summary>
	/// 檢修後日配水量
	/// </summary>
	public decimal DayDistributeAmountAfter { get; set; }

	/// <summary>
	/// 檢修後最小率
	/// </summary>
	public decimal MinRateAfter
	{
		get
		{
			if (DayDistributeAmountAfter >0)
			{
				//不要 * 100 , style 有套白分比的公式
				return Math.Round( MinFlowAfter / DayDistributeAmountAfter, 4, MidpointRounding.AwayFromZero);
			}
			else
				return 0M;

		}
	}

	public decimal MinFlowDiff
	{
		get => MinFlowAfter - MinFlowBefore;
	}

	public decimal DayDistributeAmountDiff
	{
		get => DayDistributeAmountAfter - DayDistributeAmountBefore;
	}


	public decimal MinRateDiff
	{
		get
		{
			return MinRateAfter - MinRateBefore;

		}
	}





}


