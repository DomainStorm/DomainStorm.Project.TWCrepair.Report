using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA028
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-工作計畫
        /// </summary>
        public class QueryRA028 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
