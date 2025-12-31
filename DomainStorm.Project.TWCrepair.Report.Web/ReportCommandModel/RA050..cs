using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA050
{
    public static class V1
    {
        /// <summary>
        /// 檢漏系統-年度計畫-系統成果報告書-二.檢修漏成果計算統計表
        /// </summary>
        public class QueryRA050 : IQuery
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
