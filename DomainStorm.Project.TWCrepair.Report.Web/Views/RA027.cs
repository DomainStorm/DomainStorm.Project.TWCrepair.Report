using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-目錄
/// </summary>
public class RA027 : ReportDataModel
{
    public int Year { get; set; }
}