using System.Security.Policy;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-附表十一、隊員目標
/// </summary>
public class RA040 : ReportDataModel
{
    
    public List<RA040_Item> Items { get; set; } = new List<RA040_Item>();

}

/// <summary>
/// 隊員
/// </summary>
public class RA040_Item
{
    public string Name { get; set; }

    
    /// <summary>
    /// 目標管長
    /// </summary>
    public double? TargetPipeLength { get; set; }


    /// <summary>
    /// 目標件數
    /// </summary>
    public double? TargetAmount { get; set; }
}
    
