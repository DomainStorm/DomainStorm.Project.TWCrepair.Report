using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-附表四、檢漏作業計劃差旅費分析表
/// </summary>
public class RA034 : ReportDataModel
{
    public int Year { get; set; }

    /// <summary>
    /// 現有人員數-師
    /// </summary>
    public int? CurrentPeople1 { get; set; }

    /// <summary>
    /// 現有人員數-員
    /// </summary>
    public int? CurrentPeople2 { get; set; }

    /// <summary>
    /// 現有人員數-士
    /// </summary>
    public int? CurrentPeople3 { get; set; }


}






