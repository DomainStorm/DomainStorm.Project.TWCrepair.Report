using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA017
{
    public static class V1
    {
        /// <summary>
        /// 合約-單價分析表
        /// </summary>
        public class QueryRA017 : IQuery
        {
            public Guid Id { get; set; }
            public IConvert.Extension Extension { get; set; }
        }
    }
}
