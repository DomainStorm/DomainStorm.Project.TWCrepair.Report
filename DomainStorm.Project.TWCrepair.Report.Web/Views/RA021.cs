using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 修漏紀錄簿
    public class RA021 : ReportDataModel
    {
        /// <summary>
        /// 區處名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 廠所名稱
        /// </summary>
        public string SiteName { get; set; }


        public string DateRange { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<RA021Item> Items { get; set; }
    }

    public class RA021Item
    {
        /// <summary>
        /// 修漏案號
        /// </summary>
        public string? FixCaseNo { get; set; }

        /// <summary>
        /// 受理時間
        /// </summary>
        public DateTime? AcceptanceTime { get; set; }

        /// <summary>
        /// 報修位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 案件來源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 報修人行動電話
        /// </summary>
        public string? ReporterMobile { get; set; }

        /// <summary>
        /// 報修內容
        /// </summary>
		public string? FixDescription { get; set; }

        /// <summary>
        ///管徑
        /// </summary>
        public string PipeDiameter { get; set; }

        /// <summary>
        /// 修復時間
        /// </summary>
        public DateTime? FixTime { get; set; }

        /// <summary>
        /// 修復期限
        /// </summary>
        public DateTime? FixDeadline { get; set; }

        /// <summary>
        /// 逾期天數
        /// </summary>
        public int OverDueDays
        {
            get
            {
                if (FixDeadline.HasValue && FixTime.HasValue && FixTime.Value.Date > FixDeadline.Value.Date)
                {
                    return (int)(FixTime.Value.Date - FixDeadline.Value.Date).TotalDays;
                }
                else
                {
                    return 0;
                }
            }
        }


        /// <summary>
        /// 維修單位
        /// </summary>
        public string? FixUnit { get; set; }



        /// <summary>
        /// 案件緊急性
        /// </summary>
        public string? CaseEmergency { get; set; }


        /// <summary>
        /// 工作時間別
        /// </summary>
        public string? WorkTime { get; set; }


        /// <summary>
        /// 收費金額
        /// </summary>
        public int? ChargeAmount { get; set; }


        /// <summary>
        /// 派工的備忘
        /// </summary>
        public string DispatchNotes { get; set; }

        /// <summary>
        /// 移轉的備註
        /// </summary>
        public string TransferNotes { get; set; }

        /// <summary>
        /// 是否為移辦單取回
        /// </summary>
        public bool IsRetrieved { get; set; }

        /// <summary>
        /// (Final)備忘
        /// </summary>
        public string? Notes
        {
            get => $"{DispatchNotes} {TransferNotes} {(IsRetrieved ? "檢漏系統取回" : "") }";
        }
    }
}
