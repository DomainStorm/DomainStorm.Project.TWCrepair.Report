using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;



namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 列印派工單-第四頁
    /// </summary>
    public class RA005 : ReportDataModel
    {
        /// <summary>
        /// 修漏案號
        /// </summary>
        public string FixCaseNo { get; set; }

        public bool HolidayCase { get; set; }

        public string Location { get; set; }

        
        public string Contractor { get; set; }

        public string StartTime { get; set; }

        public FixFormOutsourcingCost FixFormOutsourcingCost { get; set; }

    }
}

    
