using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA030
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-計劃經費
        /// </summary>
        public class QueryRA030 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
