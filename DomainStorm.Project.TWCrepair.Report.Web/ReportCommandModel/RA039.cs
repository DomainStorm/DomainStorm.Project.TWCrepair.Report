using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA039
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-附表十、儀器需求統計
        /// </summary>
        public class QueryRA039 :  IQuery
        {
            public Guid Id { get; set; }
            public FileExtension Extension { get; set; }
            /// <summary>
            /// 是否載入初始資料(報表輸出時應為 false ; editor 時為 true , 若無已儲存資料時, 會自動載入初始資料)
            /// </summary>
            public bool Initialize { get; set; } = false;
        }
    }
}
