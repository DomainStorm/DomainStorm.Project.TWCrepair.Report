using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 發包-詳細表(估價單)
    /// </summary>
    public class RA013 : ReportDataModel
    {
        public BudgetDocOutSourceDetail BudgetDocOutSourceDetail { get; set; }
    }
}


