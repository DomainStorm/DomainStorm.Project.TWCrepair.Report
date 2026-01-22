using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-六.最小流量比較表
/// </summary>
public class RA054 : ReportDataModel
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


	
	public List<RA054_Item> Items { get; set; } = new List<RA054_Item>();

	

}

public class RA054_Item
{
	/// <summary>
	/// 日期
	/// </summary>
	public DateTime MeasureDate { get; set; }

	/// <summary>
	/// 最低流量
	/// </summary>
	public decimal? LowestFlow { get; set; }


	/// <summary>
	/// 日配水量
	/// </summary>
	public decimal? DistributeAmount { get; set; }


	/// <summary>
	/// 最小流量率
	/// </summary>
	public decimal? LowerFlowRate
	{
		get
		{
			if (DistributeAmount.HasValue && DistributeAmount.Value > 0 && LowestFlow.HasValue)
			{
				return Math.Round(100.0M * LowestFlow.Value / DistributeAmount.Value, 2, MidpointRounding.AwayFromZero);
			}
			else
				return null;
		}
	}

	/// <summary>
	/// 用戶數
	/// </summary>
	public int CustomerAmount { get; set; }


	/// <summary>
	/// 戶配水量
	/// </summary>
	public decimal? CustomerDirstribute
	{
		get
		{
			if(CustomerAmount > 0 && DistributeAmount.HasValue)
			{

				return Math.Round(DistributeAmount.Value / CustomerAmount, 2, MidpointRounding.AwayFromZero);
			}
			else
				return null;
		}
	}

	public string? BeforeOrAfter { get; set; }	




}


