using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-附表一 抄見率暨戶日配水量明細表
/// </summary>
public class RA031 : ReportDataModel
{
    public int Year { get; set; }
    public List<YearPlanStatistics> Items { get; set; } = new List<YearPlanStatistics>();
}