using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 合約-詳細表
    /// </summary>
    public class RA016 : ReportDataModel
    {
        public BudgetDocContractDetail BudgetDocContractDetail { get; set; }
    }
}


