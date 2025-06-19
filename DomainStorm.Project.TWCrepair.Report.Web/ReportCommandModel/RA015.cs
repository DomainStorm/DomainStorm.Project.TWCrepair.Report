using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA015
{
    public static class V1
    {
        /// <summary>
        /// 發包-資源統計表
        /// </summary>
        public class QueryRA015 : IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
