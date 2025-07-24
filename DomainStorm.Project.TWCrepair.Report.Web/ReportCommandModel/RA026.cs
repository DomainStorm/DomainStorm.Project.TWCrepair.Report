using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA026
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-封面
        /// </summary>
        public class QueryRA026 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
