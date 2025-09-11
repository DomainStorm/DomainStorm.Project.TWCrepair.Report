using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA037
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表七、各系統大區NRW
        /// </summary>
        public class QueryRA037 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
