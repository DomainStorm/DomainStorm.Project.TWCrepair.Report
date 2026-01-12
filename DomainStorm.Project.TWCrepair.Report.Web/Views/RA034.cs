using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using System.ComponentModel.DataAnnotations;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-附表四、檢漏作業計劃差旅費分析表
/// </summary>
public class RA034 : ReportDataModel
{
    public int Year { get; set; }

    

    public int CurrentPeople { get; set; }
    

    public List<RA034Item> Items { get; set; } = new List<RA034Item>();
    public RA034Item SumItem { get; set; } = new RA034Item();
}

public class RA034Item
{

    public string? WorkSpaceName
    {
        get => WorkSpace?.WorkSpaceName;
    }

    public DepartmentWorkSpaceSimple? WorkSpace { get; set; }


    /// <summary>
    /// 計劃作業管長
    /// </summary>
    public int? PlanPipeLength { get; set; }


    /// <summary>
    /// 距離
    /// </summary>
    [StringLength(30)]
    public string? Distance { get; set; }

    /// <summary>
    /// 作業區管長佔作業總管長 % 
    /// </summary>
    public double? PipeLengthPercentage { get; set; }

   
    /// <summary>
    /// 差旅費小計天數
    /// </summary>
    public int? SubTotalTraveDays { get; set; }

    /// <summary>
    /// 差旅費小計費用
    /// </summary>
    public int? SubTotalTraveExpense { get; set; }

    /// <summary>
    /// 短程差旅天數
    /// </summary>
    public int? ShortDistanceDays { get; set; }

    /// <summary>
    /// 短程差旅費用
    /// </summary>
    public int? ShortDistanceExpense { get; set; }

    /// <summary>
    /// 長程差旅天數
    /// </summary>
    public int? LongDistanceDays { get; set; }

    /// <summary>
    /// 長程差旅費用
    /// </summary>
    public int? LongDistanceExpense { get; set; }
}
