using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA011
{
    public static class V1
    {
        /// <summary>
        /// 預算書-資源統計表
        /// </summary>
        public class QueryRA011 : IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
