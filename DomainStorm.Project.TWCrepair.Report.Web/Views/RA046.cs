using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 工作日報表-請假天數檢核
/// </summary>
public class RA046 : ReportDataModel
{
    public List<RA046_TeamMemer> TeamMemers { get; set; } = new List<RA046_TeamMemer>();
    public List<RA046_Item> Items { get; set; } = new List<RA046_Item>();
}

public class RA046_TeamMemer
{
    public string Name { get; set; } 
    public Guid PostId { get; set;  }
}


public class RA046_Item
{
    public DateTime Date { get; set; }

    public List<decimal> TotalDays { get; set; } = new List<decimal> { };
    public bool isHoliday { get; set;  }
}

