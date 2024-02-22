using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWC.Report.Web.ReportCommandModel
{
    public static class RA001
    {
        public static class V1
        {
            public class QueryRA001 : IQuery
            {
                public DateTime ApplyDateBegin { get; set; }
                public DateTime ApplyDateEnd { get; set; }
                public IConvert.Extension Extension { get; set; }
                public List<Guid> DepartmentIds { get; set; }
            }
        }
    }
}
