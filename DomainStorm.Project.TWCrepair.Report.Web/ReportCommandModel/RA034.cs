using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA034
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表四、檢漏作業計劃差旅費分析表
        /// </summary>
        public class QueryRA034 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
