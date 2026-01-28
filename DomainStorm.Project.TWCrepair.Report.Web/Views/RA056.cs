using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-七.系統暨成本費用工作報表
/// </summary>
public class RA056 : ReportDataModel
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
	/// 作業期間-起
	/// </summary>
	public DateTime OperationStartDate { get; set;  }

	/// <summary>
	/// 作業期間-訖
	/// </summary>
	public DateTime OperationEndDate { get; set; }

	/// <summary>
	/// 依月份統計的項目
	/// </summary>
	public List<RA056_MonthItem> MonthItems { get; set; } = new List<RA056_MonthItem>();
	

	

	/// <summary>
	/// 依系統統計的項目
	/// </summary>
	public List<RA056_SystemItem> SystemItems { get; set; } = new List<RA056_SystemItem>();
	
	public RA056()
	{
		for(var i = 1; i<= 12; i++)
		{
			MonthItems.Add(new RA056_MonthItem
			{
				Month = i
			});
		}
	}

	public void AppendSumItem()
	{
		var monthSum = new RA056_MonthItem(MonthItems);
		monthSum.IsSum = true;
		monthSum.HasData = true;
		MonthItems.Insert(0, monthSum);

		var sysSum = new  RA056_SystemItem(SystemItems);
		sysSum.IsSum = true;
		SystemItems.Insert(0, sysSum);
	}



}

public class RA056_BaseItem
{
	/// <summary>
	/// 丄作日統計 : 0檢漏作業	1確認作業	2水壓調查	3計量調查	4其它現場	5資料整備	6圖面整備	7其它事項
	/// </summary>
	public decimal[] WorkDays { get; set; } = new decimal[8];

	/// <summary>
	/// 工作日合計
	/// </summary>
	public decimal WorkDaysSum
	{
		get => WorkDays.Sum(x => x );
	}

	/// <summary>
	/// 檢漏作業 : 0配水管	1給水管	2止水栓	3制水閥	4救火栓	5管線聽音日數
	/// </summary>

	public int[] Checks { get ; set; } = new int[6];

	/// <summary>
	/// 系統確認 : 確認漏水	確認無漏
	/// </summary>
	public int[] SysteConfirms { get; set; } = new int[2];


	/// <summary>
	/// 管線隊費用 : 0差旅費	1車輛油料費	2人事費	3其它事務費	4車輛維護費	5器具維護費	6儀器折舊費	7加班費 8檢漏工具費
	/// </summary>
	public int[] Costs { get; set; } = new int[9];

	/// <summary>
	/// 管線隊費用合計
	/// </summary>
	public int CostsSum
	{
		get => Costs.Sum(x => x );
	}

	public RA056_BaseItem() { }

	
	


	/// <summary>
	/// 產生合計 item 
	/// </summary>
	/// <param name="items"></param>
	public RA056_BaseItem (List<RA056_BaseItem> items)
	{
		for(var i = 0; i <WorkDays.Length; i++)
		{
			WorkDays[i] = items.Sum(x => x.WorkDays[i]);
		}

		for (var i = 0; i < Checks.Length; i++)
		{
			this.Checks[i] = items.Sum(x => x.Checks[i]);
		}

		for (var i = 0; i < SysteConfirms.Length; i++)
		{
			this.SysteConfirms[i] = items.Sum(x => x.SysteConfirms[i]);
		}

		for (var i = 0; i < Costs.Length; i++)
		{
			this.Costs[i] = items.Sum(x => x.Costs[i]);
		}
	}
}

/// <summary>
/// 依月份統計
/// </summary>
public class RA056_MonthItem : RA056_BaseItem
{
	public int Month { get; set; }
	public bool HasData { get; set; }

	public bool IsSum { get; set; }

	public string Name
	{
		get => IsSum ? "月合計" :
			HasData ? $"{Month}月合計" : "";
	}

	public RA056_MonthItem() { }

	public RA056_MonthItem(List<RA056_MonthItem> items) : base(items.Select(x => x as RA056_BaseItem).ToList())
	{

	}
}

/// <summary>
/// 依系統統計
/// </summary>
public class RA056_SystemItem : RA056_BaseItem
{
	public Guid WaterSupplySystemId { get; set; }
	 public string WaterSupplySystemName { get; set; }

	public bool IsSum { get; set; }

	public string Name
	{
		get => IsSum ? "區域合計" : WaterSupplySystemName;
	}

	public RA056_SystemItem() { }
	public RA056_SystemItem(List<RA056_SystemItem> items) : base(items.Select(x => x as RA056_BaseItem).ToList())
	{

	}

}


