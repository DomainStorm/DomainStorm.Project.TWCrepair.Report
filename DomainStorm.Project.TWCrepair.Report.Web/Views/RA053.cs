using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-五.作業前後水壓比較表
/// </summary>
public class RA053 : ReportDataModel
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
	/// 檢修前日期
	/// </summary>
	public DateTime BeforeDate { get; set; }

	/// <summary>
	/// 檢修後日期
	/// </summary>
	public DateTime AfterDate { get; set; }

	/// <summary>
	/// 各地點的項目
	/// </summary>
	public List<RA053_Item> Items { get; set; } = new List<RA053_Item>();

	/// <summary>
	/// 分析項目 index 0 : 最高點 ;  index 1 : 平均點 ;  index 2 : 最低點
	/// </summary>
	public List<RA053_Item> AnalysisItems { get; set; } = new List<RA053_Item>();

}

public class RA053_Item
{
	/// <summary>
	/// 地點名稱
	/// </summary>
	public string LocationNumber { get; set; }
	/// <summary>
	/// 地點編號
	/// </summary>
	public string Location { get; set; }

	/// <summary>
	/// 檢修前最高點
	/// </summary>
	public decimal HighestBefore { get; set; } = 0M;
	/// <summary>
	/// 檢修後最高點
	/// </summary>
	public decimal HighestAfter { get; set; } = 0M;

	/// <summary>
	/// 檢修前平均點
	/// </summary>
	public decimal AverageBefore { get; set; } = 0M;
	/// <summary>
	/// 檢修後平均點
	/// </summary>
	public decimal AverageAfter { get; set; } = 0M;

	/// <summary>
	/// 檢修前最低點
	/// </summary>
	public decimal LowestBefore { get; set; } = 0M;
	/// <summary>
	/// 檢修後最低點
	/// </summary>
	public decimal LowestAfter { get; set; } = 0M;


	/// <summary>
	/// 檢修前總水頭
	/// </summary>
	public double? TotalWaterBefore { get; set; } = 0;

	/// <summary>
	/// 檢修後總水頭
	/// </summary>
	public double? TotalWaterAfter { get; set; } = 0;

}


