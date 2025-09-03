using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA040
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表十一、隊員目標
        /// </summary>
        public class QueryRA040 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
           
        }
    }
}
