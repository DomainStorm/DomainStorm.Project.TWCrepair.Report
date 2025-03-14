using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA016
{
    public static class V1
    {
        /// <summary>
        /// 合約-詳細表
        /// </summary>
        public class QueryRA016 : IQuery
        {
            public Guid Id { get; set; }
            public IConvert.Extension Extension { get; set; }
        }
    }
}
