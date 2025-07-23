using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-附表三、檢漏作業各系統費用分析表
/// </summary>
public class RA033 : ReportDataModel
{
    public int Year { get; set; }

    /// <summary>
    /// 合計列, style 及要顯示的欄位不一樣, 從 list 抽出
    /// </summary>
    public YearPlanExpenseAllocateWorkSpace SumItem { get; set; }

    /// <summary>
    /// 合計的百分比, style 及要顯示的欄位不一樣, 從 list 抽出
    /// </summary>
    public YearPlanExpenseAllocateWorkSpace SumPercentageItem { get; set; }


    public YearPlanExpenseAllocate YearPlanExpenseAllocate { get; set; }


}

