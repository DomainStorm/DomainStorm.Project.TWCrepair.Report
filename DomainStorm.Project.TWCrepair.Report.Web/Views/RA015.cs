using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 發包-資源統計表
    public class RA015 : ReportDataModel
    {
        /// <summary>
        /// 發包所屬單位名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 工程名稱
        /// </summary>
        public string EngineeringName { get; set; }

        public List<BudgetDocOutSourceResourceStatisticsItem> BudgetDocOutSourceResourceStatisticsItems { get; set; } = new List<BudgetDocOutSourceResourceStatisticsItem>();
    }
}


