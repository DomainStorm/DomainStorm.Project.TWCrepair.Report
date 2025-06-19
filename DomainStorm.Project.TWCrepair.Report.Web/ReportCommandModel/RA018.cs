using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA018
{
    public static class V1
    {
        /// <summary>
        /// 合約-資源統計表
        /// </summary>
        public class QueryRA018 : IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
