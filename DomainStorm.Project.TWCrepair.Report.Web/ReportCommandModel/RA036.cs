using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA036
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表六、各系統管徑、管長暨附屬設備統計表
        /// </summary>
        public class QueryRA036 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
