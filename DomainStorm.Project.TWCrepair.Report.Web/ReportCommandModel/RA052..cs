using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA052
{
    public static class V1
    {
		/// <summary>
		/// 檢漏系統-年度計畫-系統成果報告書-四.最小流量率(修正值)比較表
		/// </summary>
		public class QueryRA052 : IQuery
        {
            public FileExtension Extension { get; set; }

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
