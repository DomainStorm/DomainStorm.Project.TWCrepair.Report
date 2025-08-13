using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-附表六、各系統管徑、管長暨附屬設備統計表
/// </summary>
public class RA036 : ReportDataModel
{
    /// <summary>
    /// 年度
    /// </summary>
    public int Year { get; set; }

    public List<RA036WorkSapce> WorkSapces { get; set; } = new List<RA036WorkSapce>();

    /// <summary>
    /// 各管徑的統計資料, 會有一筆合計列在First()
    /// </summary>
    public List<RA036PiepSiteData> PiepSiteDatas { get; set; } = new List<RA036PiepSiteData>();




}

public class RA036WorkSapce
{
    public Guid DepartmentId { get; set; }
    /// <summary>
    /// 廠所代碼
    /// </summary>
    public Guid SiteId { get; set; }

    /// <summary>
    /// 廠所名稱
    /// </summary>
    public string SiteName { get; set; }


    /// <summary>
    /// 供水系統代碼
    /// </summary>
    public Guid WaterSupplySystemId { get; set; }

    /// <summary>
    /// 供水系統名稱
    /// </summary>
    public string WaterSupplySystemName { get; set; }

}


/// <summary>
/// 各管徑的各廠所的資料
/// </summary>
public class RA036PiepSiteData
{
    /// <summary>
    /// 管徑
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 該管徑的各廠所的計劃管長
    /// </summary>
    public List<double> Lengthes { get; set; } = new List<double>();

    /// <summary>
    /// 小計
    /// </summary>
    public double SubTotal
    {
        get => Lengthes.Sum();
    }
}
