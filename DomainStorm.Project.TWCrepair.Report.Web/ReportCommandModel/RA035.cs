using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA035
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表五、系統檢修漏作業流程圖
        /// </summary>
        public class QueryRA035 :  IQuery
        {
            //public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
