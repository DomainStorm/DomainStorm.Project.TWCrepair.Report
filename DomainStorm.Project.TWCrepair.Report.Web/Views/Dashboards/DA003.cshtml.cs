using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

public class DA003 : ReportDataModel
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
            Xaxis = new Xaxis(),
            Yaxis = new Yaxis
            {
                Title ="壓力：kg/cm2"
            }
        }
    };

    

    
}


