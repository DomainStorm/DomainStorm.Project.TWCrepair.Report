using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA005
{
    public static class V1
    {
        /// <summary>
        /// 列印派工單-第四頁
        /// </summary>
        public class QueryRA005 : IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
