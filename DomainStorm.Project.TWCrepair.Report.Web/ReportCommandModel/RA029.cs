using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA029
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-作業概要
        /// </summary>
        public class QueryRA029 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
