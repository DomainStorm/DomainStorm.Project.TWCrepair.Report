using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

public class DA002 : ReportDataModel
{
    public PlotlyJson PlotlyJson { get; set; } = new()
    {
        Data = new List<Datum>
            {
                new()
                {
                    Header = new TableCell
                    {
                        Values = new List<List<string>>
                        {
                            new List<string>{ "檢漏案號" },
                            new List<string>{ "修漏案號" },
                            new List<string>{ "異動日期" },
                            new List<string>{ "異動作業" },
                        }
                    },
                    Cells = new TableCell(),
                    Type = "table",
                    Name = "Series 1"
                }
            },
        Layout = new Layout
        {
            Title = "修漏資料變更一覽表",
            Autosize = true,
            Margin = new Margin(),
            Xaxis = new Axis(),
            Yaxis = new Axis()
        }
    };

    

    
}

public class DA002_Item
{
    public string CheckCaseNo { get; set; }
    public string FixCaseNo { get; set; }
    public DateTime ModifyTime { get; set; }
    public string ModifyNotes { get; set; }
}

