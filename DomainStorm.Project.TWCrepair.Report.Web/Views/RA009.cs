using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 預算書-詳細表
    public class RA009 : ReportDataModel
    {
        public BudgetDocDetail BudgetDocDetail { get; set; }
    }
}


