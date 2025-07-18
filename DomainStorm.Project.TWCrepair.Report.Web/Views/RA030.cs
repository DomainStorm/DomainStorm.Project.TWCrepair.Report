
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-計劃經費
/// </summary>
public class RA030 : ReportDataModel
{
    /// <summary>
    /// 經費
    /// </summary>
    public string Funding { get; set; }

    /// <summary>
    /// 效益
    /// </summary>
    public string Benefit { get; set; }

    /// <summary>
    /// 結論
    /// </summary>
    public string? Conclusion { get; set; }
}

