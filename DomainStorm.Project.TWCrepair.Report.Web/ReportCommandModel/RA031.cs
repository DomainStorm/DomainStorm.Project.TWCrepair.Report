using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA031
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表一、抄見率暨戶日配水量明細表
        /// </summary>
        public class QueryRA031 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
