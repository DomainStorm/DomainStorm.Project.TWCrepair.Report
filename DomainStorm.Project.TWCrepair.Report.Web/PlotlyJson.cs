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
        public TableCell Header { get; set; }
        public TableCell Cells { get; set; }
        public string Orientation { get; set; }
        public List<string> Text { get; set; }
        public string TextPosition { get; set; } //  "inside" | "outside" | "auto" | "none" (outside 沒有成功)

    }

    public class BarDatum : Datum
    {
        public Marker Marker { get; set; }   //若把它加在 base Class Datum 中,會搞死曲線圖
        public BarDatum ()
        {
            base.Type = "bar";
        }
    }

    public class Marker
    {
        public string Color { get; set; }
    }

    public class TableCell
    {
        public List<List<string>> Values { get; set; } = new List<List<string>>();
    }
    

    public class Layout
    {
        public string Title { get; set; }
        public Axis Xaxis { get; set; }
        public Axis Yaxis { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public bool Autosize { get; set; }
        public Margin Margin { get; set; }
    }

    public class Axis
    {
        public string Title { get; set; }
        public string GridColor { get; set; }
        public int GridWidth { get; set; } = 1;
    }

    

    public class Margin
    {
        public int L { get; set; } = 80;
        public int R { get; set; } = 80;
        public int T { get; set; } = 100;
        public int B { get; set; } = 80;
    }
}
