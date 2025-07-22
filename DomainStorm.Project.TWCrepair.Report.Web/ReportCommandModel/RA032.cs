using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA032
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表二、檢漏工作計劃表
        /// </summary>
        public class QueryRA032 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
        }
    }
}
