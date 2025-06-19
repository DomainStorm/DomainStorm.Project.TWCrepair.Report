using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA013
{
    public static class V1
    {
        /// <summary>
        /// 發包-詳細表(估價單)
        /// </summary>
        public class QueryRA013 : IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
