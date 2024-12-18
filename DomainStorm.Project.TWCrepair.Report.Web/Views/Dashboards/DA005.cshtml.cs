using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

public class DA005 : ReportDataModel
{
    public PlotlyJson PlotlyJson { get; set; } = new()
    {
        Data = new List<Datum>
         {
            new Datum
             {
                    Type = "line",
                    X = new List<string>(),
                    Y= new List<string>(),
                    Name = "檢修後總水頭",
                },
                new Datum
                {
                    Type = "line",
                    X = new List<string>(),
                    Y= new List<string>(),
                    Name = "檢修前總水頭",
                 }
                
            },
        Layout = new Layout
        {
            Title = "總水頭分布圖",
            Autosize = true,
            Margin = new Margin(),
            Xaxis = new Axis(),
            Yaxis = new Axis()
        }
    };
}


