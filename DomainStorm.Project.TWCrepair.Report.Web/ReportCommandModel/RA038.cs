using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA038
{
    public static class V1
    {
        /// <summary>
        /// 空白頁
        /// </summary>
        public class QueryRA038 :  IQuery
        {
            public FileExtension Extension { get; set; }

            /// <summary>
            /// 產出的工作表名稱
            /// </summary>
            public string SheetName { get; set; } = "工作表1";
        }
    }
}
