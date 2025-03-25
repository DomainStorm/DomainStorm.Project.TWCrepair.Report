using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 發包-單價分析表
    /// </summary>
    public class RA014 : ReportDataModel
    {
        /// <summary>
        /// 所屬區處名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 所屬廠所名稱
        /// </summary>
        public string SiteName { get; set; }


        /// <summary>
        /// 工程名稱
        /// </summary>
        public string EngineeringName { get; set; }

        public List<BudgetDocOutSourceUnitPrice> BudgetDocOutSourceUnitPrices { get; set; }
    }
}


