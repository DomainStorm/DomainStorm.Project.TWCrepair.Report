using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel
{
    public static class RA001
    {
        public static class V1
        {
            public class QueryRA001 : IQuery
            {
                /// <summary>
                /// 區處代碼
                /// </summary>
                public Guid DepartmentId { get; set; }

                /// <summary>
                /// 廠所代碼
                /// </summary>
                public Guid? SiteId { get; set; }

                /// <summary>
                /// 供水系統代碼
                /// </summary>
                public Guid WaterSupplySystemId { get; set; }

                /// <summary>
                /// 工作區代碼
                /// </summary>
                public Guid WorkSpaceId { get; set; }

                /// <summary>
                /// 小區代碼
                /// </summary>
                public Guid? SmallRegionId { get; set; }

                /// <summary>
                /// 
                /// </summary>
                public DateTime BeforeDate { get; set; }
                public DateTime AfterDate { get; set; }

                public IConvert.Extension Extension { get; set; }
            }

            /// <summary>
            /// 查詢目前單有上傳資料的檢修日期
            /// </summary>
            public class QueryRA001Date : IQuery 
            {
                /// <summary>
                /// 區處代碼
                /// </summary>
                public Guid DepartmentId { get; set; }

                /// <summary>
                /// 廠所代碼
                /// </summary>
                public Guid? SiteId { get; set; }

                /// <summary>
                /// 供水系統代碼
                /// </summary>
                public Guid WaterSupplySystemId { get; set; }

                /// <summary>
                /// 工作區代碼
                /// </summary>
                public Guid WorkSpaceId { get; set; }

                /// <summary>
                /// 小區代碼
                /// </summary>
                public Guid? SmallRegionId { get; set; }

                /// <summary>
                /// 年份(量測日期)
                /// </summary>
                public int Year { get; set; }

                /// <summary>
                /// 檢修前後詞庫代碼
                /// </summary>
                public Guid BeforeOrAfterWordId { get; set; }
            }

        }
    }
}
