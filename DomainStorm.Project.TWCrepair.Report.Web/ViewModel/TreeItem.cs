namespace DomainStorm.Project.TWCrepair.Report.Web.ViewModel;

public class TreeItem
{
    public string Label { get; set; }
    public string? Href { get; set; }
    public string? Icon { get; set; }
    public string? ExpandIcon { get; set; }
    public string? CollapseIcon { get; set; }
    public TreeItem[]? Children { get; set; }
}

