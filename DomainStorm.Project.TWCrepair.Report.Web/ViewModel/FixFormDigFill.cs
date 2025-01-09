
namespace DomainStorm.Project.TWCrepair.Report.Web.ViewModel;


/// <summary>
/// 各種(挖填坼修)的尺寸描述
/// </summary>
public class FixFormDigFillItem
{
    /// <summary>
    /// 長
    /// </summary>
    public decimal? Length { get; set; }

    /// <summary>
    /// 寛
    /// </summary>
    public decimal? Width { get; set; }

    /// <summary>
    /// 深度(或厚度)
    /// </summary>
    public decimal? Depth { get; set; }
}

/// <summary>
/// 柏油路面修護記錄
/// </summary>
public class FixFormDigFillAsphaltRepair
{
    public List<FixFormDigFillItem> FixFormDigFillItems { get; set; }

    /// <summary>
    /// 臨時修護
    /// </summary>
    public bool TemporaryRepair { get; set; }

    /// <summary>
    /// 面積
    /// </summary>
    public decimal? Area { get; set; }
}


public class FixFormDigFill
{
    /// <summary>
    /// 挖土機清單
    /// </summary>
    public List<FixFormDigFillItem> ExcavatorItems { get; set; }

    /// <summary>
    /// 挖土機挖方
    /// </summary>
    public decimal? ExcavatorMass { get; set; }

    /// <summary>
    /// 人工挖掘清單
    /// </summary>
    public List<FixFormDigFillItem> ManualItems { get; set; }

    /// <summary>
    /// 人工挖掘挖方
    /// </summary>
    public decimal? ManualMass { get; set; }

    /// <summary>
    /// 回填砂-原砂
    /// </summary>
    public decimal? RawSand { get; set; }

    /// <summary>
    /// 回填砂-換填砂
    /// </summary>
    public decimal? ChangedSand { get; set; }

    /// <summary>
    /// 回填級配-原級配
    /// </summary>
    public decimal? RawLevel { get; set; }

    /// <summary>
    /// 回填級配-換填級配
    /// </summary>
    public decimal? ChangedLevel { get; set; }

    /// <summary>
    /// 回填多功能再生混凝土
    /// </summary>
    public decimal? MRC { get; set; }

    /// <summary>
    /// 換填高流動性低強度混凝土
    /// </summary>
    public decimal? LowStrength { get; set; }

    /// <summary>
    /// 回填其它混凝土詞庫代碼
    /// </summary>
    public Word OtherConcrete { get; set; }


    /// <summary>
    /// 回填其它混凝土
    /// </summary>
    public decimal? OtherConcreteMass { get; set; }

    /// <summary>
    /// 原土回填
    /// </summary>
    public decimal? OriginalSoil { get; set; }

    /// <summary>
    /// 殘土處理
    /// </summary>
    public decimal? ResidueSoil { get; set; }

    /// <summary>
    /// 各項挖填據檢查無誤
    /// </summary>
    public bool DigFillDataChecked { get; set; }

    /// <summary>
    /// 柏油路面拆除
    /// </summary>
    public List<FixFormDigFillItem> AsphaltRemoveItems { get; set; }

    /// <summary>
    /// 柏油路面拆除面積
    /// </summary>
    public decimal? AsphaltRemoveArea { get; set; }

    /// <summary>
    /// 柏油路面切割長度
    /// </summary>
    public decimal? AsphaltCutLength { get; set; }

    /// <summary>
    /// 柏油路面切割面積
    /// </summary>
    public decimal? AsphaltCutArea { get; set; }

    /// <summary>
    /// 柏油路面修護清單
    /// </summary>
    public List<FixFormDigFillAsphaltRepair> AsphaltRepairs { get; set; }

    /// <summary>
    /// 正式路面修復時間
    /// </summary>
    public DateTime? RoadRepairTime { get; set; }

    /// <summary>
    /// 同修復時間
    /// </summary>
    public bool SameRepairTime { get; set; }

    /// <summary>
    /// 柏油路面路權代修
    /// </summary>
    public bool AsphaltProxyRepair { get; set; }

    /// <summary>
    /// 柏油路面路權代修費用
    /// </summary>
    public decimal? AsphaltProxyCost { get; set; }

    /// <summary>
    /// 柏油路權代修單位詞庫代碼
    /// </summary>
    public Word AsphaltProxyUnit { get; set; }


    /// <summary>
    /// 柏油路權代修單位說明
    /// </summary>
    public string? ProxyAsphaltUnitDescription { get; set; }

    /// <summary>
    /// 各項柏油據檢查無誤
    /// </summary>
    public bool AsphaltDataChecked { get; set; }


    /// <summary>
    /// 混疑土路面拆除清單
    /// </summary>
    public List<FixFormDigFillItem> ConcreteRemoveItems { get; set; }


    /// <summary>
    /// 混疑土路面拆除體積
    /// </summary>
    public decimal? ConcreteRemoveMass { get; set; }

    /// <summary>
    /// 混疑土路面切割長度
    /// </summary>
    public decimal? ConcreteCutLength { get; set; }

    /// <summary>
    /// 混疑土路面切割面積
    /// </summary>
    public decimal? ConcreteCutArea { get; set; }

    /// <summary>
    /// 混疑土路面修護清單
    /// </summary>
    public List<FixFormDigFillItem> ConcreteRepairItems { get; set; }

    /// <summary>
    /// 混疑土路面修復體積
    /// </summary>
    public decimal? ConcreteReapirMass { get; set; }

    /// <summary>
    /// 混疑土路面路權代修
    /// </summary>
    public bool ConcreteProxyRepair { get; set; }

    /// <summary>
    /// 混疑土路面路權代修費用
    /// </summary>
    public decimal? ConcreteProxyCost { get; set; }

    /// <summary>
    /// 各項混疑土據檢查無誤
    /// </summary>
    public bool ConcreteDataChecked { get; set; }

    /// <summary>
    /// 其他路面
    /// </summary>
    public string? OtherRoad { get; set; }
}

/// <summary>
/// 計算體積或面積
/// </summary>
public class FixFormDigFillCalculateResult
{
    public Repository.Models.FixFormDigFillItemType Type { get; set; }

    public decimal Result { get; set; }
}

