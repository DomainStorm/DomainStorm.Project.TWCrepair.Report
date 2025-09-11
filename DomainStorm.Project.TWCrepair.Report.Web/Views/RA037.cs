using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-附表七、各系統大區NRW
/// </summary>
public class RA037 : ReportDataModel
{
    /// <summary>
    /// 年度
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// 前五年的及今年度
    /// </summary>
    public int[] LastYears { get; set; } = new int[6];

    public List<RA037WorkSapce> WorkSapces { get; set; } = new List<RA037WorkSapce>();
}

public class RA037WorkSapce
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


    /// <summary>
    /// 前5年度及今年是否有資料
    /// </summary>
    public bool[] LastYearsHasData { get; set; } = new bool[6];


    /// <summary>
    /// 計劃作業管長
    /// </summary>
    public int? PlanPipeLength { get; set; }

    /// <summary>
    /// 總售水量 (抄見量)
    /// </summary>
    public int ReadAmount { get; set; }


    /// <summary>
    /// 總供水量 (配水量)
    /// </summary>
    public int DistributionAmount { get; set; }

    /// <summary>
    /// 日售水量
    /// </summary>
    public int DayReadAmount
    {
        get => ReadAmount / 123;
    }


    /// <summary>
    /// 日供水量 
    /// </summary>
    public int DayDistributionAmount
    {
        get => DistributionAmount / 123;
    }

    /// <summary>
    /// 售水率
    /// </summary>
    public double SaleWaterRate
    {
        get => DayDistributionAmount > 0 ?
            Math.Round((double)DayReadAmount / (double)DayDistributionAmount, 4) :
            0;
    }

    /// <summary>
    /// 無收費水量NRW
    /// </summary>
    public int NRW
    {
        get => DayDistributionAmount - DayReadAmount;
    }

    /// <summary>
    /// 每公里NRW
    /// </summary>
    public int NRWPerKm
    {
        get => PlanPipeLength.HasValue && PlanPipeLength.Value > 0 ?
            NRW / PlanPipeLength.Value :
            0;
    }


    /// <summary>
    /// 售水率80%之NRW
    /// </summary>
    public int NRWof80Percent
    {
        get => DayDistributionAmount - (int)(0.8 * DayReadAmount);
    }


    /// <summary>
    /// (營收系統的)用戶數
    /// </summary>
    public int Customer { get; set; }

    /// <summary>
    /// (分區計量管網)用戶數
    /// </summary>
    public int  CustomerFromSubSection { get; set; }

    /// <summary>
    /// 小區覆蓋率
    /// </summary>
    public double CoverRate
    {
        get => Customer > 0 ?
             Math.Round( (double)CustomerFromSubSection / (double)Customer  , 4) :
            0;
    }



}



