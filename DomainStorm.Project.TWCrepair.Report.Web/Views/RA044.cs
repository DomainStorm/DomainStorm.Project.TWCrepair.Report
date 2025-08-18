using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;



/// <summary>
/// 區處執行管控表
/// </summary>
public class RA044 : ReportDataModel
{
    public List<RA044_Item> Items { get; set; }
}


public class RA044_Item
{

    public Guid Id { get; set; }

    /// <summary>
    /// 管控表編號
    /// </summary>
    public string? SerialNumber { get; set; }


    /// <summary>
    /// 區處名稱
    /// </summary>
    public string DepartmentName { get; set; }


    /// <summary>
    /// 廠所名稱
    /// </summary>
    public string SiteName { get; set; }


    /// <summary>
    /// 供水系統名稱
    /// </summary>
    public string WaterSupplySystemName { get; set; }


    /// <summary>
    /// 工作區名稱
    /// </summary>
    public string WorkSpaceName { get; set; }


    /// <summary>
    /// 小區名稱
    /// </summary>
    public string SmallRegionName { get; set; }


    /// <summary>
    /// 年度
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// 建議日期
    /// </summary>
    public DateTime? SuggestionDate { get; set; }

    /// <summary>
    /// 依據
    /// </summary>
    public string? BasedOn { get; set; }

    /// <summary>
    /// 建議事項
    /// </summary>
    public string? Suggestions { get; set; }

    /// <summary>
    /// 核准日期及文號
    /// </summary>
    public string? ApproveDateAndNo { get; set; }

    /// <summary>
    /// 區處執行情形
    /// </summary>
    public string ExecuteSituation { get; set; }

    /// <summary>
    /// 承辦單位代碼
    /// </summary>
    public Guid? InChargeDepartmentId { get; set; }

    /// <summary>
    /// 承辦單位名稱
    /// </summary>
    public string? InChargeDepartmentName { get; set; }

    /// <summary>
    /// 承辦人姓名
    /// </summary>
    public string? InChargePerson { get; set; }


    /// <summary>
    /// 預定完成日
    /// </summary>
    public DateTime? PlanFinishDate { get; set; }

    /// <summary>
    /// 實際完成日
    /// </summary>
    public DateTime? FinishDate { get; set; }

    /// <summary>
    /// 現勘複查日及結果
    /// </summary>
    public string? ReviewDateAndResult { get; set; }


    /// <summary>
    /// 區處建議解除列管
    /// </summary>
    public bool SuggesteDelisting { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// 總處解除列管
    /// </summary>
    public bool Delisting { get; set; }

    /// <summary>
    /// 總處意見
    /// </summary>
    public string? Comments { get; set; }
}

