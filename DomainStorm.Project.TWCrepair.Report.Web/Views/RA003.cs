using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 列印派工單-第二頁
    /// </summary>
    public class RA003 : ReportDataModel
    {
        /// <summary>
        /// 修漏案號
        /// </summary>
        public string FixCaseNo { get; set; }

        public string Location { get; set; }

        public FixFormAudit FixFormAudit { get; set; }
    }
}

    
