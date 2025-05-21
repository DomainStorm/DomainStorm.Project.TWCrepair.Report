using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA019
{
    public static class V1
    {
        /// <summary>
        /// 漏水情形管制月報表
        /// </summary>
        public class QueryRA019 : IQuery
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
            /// 年月份( "2025/01")
            /// </summary>
            public string? YearMonth { get; set; }

            /// <summary>
            /// 年份
            /// </summary>
            public int? Year { get; set; }

            /// <summary>
            /// 季 ( 第一季,第二季,第三季,第四季)
            /// </summary>
            public string? Season { get; set; }

           

            /// <summary>
            ///  年度 ( 上半年,下半年, 全年度)
            /// </summary>
            public string? Half { get; set; }
            


            /// <summary>
            /// 受理日期起
            /// </summary>
            public DateTime? AcceptanceDateBegin { get; set; }

            /// <summary>
            /// 受理日期迄
            /// </summary>
            public DateTime? AcceptanceDateEnd { get; set; }
            public IConvert.Extension Extension { get; set; }
        }

        
    }
}
