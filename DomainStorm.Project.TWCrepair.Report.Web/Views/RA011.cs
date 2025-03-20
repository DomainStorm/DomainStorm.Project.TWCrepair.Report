using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 預算書-資源統計表
    public class RA011 : ReportDataModel
    {
        /// <summary>
        /// 預算書所屬區處名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 預算書所屬廠所名稱
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 工程名稱
        /// </summary>
        public string EngineeringName { get; set; }

        public List<BudgetDocResourceStatisticsItem> BudgetDocResourceStatisticsItems { get; set; } = new List<BudgetDocResourceStatisticsItem>();
    }
}


