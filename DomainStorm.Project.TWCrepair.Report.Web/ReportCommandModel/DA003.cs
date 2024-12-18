using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel
{
    public static class DA003
    {
        public static class V1
        {
            public class QueryDA003 : IQuery
            {
                /// <summary>
                /// 水壓調查的Id
                /// </summary>
                public Guid Id { get; set; }
            }
        }
    }
}
