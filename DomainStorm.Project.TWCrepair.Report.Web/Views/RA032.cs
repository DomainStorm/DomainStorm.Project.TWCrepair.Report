using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-附表二、檢漏工作計劃表
/// </summary>
public class RA032 : ReportDataModel
{
    public int Year { get; set; }

    /// <summary>
    /// 合計列, style 及要顯示的欄位不一樣, 從 list 抽出
    /// </summary>
    public RA032Item SumItem { get; set; }
    public List<RA032Item> Items { get; set; } = new List<RA032Item>();

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


public class RA032Item
{
    public Guid Id { get; set; }

    /// <summary>
    /// 現有人員數
    /// </summary>
    public int CurrentPeople { get; set; }

    /// <summary>
    /// 作業區名稱
    /// </summary>
    public string OperationAreaName { get; set; }

    /// <summary>
    /// 漏水率降低目標數(%) (最小流量率降低數)
    /// </summary>
    public double? LeakageLowerTarget { get; set; }

    /// <summary>
    /// 距離
    /// </summary>
    public string? Distance { get; set; }

    /// <summary>
    /// 計劃作業管長
    /// </summary>
    public int? PlanPipeLength { get; set; }

    /// <summary>
    /// 作業區管長佔作業總管長 % 
    /// </summary>
    public double? PipeLengthPercentage { get; set; }

    /// <summary>
    /// 用戶數
    /// </summary>
    public int? CustomerAmount { get; set; }


    /// <summary>
    ///「地下漏水檢出件數(配水管線)
    /// </summary>
    public int? CheckOutAmountDistributionPipe { get; set; }

    /// <summary>
    ///「地下漏水檢出件數(用戶外線)
    /// </summary>
    public int? CheckOutAmountOutdoorPipe { get; set; }

    /// <summary>
    /// 有效年數(成本年數)
    /// </summary>
    public int EfficientYear { get; set; } = 1;

    /// <summary>
    /// 生產成本(元/M3)	(供水成本)
    /// </summary>
    public double? ProductionCost { get; set; }


    /// <summary>
    /// 檢出地下漏水(CMD)
    /// </summary>
    public double? CheckOutCMD { get; set; }

    /// <summary>
    /// 作業費
    /// </summary>
    public int? OperationExpense { get; set; }

    /// <summary>
    /// 用人費
    /// </summary>
    public int? PersonnelExpense { get; set; }


    /// <summary>
    /// 器具折舊費用
    /// </summary>
    public int? DepreciationExpense { get; set; }


    /// <summary>
    /// 檢漏作業總經費
    /// </summary>
    public int CheckOutTotalExpense { get; set; }

    /// <summary>
    /// 檢漏成本(元/CMD)
    /// </summary>
    public double? CheckOutCostPerCMD { get; set; }

    /// <summary>
    /// 檢漏成本(元/Km)
    /// </summary>
    public double? CheckOutCostPerKm { get; set; }

    /// <summary>
    /// 效益額
    /// </summary>
    public int? BenefitAmount { get; set; }

    /// <summary>
    /// 作業期間-起
    /// </summary>
    public DateTime? OperationStartDate { get; set; }

    /// <summary>
    /// 作業期間-訖
    /// </summary>
    public DateTime? OperationEndDate { get; set; }
}