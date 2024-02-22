using DomainStorm.Framework;

namespace DomainStorm.Project.TWC.Report.Web.ReportCommandModel
{
    public static class Report
    {
        public static class V1
        {
            public class ReportDataModel
            {
                /// <summary>
                /// 列印者姓名
                /// </summary>
                public string UserName { get; set; }

                /// <summary>
                /// 列印時間
                /// </summary>
                public DateTime PrintDate { get; set; }

                /// <summary>
                /// 列印單位名稱 (選擇的單位,非使用者單位)
                /// </summary>
                public string DepartmentName { get; set; }

                /// <summary>
                /// 用在清單類的項目,可以自動登入別的系統的 token
                /// </summary>
                public string Token { get; set; }

                public ReportDataModel()
                {
                    PrintDate = DateTime.Now;
                }
            }

            public class ReportConvertRequest
            {
                public string ViewName { get; set; }
                public IConvert.Extension Extension { get; set; }
                public ReportDataModel Model { get; set; }
            }
        }
    }
}
