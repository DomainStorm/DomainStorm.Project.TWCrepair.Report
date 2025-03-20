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
        /// 合約所屬區處名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 合約所屬廠所名稱
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 工程名稱
        /// </summary>
        public string EngineeringName { get; set; }

        public List<BudgetDocContractUnitPrice> BudgetDocContractUnitPrices { get; set; }
    }
}


