using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

/// <summary>
/// 流量分析-(檢前總表/檢後總表)的流量曲線圖(結合在 RA041裡的圖)
/// </summary>
public class DA008 : ReportDataModel
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
            Title = "當日流量曲線圖",
            Autosize = true,
            Margin = new Margin(),
            Xaxis = new Axis(),
            Yaxis = new Axis
            {
                Title = "流量(cmd)"
            }
        }
    };
}


