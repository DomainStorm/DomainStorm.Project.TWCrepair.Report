namespace DomainStorm.Project.TWCrepair.Report.Web
{
    public class PlotlyJson
    {
        public List<Datum> Data { get; set; }
        public Layout Layout { get; set; }
    }

    public class Datum
    {
        public List<string> X { get; set; }
        public List<string> Y { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class Layout
    {
        public string Title { get; set; }
        public Xaxis Xaxis { get; set; }
        public Yaxis Yaxis { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public bool Autosize { get; set; }
        public Margin Margin { get; set; }
    }

    public class Xaxis
    {
        public string Title { get; set; }
    }

    public class Yaxis
    {
        public string Title { get; set; }
    }

    public class Margin
    {
        public int L { get; set; } = 80;
        public int R { get; set; } = 80;
        public int T { get; set; } = 100;
        public int B { get; set; } = 80;
    }
}
