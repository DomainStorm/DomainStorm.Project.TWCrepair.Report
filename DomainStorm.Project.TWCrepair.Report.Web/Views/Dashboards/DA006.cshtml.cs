using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

public class DA006 : ReportDataModel
{
    public PlotlyJson PlotlyJson { get; set; } = new()
    {
        Data = new List<Datum>
            {
                new()
                {
                    Type = "line",
                    X = new List<string>(),
                    Y= new List<string>()
                }
            },
        Layout = new Layout
        {
            Title = "當日水壓曲線圖",
            Autosize = true,
            Margin = new Margin(),
            Xaxis = new Axis(),
            Yaxis = new Axis
            {
                Title = "壓力：kg/c㎡"
            }
        }
    };
}


