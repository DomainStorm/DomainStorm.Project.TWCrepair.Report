using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA004
{
    public static class V1
    {
        /// <summary>
        /// 列印派工單-第三頁
        /// </summary>
        public class QueryRA004 : IQuery
        {
            public Guid Id { get; set; }
            public IConvert.Extension Extension { get; set; }
        }
    }
}
