using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA010
{
    public static class V1
    {
        /// <summary>
        /// 預算書-單價分析表
        /// </summary>
        public class QueryRA010 : IQuery
        {
            public Guid Id { get; set; }
            public IConvert.Extension Extension { get; set; }
        }
    }
}
