using DomainStorm.Report.BlazorComponent.ViewModel;

namespace DomainStorm.Project.TWC.Report.Web.InputModel;

public class RA002_InputModel : ReportSearchBase
{
    public RA002_InputModel()
    {
        InitializeAvailableYears();
        Clear();
    }
    /// <summary>
    /// 選擇的年份
    /// </summary>
    public int Year { get; set; }
    /// <summary>
    /// 取得可選年份列表
    /// </summary>
    public List<int> AvailableYears { get; } = new List<int>();
    private void InitializeAvailableYears()
    {
        int currentYear = DateTime.Now.Year;

        // 設定要保留的歷史年份數
        int numberOfPastYearsToKeep = 2;

        // 計算起始年份，根據當前日期和要保留的歷史年份數
        int startYear = currentYear - numberOfPastYearsToKeep;

        // 使用起始年份到結束年份的範圍，確保顯示包括當前年份在內的五個年份
        for (int i = startYear ; i <= startYear + 4; i++)
        {
            AvailableYears.Add(i);
        }
        //int currentYear = DateTime.Now.Year;
        //for (int i = 0; i < 5; i++)
        //{
        //    AvailableYears.Add(currentYear + i);
        //}
    }
    public override void Clear()
    {
        base.Clear();
        Year = DateTime.Now.Year; // 預設選擇當前年份
    }
}