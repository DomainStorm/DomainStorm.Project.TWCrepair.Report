using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel
{
    /// <summary>
    /// 檢修漏作業水壓比較圖
    /// </summary>
    public static class DA004
    {
        public static class V1
        {

            public class QueryDA004 :  RA001.V1.QueryRA001, IQuery  //和 RA001 是同一個要求,產生多個圖表
            {
                
            }
        }
    }
}
