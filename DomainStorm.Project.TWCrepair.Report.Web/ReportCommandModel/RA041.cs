using DomainStorm.Framework;
using DomainStorm.Framework.Services;


namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA041
{
    public static class V1
    {
        /// <summary>
        /// 流量分析-檢前總表/檢後總表
        /// </summary>
        public class QueryRA041 :  IQuery
        {
            public FileExtension Extension { get; set; }

            /// <summary>
            /// 檢修前或檢修後詞庫代碼
            /// </summary>
            public Guid BeforeOrAfterWordId { get; set; }

            /// <summary>
            /// 區處代碼
            /// </summary>
            public Guid DepartmentId { get; set; }

            ///// <summary>
            ///// 年份(量測日期為必要, 年份應該不用)
            ///// </summary>
            //public int? Year { get; set; }

            /// <summary>
            /// 廠所代碼
            /// </summary>
            public Guid? SiteId { get; set; }

            
            /// <summary>
            /// 供水系統代碼
            /// </summary>
            public Guid? WaterSupplySystemId { get; set; }


            /// <summary>
            /// 工作區代碼
            /// </summary>
            public Guid? WorkSpaceId { get; set; }


            /// <summary>
            /// 小區代碼
            /// </summary>
            public Guid? SmallRegionId { get; set; }

            /// <summary>
            /// 量測日期
            /// </summary>
            public DateTime MeasureDate { get; set; }

        }
    }
}
