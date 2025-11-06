using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA026.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA040
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表十一、隊員目標
        /// </summary>
        public class QueryRA040 :IQuery
        {

            /// <summary>
            /// 區處代碼
            /// </summary>
            public Guid DepartmentId { get; set; }

            /// <summary>
            /// 年度
            /// </summary>
            public int Year { get; set; }

            public FileExtension Extension { get; set; }

        }
    }
}
