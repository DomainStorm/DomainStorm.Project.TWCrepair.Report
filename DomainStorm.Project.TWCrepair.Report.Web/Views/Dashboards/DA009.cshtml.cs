using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-四.最小流量率(修正值)比較表(結合在 RA052裡的圖)
/// </summary>
public class DA009 : ReportDataModel
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
						"修正後"
					},

					Name = "修正後",
					Marker = new Marker //不成功,會在 Deserialize 過程中遺失
                    {
						Color= "rgb(101,31,255)"
					},
					Orientation = "h",
					Text = new List<string>(),
					TextPosition = "outside"
				 },
				new BarDatum
				{

					X = new List<string>(),
					Y= new List<string>()
					{
						"修正前"
					},

					Name = "修正前",
					Marker = new Marker   //不成功,會在 Deserialize 過程中遺失
                    {
						Color= "rgb(229,57,53)"
					},
					Orientation = "h" ,
					Text = new List<string>(),
					TextPosition = "outside"

				}

			},
		Layout = new Layout
		{
			Title = "最小流量率(修正值)比較表",
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


