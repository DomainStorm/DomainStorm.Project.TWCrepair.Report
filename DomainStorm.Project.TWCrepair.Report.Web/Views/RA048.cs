using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-目錄
/// </summary>
public class RA048 : ReportDataModel
{
    public int Year { get; set; }
    public string WorkSpaceName { get; set; }
}

