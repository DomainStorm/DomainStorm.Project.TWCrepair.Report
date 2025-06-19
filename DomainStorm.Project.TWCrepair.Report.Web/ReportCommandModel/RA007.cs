using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA007
{
    public static class V1
    {
        /// <summary>
        /// 預算書-詳細總表
        /// </summary>
        public class QueryRA007 : IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
