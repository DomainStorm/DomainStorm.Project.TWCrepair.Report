using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA044
{
    public static class V1
    {
        /// <summary>
        /// 區處執行管控表
        /// </summary>
        public class QueryRA044 :  IQuery
        {
            public FileExtension Extension { get; set; }

            /// <summary>
            /// 區處代碼
            /// </summary>
            public Guid DepartmentId { get; set; }

            /// <summary>
            /// 建議日期起
            /// </summary>
            public DateTime SuggestionDateBegin { get; set; }

            /// <summary>
            /// 建議日期迄
            /// </summary>
            public DateTime SuggestionDateEnd { get; set; }

            /// <summary>
            /// 總處解除列管 (null 表示查全部)
            /// </summary>
            public bool? Delisting { get; set; }
        }
    }
}
