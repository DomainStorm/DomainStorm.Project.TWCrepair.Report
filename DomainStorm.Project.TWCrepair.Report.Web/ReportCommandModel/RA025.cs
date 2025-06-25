using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Repository.CommandModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA025
{
    public static class V1
    {
        /// <summary>
        /// 個案支援（31表）件數統計
        /// </summary>
        public class QueryRA025 : ReportDateRange , IQuery
        {
            public FileExtension Extension { get; set; }
        }
    }
}
