using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA027
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-目錄
        /// </summary>
        public class QueryRA027 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
