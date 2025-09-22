using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA045
{
    public static class V1
    {
        /// <summary>
        /// 工作日報表-天數檢核
        /// </summary>
        public class QueryRA045 :  IQuery
        {
            public FileExtension Extension { get; set; }

            /// <summary>
            /// 區處代碼
            /// </summary>
            public Guid DepartmentId { get; set; }

            /// <summary>
            /// 年份
            /// </summary>
            public int Year { get; set; }
            /// <summary>
            /// 月份
            /// </summary>

            public int Month { get; set; }  
        }
    }
}
