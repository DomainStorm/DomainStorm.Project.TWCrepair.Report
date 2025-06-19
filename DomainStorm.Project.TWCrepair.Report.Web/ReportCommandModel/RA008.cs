using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA008
{
    public static class V1
    {
        /// <summary>
        /// 預算書-進度
        /// </summary>
        public class QueryRA008 : IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
