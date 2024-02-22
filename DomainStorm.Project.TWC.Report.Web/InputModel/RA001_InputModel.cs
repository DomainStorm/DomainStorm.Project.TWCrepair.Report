using DomainStorm.Report.BlazorComponent.ViewModel;

namespace DomainStorm.Project.TWC.Report.Web.InputModel;

public class RA001_InputModel : ReportSearchBase
{
    public RA001_InputModel()
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
        ApplyDateBegin = DateTime.Now.Date;
        //ApplyDateEnd = ApplyDateBegin.AddDays(7);
        ApplyDateEnd = ApplyDateBegin;
    }
}