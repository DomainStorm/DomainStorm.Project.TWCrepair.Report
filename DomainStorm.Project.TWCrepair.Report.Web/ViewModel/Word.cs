using System.Text.Json.Serialization;

namespace DomainStorm.Project.TWCrepair.Report.Web.ViewModel;

/// <summary>
/// 詞庫
/// </summary>
public class Word
{
    public Guid Id { get; set; }

    /// <summary>
    /// 代碼
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 名稱
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 排序碼
    /// </summary>
    public short Sort { get; set; }

    /// <summary>
    /// 是否啟用
    /// </summary>
    public bool Enabled { get; set; }

    [JsonIgnore]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 詞庫擁有者 Id
    /// </summary>
    public Guid? OwnerId { get; set; }

    /// <summary>
    /// 詞庫擁有者名稱
    /// </summary>
    public string? OwnerName { get; set; }
}