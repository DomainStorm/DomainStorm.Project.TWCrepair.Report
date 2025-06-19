using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA002
{
    public static class V1
    {
        /// <summary>
        /// 列印派工單-第一頁
        /// </summary>
        public class QueryRA002 : IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
