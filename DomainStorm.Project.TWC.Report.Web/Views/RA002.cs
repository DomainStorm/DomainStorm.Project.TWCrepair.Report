using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;

namespace DomainStorm.Project.TWC.Report.Web.Views
{
    public class RA002 : ReportDataModel
    {
        public int Year { get; set; }

        public ICollection<RA002_Item> Items { get; set; }
        public RA002()
        {
            Items = new List<RA002_Item>();
        }
    }

    public class RA002_Item
    {
        public string AnotherCode { get; set; }
        public string Name { get; set; }
        public int C1 { get; set; }
        public int C2 { get; set; }
        public int C3 { get; set; }
        public int C4 { get; set; }
        public int C5 { get; set; }
        public int C6 { get; set; }
        public int C7 { get; set; }
        public int C8 { get; set; }
        public int C9 { get; set; }
        public int C10 { get; set; }
        public int C11 { get; set; }
        public int C12 { get; set; }
        public int Total { get; set; }
    }
}
