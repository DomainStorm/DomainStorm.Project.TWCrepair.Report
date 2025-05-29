using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Shared.CommandModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA021
{
    public static class V1
    {
        /// <summary>
        /// 修漏紀錄簿
        /// </summary>
        public class QueryRA021 : ReportDateRange , IQuery
        {
            /// <summary>
            /// 區處代碼
            /// </summary>
            public Guid DepartmentId { get; set; }

            /// <summary>
            /// 廠所代碼
            /// </summary>
            public Guid? SiteId { get; set; }

            public IConvert.Extension Extension { get; set; }
        }
    }
}
