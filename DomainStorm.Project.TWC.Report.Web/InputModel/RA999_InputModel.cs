using DomainStorm.Report.BlazorComponent.ViewModel;

namespace DomainStorm.Project.TWC.Report.Web.InputModel;

public class RA999_InputModel : ReportSearchBase
{
    public RA999_InputModel()
    {
        Clear();
    }

    /// <summary>
    ///     受理日期起
    /// </summary>
    public DateTime ApplyDateBegin { get; set; }

    /// <summary>
    ///     受理日期迄
    /// </summary>
    public DateTime ApplyDateEnd { get; set; }

    public override void Clear()
    {
        base.Clear();
        ApplyDateBegin = DateTime.Now;
        ApplyDateEnd = DateTime.Now.AddDays(7);
    }
}