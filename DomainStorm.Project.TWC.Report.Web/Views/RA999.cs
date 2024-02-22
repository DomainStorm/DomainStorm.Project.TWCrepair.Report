using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;

namespace DomainStorm.Project.TWC.Report.Web.Views
{
    public class RA999 : ReportDataModel
    {
        public DateTime ApplyDateBegin { get; set; }
        public DateTime ApplyDateEnd { get; set; }
        public ICollection<RA999_Item> Items { get; set; }

        
        public RA999()
        {
            Items = new List<RA999_Item>();
        }
    }

    public class RA999_Item
    {
        public Guid FormId { get; set; }

        /// <summary>
        /// 受理號碼
        /// </summary>
        public string? ApplyCaseNo { get; set; }

        /// <summary>
        /// 受理日期
        /// </summary>
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// 站所
        /// </summary>
        public string OperatingArea { get; set; }

        /// <summary>
        /// 水號
        /// </summary>
        public string WaterNo { get; set; }

        /// <summary>
        /// 異動種類
        /// </summary>
        public string TypeChangeName { get; set; }


        /// <summary>
        /// 臨櫃人員姓名
        /// </summary>
       public string UserName { get; set; }

        /// <summary>
        /// 結案人員姓名
        /// </summary>
        public string? FinishUserName { get; set; }

        /// <summary>
        /// 結案日期
        /// </summary>
        public DateTime? FinishDate { get; set; }
    }
}
