using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Shared.CommandModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA022
{
    public static class V1
    {
        /// <summary>
        /// 修漏紀錄簿二
        /// </summary>
        public class QueryRA022 : ReportDateRange , IQuery
        {
            /// <summary>
            /// 區處代碼
            /// </summary>
            public Guid DepartmentId { get; set; }

            /// <summary>
            /// 廠所代碼
            /// </summary>
            public Guid? SiteId { get; set; }

            public FileExtension Extension { get; set; }
        }
    }
}
