using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 年度計畫報告-附表十、儀器需求統計
/// </summary>
public class RA039 : ReportDataModel
{
    /// <summary>
    /// 現場作業-人員配置
    /// </summary>
    public int? OnSitePeople { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<RA039_Catagory> Catagories { get; set; } = new List<RA039_Catagory>();


    /// <summary>
    /// 取得該種類的儀器
    /// </summary>
    /// <param name="catagoryIndex"></param>
    /// <returns></returns>
    public List<RA039_Item> CatagoryItems(int catagoryIndex)
    {
        var categoryName = Catagories[catagoryIndex].Name;
        return Items.Where(x => x.CategoryName == categoryName)
            .OrderBy(x => x.Sort)
            .ToList();
    }

    public List<RA039_Item> Items { get; set; } = new List<RA039_Item>();

}

/// <summary>
/// 財產設備種類
/// </summary>
public class RA039_Catagory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
    



/// <summary>
/// 儀器
/// </summary>
public class RA039_Item
{
    /// <summary>
    /// 設備種類名稱
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// 設備代碼
    /// </summary>
    public Guid? EquipmentWordId { get; set; }

    /// <summary>
    /// 儀器名稱
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 規格
    /// </summary>
    public string? Specification { get; set; }

    /// <summary>
    /// 現有數量_堪用
    /// </summary>
    public int CurrentAmountUsable { get; set; }

    /// <summary>
    /// 現有數量_待修
    /// </summary>
    public int CurrentAmountRepair { get; set; }

    /// 現有數量_無法修復
    /// </summary>
    public int CurrentAmountBroken { get; set; }

    /// <summary>
    /// 現有數量合計
    /// </summary>
    public int CurrentAmount { get; set; }
    

    /// <summary>
    /// 計畫數量
    /// </summary>
    public int PlanAmount { get; set; }

    /// <summary>
    /// 需求數量
    /// </summary>
    public int NeedAmount { get; set; }


    public short Sort { get; set; }
    
}