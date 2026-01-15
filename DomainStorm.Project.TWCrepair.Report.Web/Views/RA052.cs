using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-四.最小流量率(修正值)比較表
/// </summary>
public class RA052 : ReportDataModel
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
	///c.本期檢修後最小流量
	/// </summary>
	public decimal? MinFlowAfter { get; set; }


	/// <summary>
	/// q.檢修前日配水量
	/// </summary>
	public decimal? DayDistributeAmountBefore { get; set; }


	/// <summary>
	/// r.檢修後日配水量
	/// </summary>
	public decimal? DayDistributeAmountAfter { get; set; }

	/// <summary>
	/// 最小流量率(檢修前)
	/// </summary>
	public decimal? MinFlowRateBefore { get; set; }

	/// <summary>
	/// 最小流量率(檢修後)
	/// </summary>
	public decimal? MinFlowRateAfter { get; set; }


	/// <summary>
	/// 最高平均水壓(檢修前)
	/// </summary>
	public decimal? MaxPressureBefore { get; set; }

	/// <summary>
	/// 最高平均水壓(檢修後)
	/// </summary>
	public decimal? MaxPressureAfter { get; set; }



	/// <summary>
	/// s.檢修前平均水壓
	/// </summary>
	public decimal? AveragePressureBefore { get; set; }


	/// <summary>
	/// t.檢修後平均水壓
	/// </summary>
	public decimal? AveragePressureAfter { get; set; }





	/// <summary>
	/// (水壓換算裡的)檢修前最小流量
	/// </summary>
	public decimal? MinFlowBefore2
	{
		get
		{
			if (MaxPressureBefore.HasValue && MaxPressureBefore.Value > 0 && AveragePressureBefore.HasValue && MinFlowBefore.HasValue)
			{
				return Math.Round(
						(decimal)Math.Pow((double)(AveragePressureBefore.Value / MaxPressureBefore.Value), 0.5) * MinFlowBefore.Value,
						2,
						MidpointRounding.AwayFromZero);


			}
			else
				return null;
		}
	}

	/// <summary>
	///(水壓換算裡的)檢修後最小流量
	/// </summary>
	public decimal? MinFlowAfter2
	{
		get
		{
			if (MaxPressureAfter.HasValue && MaxPressureAfter.Value > 0 && AveragePressureAfter.HasValue && MinFlowAfter.HasValue)
			{
				return Math.Round(
						(decimal)Math.Pow((double)(AveragePressureAfter.Value / MaxPressureAfter.Value), 0.5) * MinFlowAfter.Value,
						2,
						MidpointRounding.AwayFromZero);


			}
			else
				return null;
		}
	}



	/// <summary>
	/// (水壓換算裡的)檢修前最小流量率
	/// </summary>
	public decimal? MinFlowBeforeRate2
	{
		get
		{
			if (DayDistributeAmountBefore.HasValue && DayDistributeAmountBefore.Value > 0 && MinFlowBefore2.HasValue)
			{
				return Math.Round(
						100 * MinFlowBefore2.Value / DayDistributeAmountBefore.Value,
						2,
						MidpointRounding.AwayFromZero);


			}
			else
				return null;
		}
	}

	/// <summary>
	/// (水壓換算裡的)檢修後最小流量率
	/// </summary>
	public decimal? MinFlowAfterRate2
	{
		get
		{
			if (DayDistributeAmountAfter.HasValue && DayDistributeAmountAfter.Value > 0 && MinFlowAfter2.HasValue)
			{
				return Math.Round(
						100 * MinFlowAfter2.Value / DayDistributeAmountAfter.Value,
						2,
						MidpointRounding.AwayFromZero);


			}
			else
				return null;
		}
	}




	public decimal? Diff(decimal? before, decimal? after)
	{
		return after - before;
	}

	public decimal? DiffRate(decimal? before, decimal? after)
	{
		if (after.HasValue && before.HasValue && before.Value > 0)
		{
			return Math.Round(100 * Diff(before, after)!.Value / before.Value, 2, MidpointRounding.AwayFromZero);
		}
		else
		{
			return null;
		}
	}



}