using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class DA011

{
    public static class V1
    {
		/// <summary>
		/// 檢漏系統-年度計畫-系統成果報告書-五.作業前後水壓比較表-總水頭分布圖(結合在 RA053裡的圖)
		/// </summary>
		public class QueryDA011 : IQuery
        {
            
            ///// <summary>
            ///// 年份(工作區代碼已經是唯一的,不需要)
            ///// </summary>
            //public int Year { get; set; }
            ///// <summary>
            ///// 區處代碼(工作區代碼已經是唯一的,不需要)
            ///// </summary>
            //public Guid DepartmentId { get; set; }

            /// <summary>
            /// 工作區代碼
            /// </summary>
            public Guid WorkSpaceId { get; set; }
        }
    }
}
