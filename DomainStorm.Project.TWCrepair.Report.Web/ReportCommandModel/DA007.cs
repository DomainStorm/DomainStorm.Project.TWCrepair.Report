using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel
{
    /// <summary>
    /// 當日流量曲線圖
    /// </summary>
    public static class DA007
    {
        public static class V1
        {
            public class QueryDA007 : IQuery
            {
                /// <summary>
                /// 地點名稱
                /// </summary>
                public string Location { get; set; }

                /// <summary>
                /// 量測日期
                /// </summary>
                public DateTime MeasureDate { get; set; }

                /// <summary>
                /// 檢修前後詞庫代碼
                /// </summary>
                public Guid? BeforeOrAfterWordId { get; set; }
            }
        }
    }
}
