using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 預算書-封面
    /// </summary>
    public class RA006 : ReportDataModel
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
        /// 工程地點
        /// </summary>
        public string? EngineeringLocation { get; set; }

        /// <summary>
        /// 施工方法
        /// </summary>
        public string? EngineeringMethod { get; set; }

        /// <summary>
        /// 工程概要
        /// </summary>
        public string? EngineeringSummary { get; set; }

        /// <summary>
        /// 會計科目
        /// </summary>
        public string? AccountingAccount { get; set; }

        /// <summary>
        /// 預計開工日期
        /// </summary>
        public DateTime? PlanStartDate { get; set; }

        /// <summary>
        /// 預計完工日期
        /// </summary>
        public DateTime? PlanEndDate { get; set; }


        /// <summary>
        /// 工程費用
        /// </summary>
        public decimal? EngineeringPrice { get; set; } = 0;

        /// <summary>
        /// 材料費用
        /// </summary>
        public decimal? MaterialPrice { get; set; } = 0;

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

        
        /// <summary>
        /// 設計圖張數
        /// </summary>
        public int? DesignDrawingAmount { get; set; }

        /// <summary>
        /// 說明書頁數
        /// </summary>
        public int? ManualAmount { get; set; }

        /// <summary>
        /// 計算書頁數
        /// </summary>
        public int? CalculationBookAmount { get; set; }

        /// <summary>
        /// 詳細表頁數
        /// </summary>
        public int? DetailTableAmount { get; set; }

        /// <summary>
        /// 單價分析表頁數
        /// </summary>
        public int? UnitPriceAmount { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string? Notes { get; set; }
    }
}

    
