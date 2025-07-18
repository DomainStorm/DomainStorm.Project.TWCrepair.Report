using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-工作計畫
/// </summary>
public class RA028 : ReportDataModel
{
    /// <summary>
    /// 工作計劃
    /// </summary>
    public string WorkPlan { get; set; }

    /// <summary>
    /// 現況分析_概述
    /// </summary>
    public string? CurrentSituation { get; set; }

    /// <summary>
    /// 現有員額-師
    /// </summary>
    public int CurrentPost1 { get; set; }

    /// <summary>
    /// 現有員額-員
    /// </summary>
    public int CurrentPost2 { get; set; }

    /// <summary>
    /// 現有員額-士
    /// </summary>
    public int CurrentPost3 { get; set; }

    /// <summary>
    /// 計畫員額-師
    /// </summary>
    public int PlanPost1 { get; set; }

    /// <summary>
    /// 計畫員額-員
    /// </summary>
    public int PlanPost2 { get; set; }

    /// <summary>
    /// 計畫員額-士
    /// </summary>
    public int PlanPost3 { get; set; }


    /// <summary>
    /// 需補員額-師
    /// </summary>
    public int NeedPost1 { get; set; }

    /// <summary>
    /// 需補員額-員
    /// </summary>
    public int NeedPost2 { get; set; }

    /// <summary>
    /// 需補員額-士
    /// </summary>
    public int NeedPost3 { get; set; }

    /// <summary>
    /// 現況分析_備註
    /// </summary>
    public string? CurrentNotes { get; set; }


    /// <summary>
    /// 作業區之選定及分析
    /// </summary>
    public string? WorkSpaceAnalysis { get; set; }

    /// <summary>
    /// 儀器
    /// </summary>
    public virtual ICollection<RA028Instrument> YearPlanReportInstruments { get; set; } = new List<RA028Instrument>();

    /// <summary>
    /// 各作業區漏水率降低數
    /// </summary>
    public string WorkSpace_LeakageLowerTarget { get; set; }

    /// <summary>
    /// 地下漏水檢漏水量(CMD)
    /// </summary>
    public double WorkSpace_CheckOutCMD { get; set; }

    /// <summary>
    /// 檢漏管長(KM)
    /// </summary>
    public int WorkSpace_PipeLength { get; set; }

    /// <summary>
    /// 地下漏水管線件數
    /// </summary>
    public int WorkSpace_CheckOutAmountDistributionPipe { get; set; }

    /// <summary>
    /// 地下漏水外線件數
    /// </summary>
    public int WorkSpace_CheckOutAmountOutdoorPipe { get; set; }

}

public class RA028Instrument
{
    /// <summary>
    /// 儀器名稱
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 規格
    /// </summary>
    public string? Specification { get; set; }

    /// <summary>
    /// 現有數量
    /// </summary>
    public int CurrentAmount { get; set; }

    /// <summary>
    /// 計畫數量
    /// </summary>
    public int PlanAmount { get; set; }

    /// <summary>
    /// 需求數量
    /// </summary>
    /// 
    public int NeedAmount { get; set; }
}