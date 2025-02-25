
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 預算書-詳細總表
    /// </summary>
    public class RA007 : ReportDataModel
    {
        /// <summary>
        /// 預算書所屬單位名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 預算書編號
        /// </summary>
        public string? SerialNumber { get; set; }

        
        /// <summary>
        /// 工程編號
        /// </summary>
        public string? EngineeringNumber { get; set; }

        /// <summary>
        /// 工程名稱
        /// </summary>
        public string EngineeringName { get; set; }

        /// <summary>
        /// 工程費用
        /// </summary>
        public decimal? EngineeringPrice { get; set; } = 0;

        /// <summary>
        /// 材料費用
        /// </summary>
        public string? MaterialPrice { get; set; }

        /// <summary>
        /// 小計
        /// </summary>
        public decimal? SubTotalPrice { get; set; } = 0;

        /// <summary>
        /// 營業稅
        /// </summary>
        public decimal? Tax { get; set; } = 0;

        /// <summary>
        /// 總計
        /// </summary>
        public decimal? TotalPrice { get; set; } = 0;

       

    }
}

    
