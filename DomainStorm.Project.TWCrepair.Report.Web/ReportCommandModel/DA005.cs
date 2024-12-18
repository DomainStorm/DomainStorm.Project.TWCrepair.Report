using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel
{
    /// <summary>
    /// 總水頭分布圖
    /// </summary>
    public static class DA005
    {
        public static class V1
        {

            public class QueryDA005 :  RA001.V1.QueryRA001, IQuery  //和 RA001 是同一個要求,產生多個圖表
            {
                
            }
        }
    }
}
