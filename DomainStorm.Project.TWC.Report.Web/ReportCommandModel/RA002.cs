using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWC.Report.Web.ReportCommandModel
{
    public static class RA002
    {
        public static class V1
        {
            public class QueryRA002 : IQuery
            {
                public int Year { get; set; }
                public IConvert.Extension Extension { get; set; }
                public List<Guid> DepartmentIds { get; set; }
            }
        }
    }
}
