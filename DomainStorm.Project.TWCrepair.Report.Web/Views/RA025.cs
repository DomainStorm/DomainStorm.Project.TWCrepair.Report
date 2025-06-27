using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 個案支援（31表）件數統計
/// </summary>
public class RA025 : ReportDataModel
{
    private string[] departmentNames = new string[] { 
        "第一區管理處", "第二區管理處", "第三區管理處", "第四區管理處", "第五區管理處", 
        "第六區管理處", "第七區管理處", "第八區管理處", "第九區管理處", "第十區管理處", 
        "第十一區管理處", "第十二區管理處", "第十三區管理處" }; 
    public DateTime BeginDate { get; set; }

    public DateTime EndDate { get; set; }


    public List<RA025Item> Items { get; set; } = new List<RA025Item>();

    public RA025()
    {
        foreach(var departmentName in  departmentNames)
        {
            Items.Add(new RA025Item { 
                DepartmentName = departmentName
            });
        }
    }

    public void Sum()
    {
        var sumItem = new RA025Item
        {
            AcceptAmount = Items.Sum(x => x.AcceptAmount),
            EstablishAmount = Items.Sum(x => x.EstablishAmount),
            NotEstablishAmount = Items.Sum(x => x.NotEstablishAmount),
            CloseAmount = Items.Sum(x => x.CloseAmount),

        };        Items.Add(sumItem);
    }
}

public class RA025Item
{
    public Guid DepartmentId { get; set; } = Guid.Empty;

    public string DepartmentName { get; set; }

    /// <summary>
    /// 接獲件
    /// </summary>
    public int AcceptAmount { get; set; } = 0;

    /// <summary>
    /// 成案件數
    /// </summary>
    public int EstablishAmount { get; set; } = 0;

    /// <summary>
    /// 未成案件數
    /// </summary>
    public int NotEstablishAmount { get; set; } = 0;

    /// <summary>
    /// 結案件數
    /// </summary>
    public int CloseAmount { get; set; } = 0;

    /// <summary>
    /// 成案率
    /// </summary>
    public double EstablishPercentage
    {
        get => Percentage(EstablishAmount, AcceptAmount);
    }

    /// <summary>
    /// 未成案率
    /// </summary>
    public double NotEstablisdPercentage
    {
        get => Percentage(NotEstablishAmount, AcceptAmount);
    }


    /// <summary>
    /// 結案率
    /// </summary>
    public double ClosePercentage
    {
        get => Percentage(CloseAmount, AcceptAmount);
    }

    /// <param name="numerator">分子</param>
    /// <param name="denominator">分母</param>
    /// <returns></returns>
    public static double Percentage(double numerator, double denominator)
    {
        if (denominator == 0)
        {
            return 0;
        }
        else
        {
            return Math.Round(100.0 * numerator / denominator, 2, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bottomBold">底線是否要加粗體</param>
    /// <returns></returns>
    public string Html(string style1To3, string style4, string style5To7)
    {
        return $@"<table:table-cell table:style-name=""{style1To3}"" office:value-type=""float"" office:value=""{AcceptAmount}"" calcext:value-type=""float"" />
        <table:table-cell table:style-name=""{style1To3}"" office:value-type=""float"" office:value=""{EstablishAmount}"" calcext:value-type=""float"" />
        <table:table-cell table:style-name=""{style1To3}"" office:value-type=""float"" office:value=""{NotEstablishAmount}"" calcext:value-type=""float"" />
        <table:table-cell table:style-name=""{style4}"" office:value-type=""float"" office:value=""{CloseAmount}"" calcext:value-type=""float"" />
        <table:table-cell table:style-name=""{style5To7}"" office:value-type=""float"" office:value=""{EstablishPercentage}"" calcext:value-type=""float"" />
        <table:table-cell table:style-name=""{style5To7}"" office:value-type=""float"" office:value=""{NotEstablishAmount}"" calcext:value-type=""float"" />
        <table:table-cell table:style-name=""{style5To7}"" office:value-type=""float"" office:value=""{ClosePercentage}"" calcext:value-type=""float"" />";
    }

}