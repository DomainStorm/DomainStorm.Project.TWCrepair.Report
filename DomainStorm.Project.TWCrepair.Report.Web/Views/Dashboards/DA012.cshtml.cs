using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

public class DA012 : ReportDataModel
{
	public PlotlyJson PlotlyJson { get; set; } = new()
	{
		Data = new List<Datum>
		{
			new BarDatum
			 {

					X = new List<string>(),
					Y= new List<string>(),
					Name = "最小流量",
					Orientation = "v" ,
					Text = new List<string>(),
					TextPosition = "outside"
				},

		},
		Layout = new Layout
		{
			Title = "最小流量率(修正值)比較表",
			Autosize = true,
			Margin = new Margin(),
			Xaxis = new Axis(),
			Yaxis = new Axis()
		}
	};
}


