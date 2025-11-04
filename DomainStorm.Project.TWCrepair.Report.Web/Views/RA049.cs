
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-一.作業成果分析表
/// </summary>
public class RA049 : ReportDataModel
{
    public int Year { get; set; }
    public string WorkSpaceName { get; set; }


    /// <summary>
    /// 作業期間-起
    /// </summary>
    public DateTime? OperationStartDate { get; set; }

    /// <summary>
    /// 作業期間-訖
    /// </summary>
    public DateTime? OperationEndDate { get; set; }

    /// <summary>
    /// 作業人員數
    /// </summary>
    public int? Operators { get; set; }


    /// <summary>
    /// 工作日合計
    /// </summary>
    public decimal WorkDayTotal { get; set; }


    /// <summary>
    /// 本期最小流量的點
    /// </summary>
    public FlowData? ThisYearMinFlowData { get; set; }

    /// <summary>
    /// 前期最小流量的點
    /// </summary>
    public FlowData? LastYearMinFlowData { get; set; }


    //漏水復原率
    public double LeackageRecover 
    { 
        get
        {
            /*
             (a.本期檢修前最小流量-b. 前期檢修後最小流量 )/ (i.兩次間隔年數 * e.兩期間隔總配水量 / d.兩期間隔天數) * 100
             */
            if (ThisYearMinFlowData != null && LastYearMinFlowData != null && ThisYearMinFlowData.MinFlow.HasValue && LastYearMinFlowData.MinFlow.HasValue)
            {
                return Math.Round(100.0 * (ThisYearMinFlowData.MinFlow.Value - LastYearMinFlowData.MinFlow.Value)
                          / (ThisYearMinFlowData.MeasureDate.Year - LastYearMinFlowData.MeasureDate.Year)
                          // todo 兩期間隔總配水量 ?
                          / (ThisYearMinFlowData.MeasureDate - LastYearMinFlowData.MeasureDate).TotalDays
                          , 2);
            }
            else
            {
                return 0;
            }
        }
    }

}


public class FlowData
{
    public string LocationNumber { get; set; }
    public DateTime MeasureDate { get; set; }
    public double? MinFlow { get; set; }
}
