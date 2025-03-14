using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 合約-單價分析表
    /// </summary>
    public class RA017 : ReportDataModel
    {
        /// <summary>
        /// 預算書所屬單位名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 工程名稱
        /// </summary>
        public string EngineeringName { get; set; }

        public List<BudgetDocContractUnitPrice> BudgetDocContractUnitPrices { get; set; }
    }
}


