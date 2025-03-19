using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 預算書-進度表
    public class RA008 : ReportDataModel
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
        /// 工程編號
        /// </summary>
        public string? EngineeringNumber { get; set; }

        /// <summary>
        /// 工程名稱
        /// </summary>
        public string EngineeringName { get; set; }

        /// <summary>
        /// 預計開工日期
        /// </summary>
        public DateTime? PlanStartDate { get; set; }

        /// <summary>
        /// 預計完工日期
        /// </summary>
        public DateTime? PlanEndDate { get; set; }

        /// <summary>
        /// 進度說明
        /// </summary>
        public string? Schedule { get; set; }

    }
}


