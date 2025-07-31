using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 流量分析-檢前總表/檢後總表
/// </summary>
public class RA041 : ReportDataModel
{
    /// <summary>
    /// 量測日期
    /// </summary>
    public DateTime MeasuerDate {  get; set; }

    public string BeforeOrAfter { get; set; }

    public string SheetName { get; set; }

    public List<RA041_Item> Items = new List<RA041_Item>();


    /// <summary>
    /// 日水量合計
    /// </summary>
    public double? DayTotal
    {
        get => Math.Round( Items.Sum(x => x.DiffValue),2);
    }

    /// <summary>
    /// 最小值記錄-時間
    /// </summary>
    public DateTime? MinValueTime
    {
        get => Items.OrderBy(x => x.MinValue).FirstOrDefault()?.MinTime;
    }

    /// <summary>
    /// 最小值記錄-最小值
    /// </summary>
    public double? MinValue
    {
        get => Items.Min(x => x.MinValue);
    }

    /// <summary>
    /// 最小值流量率-最小值
    /// </summary>
    public double? MinCmd
    {
        get => MinValue * 1000;
    }

    public double MinCmdPercentage
    {
        get => DayTotal.HasValue && DayTotal.Value > 0 && MinCmd.HasValue ?
            Math.Round(100.0 * MinCmd.Value / DayTotal.Value, 2)
            : 0;
    }

}

public class RA041_Item
{
    /// <summary>
    /// 地點
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// 起始值
    /// </summary>
    public double? StartValue { get; set; }

    /// <summary>
    /// 終止值
    /// </summary>
    public double? EndValue { get; set; }

    public double DiffValue
    {
        get => Math.Round( (EndValue ?? 0) - (StartValue ?? 0) , 2);
    }

    /// <summary>
    /// 正機流
    /// </summary>
    public bool Positive { get; set; }

    public DateTime ? MinTime { get; set; }

    public double? MinValue { get; set; }

}

