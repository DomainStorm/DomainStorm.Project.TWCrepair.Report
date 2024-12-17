using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

public class DA004 : ReportDataModel
{
    public PlotlyJson PlotlyJson { get; set; } = new()
    {
        Data = new List<Datum>
         {
            new BarDatum
             {
                  
                    X = new List<string>(),
                    Y= new List<string>()
                    {
                        "最高點水壓",
                        "最高點平均水壓",
                        "平均點平均水壓",
                        "最低點平均水壓",
                        "最低點水壓",
                    },

                    Name = "檢修後水壓",
                    Marker = new Marker   //不成功,會在 Deserialize 過程中遺失
                    {
                        Color= "rgb(229,57,53)"
                    },
                    Orientation = "h" ,
                    Text = new List<string>(),
                    TextPosition = "outside"

                },
                new BarDatum
                {

                    X = new List<string>(),
                    Y= new List<string>()
                    {
                        "最高點水壓",
                        "最高點平均水壓",
                        "平均點平均水壓",
                        "最低點平均水壓",
                        "最低點水壓",
                    },
                    
                    Name = "檢修前水壓",
                    Marker = new Marker //不成功,會在 Deserialize 過程中遺失
                    {
                        Color= "rgb(101,31,255)"
                    },
                    Orientation = "h",
                    Text = new List<string>(),
                    TextPosition = "outside"
                 }
                
            },
        Layout = new Layout
        {
            Title = "檢修漏作業水壓比較圖",
            Autosize = true,
            Margin = new Margin
            {
                L = 120
            },
            Xaxis = new Axis
            {
                GridColor = "black",
                GridWidth = 3
            },
            Yaxis = new Axis()
           
        }
    };

    

    
}


