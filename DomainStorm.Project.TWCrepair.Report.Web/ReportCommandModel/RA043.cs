using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA043
{
    public static class V1
    {
        /// <summary>
        /// 毀損計算營業損失
        /// </summary>
        public class QueryRA043 :  IQuery
        {
            public FileExtension Extension { get; set; }

            /// <summary>
            /// 修漏案件Id
            /// </summary>
            public Guid Id { get; set; }
        }
    }
}
