using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA009
{
    public static class V1
    {
        /// <summary>
        /// 預算書-詳細表
        /// </summary>
        public class QueryRA009 : IQuery
        {
            public Guid Id { get; set; }
            public IConvert.Extension Extension { get; set; }
        }
    }
}
