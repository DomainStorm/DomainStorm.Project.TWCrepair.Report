using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Repository.CommandModel;
using DomainStorm.Project.TWCrepair.Shared.CommandModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA020
{
    public static class V1
    {
        /// <summary>
        /// 漏水原因分析表
        /// </summary>
        public class QueryRA020 : ReportDateRange , IQuery
        {
            /// <summary>
            /// 區處代碼(未指定表示為總管理處查詢所有區處)
            /// </summary>
            public Guid? DepartmentId { get; set; }

            /// <summary>
            /// 廠所代碼
            /// </summary>
            public Guid? SiteId { get; set; }

            public FileExtension Extension { get; set; }
        }
    }
}
