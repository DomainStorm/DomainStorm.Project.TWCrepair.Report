using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 工作日報表-天數檢核
/// </summary>
public class RA045 : ReportDataModel
{
    public List<RA045_TeamMemer> TeamMemers { get; set; } = new List<RA045_TeamMemer>();
    public List<RA045_Item> Items { get; set; } = new List<RA045_Item>();
}

public class RA045_TeamMemer
{
    public string Name { get; set; } 
    public Guid PostId { get; set;  }
}


public class RA045_Item
{
    public DateTime Date { get; set; }

    public List<decimal> TotalDays { get; set; } = new List<decimal> { };
    public bool isHoliday { get; set;  }
}

