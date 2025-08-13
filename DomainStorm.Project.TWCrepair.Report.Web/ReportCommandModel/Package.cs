using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class Package
{
    public static class V1
    {
        public class QueryPackage : IQuery
        {
            public Guid Id { get; set; }
            public IReadOnlyList<Guid> IdList { get; set; } = new List<Guid>();
            public FileExtension Extension { get; set; }
        }
    }
}
