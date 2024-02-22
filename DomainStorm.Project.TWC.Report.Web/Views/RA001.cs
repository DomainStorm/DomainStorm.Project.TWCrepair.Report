using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;

namespace DomainStorm.Project.TWC.Report.Web.Views
{
    public class RA001 : ReportDataModel
    {
        public DateTime ApplyDateBegin { get; set; }
        public DateTime ApplyDateEnd { get; set; }
        public ICollection<RA001_Item> Items { get; set; }
        public RA001()
        {
            Items = new List<RA001_Item>();
        }
    }

    public class RA001_Item
    {
        public string AnotherCode { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
