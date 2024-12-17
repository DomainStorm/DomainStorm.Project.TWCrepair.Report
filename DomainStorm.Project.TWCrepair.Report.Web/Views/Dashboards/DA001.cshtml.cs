using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;
using DomainStorm.Framework.BlazorComponent.ViewModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;



public class DA001 : ReportDataModel
{
    public PlotlyJson PlotlyJson { get; set; } = new()
    {
        Data = new List<Datum>
            {
                new()
                {
                    Header = new TableCell(),
                    Cells = new TableCell(),
                    Type = "table",
                    Name = "Series 1"
                }
            },
        Layout = new Layout
        {
            Title = "工作日報表未輸入一覽表(單位/人)",
            Autosize = true,
            Margin = new Margin(),
            Xaxis = new Axis(),
            Yaxis = new Axis()
        }
    };

    public List<DateTime> dates { get; set; } = new List<DateTime>();
    public List<DA001_Item> Items { get; set; } = new List<DA001_Item>();

    public void LoadDepartments(Department department, int level = 0)
    {
        if (level > 0)  //第 0 層是虛擬單位(只是個容器)
        {
            Items.Add(new DA001_Item(department));
        }
        if (department.Departments != null)
        {
            foreach (var child in department.Departments)
            {
                LoadDepartments(child, level + 1);
            }
        }
    }
}

public class DA001_Item
{
    public string Name { get; set; }
    internal Guid DepartmentId { get; set; }
    public int PostsCount { get; set; }
    internal List<Guid> PostIds { get; set; } = new List<Guid>();

    public DA001_Item(Department department)
    {
        Name = department.Name;
        DepartmentId = department.DepartmentId;
    }
}

