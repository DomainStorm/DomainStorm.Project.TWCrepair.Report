
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-作業概要
/// </summary>
public class RA029 : ReportDataModel
{
    /// <summary>
    /// 現場作業-人員配置
    /// </summary>
    public int? OnSitePeople { get; set; }

    /// <summary>
    /// 現場作業-工作內容
    /// </summary>
    public string? OnSiteWork { get; set; }


    /// <summary>
    /// 內務作業-人員配置
    /// </summary>
    public int? InternalPeople { get; set; }

    /// <summary>
    /// 內務作業-工作內容
    /// </summary>
    public string? InternalWork { get; set; }


    /// <summary>
    /// 作業概要_備註
    /// </summary>
    public string? OperationNotes { get; set; }

    /// <summary>
    /// 作業方式
    /// </summary>
    public string? OperationWay { get; set; }


    /// <summary>
    /// 成果比對
    /// </summary>
    public string? Achievements { get; set; }


    /// <summary>
    /// 作業區
    /// </summary>
    public virtual ICollection<RA029WorkSpace> WorkSpaces { get; set; } = new List<RA029WorkSpace>();
}

public class RA029WorkSpace
{
    /// <summary>
    /// 作業區名稱
    /// </summary>
    //public string OperationAreaName { get; set; }

    public string? WorkSpaceName
    {
        get => WorkSpace?.WorkSpaceName;
    }

    public DepartmentWorkSpaceSimple? WorkSpace { get; set; }

    /// <summary>
    /// 作業區管長佔作業總管長 %
    /// </summary>
    public double? PipeLengthPercentage { get; set; }

    /// <summary>
    /// 作業期間-起
    /// </summary>
    public DateTime? OperationStartDate { get; set; }

    /// <summary>
    /// 作業期間-訖
    /// </summary>
    public DateTime? OperationEndDate { get; set; }


    /// <summary>
    /// 作業期間含各月份
    /// </summary>
    public bool[] OperationMonthes { get; set; } = new bool[12];



}