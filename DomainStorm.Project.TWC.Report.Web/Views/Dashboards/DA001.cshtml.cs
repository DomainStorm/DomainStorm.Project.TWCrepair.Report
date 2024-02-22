using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;

namespace DomainStorm.Project.TWC.Report.Web.Views.Dashboards;

public class DA001 : ReportDataModel
{
    public PlotlyJson PlotlyJson { get; set; } = new()
    {
        Data = new List<Datum>
        {
            new()
            {
                X = new List<string>(),
                Y = new List<string>(),
                Type = "bar",
                Name = "Series 1"
            }
        },
        Layout = new Layout 
        { 
            Title = "區處前七天受理案件({ApplyDateBegin}~{ApplyDateEnd})", 
            Xaxis = new Xaxis { Title = "站所" }, 
            Yaxis = new Yaxis { Title = "件數" },
            Autosize = true,
            Margin = new Margin()
        }
    };
}