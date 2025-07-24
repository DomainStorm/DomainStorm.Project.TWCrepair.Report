using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA033
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表三、檢漏作業各系統費用分析表
        /// </summary>
        public class QueryRA033 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
