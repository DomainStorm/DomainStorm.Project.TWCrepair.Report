using System.Text;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 管線漏水密度及修理費用
    /// </summary>
    public class RA024 : ReportDataModel
    {
        /// <summary>
        /// 區處名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 廠所名稱
        /// </summary>
        public string SiteName { get; set; }


        public string DateRange { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }


        public List<RA024DiameterFilter> DiameterFilters { get; set; }

        private List<RA024KindFilter> KindFilters { get; set; }


        /// <summary>
        /// 非挖損追償案件,依管種統計的結果 (含合計列)
        /// </summary>
        public List<RA024Item> RA024Items { get; set; } = new List<RA024Item>();


        /// <summary>
        /// 漏水密度
        /// </summary>
        public Dictionary<string, double> LeakageDensity = new Dictionary<string, double>();


        public string LeakageDensityHtml(string style)
        {
            List<string> values = new List<string>();
            foreach (var dia in DiameterFilters)
            {
                values.Add(LeakageDensity[dia.Title].ToString());
            }
            return RA024Item.Html(style, values);
        }

        /// <summary>
        /// 平均費用
        /// </summary>
        public Dictionary<string, double> AverageCost= new Dictionary<string, double>();


        public string AverageCostHtml(string style)
        {
            List<string> values = new List<string>();
            foreach (var dia in DiameterFilters)
            {
                values.Add(AverageCost[dia.Title].ToString());
            }
            return RA024Item.Html(style, values);
        }


        /// <summary>
        /// 挖損追償案件
        /// </summary>
        public RA024Item ReimburseItem { get; set; } = new RA024Item();




      

        public RA024()
        {
            DiameterFilters = new List<RA024DiameterFilter>
            {
                new RA024DiameterFilter
                {
                        Title = "50mm以下管種及用戶外線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum <= 50 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "65~80mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >= 65 && x.PipeDiameterNum <= 90  && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "100mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >=100 && x.PipeDiameterNum <= 110  && x.PipeKind != "接合管鞍" )
                },
                new RA024DiameterFilter
                {
                        Title = "125~150mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >=125 && x.PipeDiameterNum <= 180  && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "200mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >= 200 && x.PipeDiameterNum <= 225  && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "250~300mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >= 250 && x.PipeDiameterNum <= 300 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "350~400mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >= 350 && x.PipeDiameterNum <= 400 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "450~500mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >= 450 && x.PipeDiameterNum <= 500 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "600mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum == 600 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "700mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum == 700 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "800mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum == 800 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "900mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum == 900 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "1000mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum == 1000 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "1100~1200mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >= 1100 && x.PipeDiameterNum <= 1200 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "1350~1500mm管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >= 1350 && x.PipeDiameterNum <= 1500 && x.PipeKind != "接合管鞍")
                },
                new RA024DiameterFilter
                {
                        Title = "1750mm以上管線修理",
                        func = new Func<RA024BaseData, bool> (x =>  x.PipeDiameterNum >= 1750 && x.PipeKind != "接合管鞍")
                },
            };


            KindFilters = new List<RA024KindFilter>
            {
                new RA024KindFilter
                {
                    Title = "DIP 延性鑄鐵管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "DIP")
                },
                new RA024KindFilter
                {
                    Title = "CIP 鑄鐵管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "CIP")
                },
                new RA024KindFilter
                {
                    Title = "SP 鋼管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "SP")
                },
                new RA024KindFilter
                {
                    Title = "SSP 不銹鋼管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "SSP")
                },
                new RA024KindFilter
                {
                    Title = "PSCP 預力混凝土管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "PSCP")
                },
                new RA024KindFilter
                {
                    Title = "PCCP 鋼襯預力混凝土管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "PCCP")
                },
                new RA024KindFilter
                {
                    Title = "FRP 玻璃纖維管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "FRP")
                },
                new RA024KindFilter
                {
                    Title = "PVCP 塑膠管",
                   func = new Func<RA024BaseData, bool>(x => x.PipeKind == "PVCP")
                },
                new RA024KindFilter
                {
                    Title = "PVCP/PE  塑膠管內襯PE管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "PVCP/PE")
                },
                new RA024KindFilter
                {
                    Title = "ABSP ABS塑鋼管",
                   func = new Func<RA024BaseData, bool>(x => x.PipeKind == "ABSP")
                },
                new RA024KindFilter
                {
                    Title = "HDPEP 高密度聚乙烯管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "HDPEP")
                },
                new RA024KindFilter
                {
                    Title = "HIWP 耐衝擊塑膠管",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind == "HIWP")
                },
                new RA024KindFilter
                {
                    Title = "其他管種(含已停用 管種RCP、PBP、 ACP及鉛管等",
                    func = new Func<RA024BaseData, bool>(x => x.PipeKind != "DIP" && x.PipeKind != "CIP" && x.PipeKind != "SP"
                    && x.PipeKind != "SSP" && x.PipeKind != "PSCP" && x.PipeKind != "PCCP"
                    && x.PipeKind != "FRP" && x.PipeKind != "PVCP" && x.PipeKind != "PVCP/PE" 
                    && x.PipeKind != "ABSP" && x.PipeKind != "HDPEP" && x.PipeKind != "HIWP"
                    )
                },

            };
        }

        /// <summary>
        /// 產生報表資料
        /// </summary>
        public void GenerateData(ICollection<RA024FixForm> allForms, ICollection<RA024ImportPipe> allPipes)
        {
            var notReimburseCases = allForms.Where(x => !x.IsReimburseCase );

            //非挖損追償案件產生各管種的資料
            foreach (var kind in KindFilters)
            {
                //篩選出符合管種的資料
                var kindFixForms = notReimburseCases.Where(kind.func);
                var kindPipes = allPipes.Where(kind.func);

                var item = new RA024Item
                {
                    Title = kind.Title,
                    RA024 = this
                };

                foreach(var dia in DiameterFilters)
                {
                    //在符合管種的資料裡, 再篩選符合的管徑資料
                    var diaFixForms = kindFixForms.Where(dia.func);
                    var diaPipes = kindPipes.Where(dia.func);

                    item.Length.Add(dia.Title, diaPipes.Where(dia.func).Sum(x => x.Length));
                    item.CaseAmount.Add(dia.Title, diaFixForms.Count());
                    item.Cost.Add(dia.Title, diaFixForms.Sum(x => (int)(x.FinalCost_Total ?? 0)));
                }

                RA024Items.Add(item);
            }

            //產生合計
            var totalItem = new RA024Item
            {
                Title = "合計",
                RA024 = this
            };
            var allLength = RA024Items.SelectMany(x => x.Length);
            var allCaseAmount = RA024Items.SelectMany(x => x.CaseAmount);
            var allCost = RA024Items.SelectMany(x => x.Cost);
            foreach (var dia in DiameterFilters)
            {
                totalItem.Length.Add(dia.Title, allLength.Where(x => x.Key == dia.Title).Sum(x => x.Value));
                totalItem.CaseAmount.Add(dia.Title, allCaseAmount.Where(x => x.Key == dia.Title).Sum(x => x.Value));
                totalItem.Cost.Add(dia.Title, allCost.Where(x => x.Key == dia.Title).Sum(x => x.Value));
            }
            RA024Items.Add(totalItem);



            //漏水密度及平均費用
            var totalItem2 = RA024Items.Last();
            foreach (var dia in DiameterFilters)
            {
                double density = 0, cost = 0;
                if (totalItem2.Length[dia.Title] != 0)
                {
                    density = Math.Round(1000.0 * totalItem2.CaseAmount[dia.Title] / totalItem2.Length[dia.Title] , 4);
                    cost = Math.Round( 1000.0 * totalItem2.Cost[dia.Title] / totalItem2.Length[dia.Title] , 2);
                }
                LeakageDensity.Add(dia.Title, density);
                AverageCost.Add(dia.Title, cost);
            }

            //挖損追償案件
            ReimburseItem.RA024 = this;
            var reimburseCases = allForms.Where(x => x.IsReimburseCase );
            foreach (var dia in DiameterFilters)
            {
                //在符合管種的資料裡, 再篩選符合的管徑資料
                var diaFixForms = reimburseCases.Where(dia.func);
                ReimburseItem.CaseAmount.Add(dia.Title, diaFixForms.Count());
                ReimburseItem.Cost.Add(dia.Title, diaFixForms.Sum(x => (int)(x.FinalCost_Total ?? 0)));
            }
        }
    }

    /// <summary>
    /// 管線統計資料
    /// </summary>
    public class RA024Item
    {
        public string Title { get; set; }

        public RA024 RA024 { get; set; }


        /// <summary>
        /// 長度
        /// </summary>
        public Dictionary<string, int> Length { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// 件費
        /// </summary>
        public Dictionary<string, int> CaseAmount { get; set; } = new Dictionary<string, int>();


        /// <summary>
        /// 費用
        /// </summary>

        public Dictionary<string, int> Cost { get; set; } = new Dictionary<string, int>();


        public int TotalLength
        {
            get => Length.Values.Sum();
        }

        public int TotalCaseAmount
        {
            get => CaseAmount.Values.Sum();
        }

        public int TotalCost
        {
            get => Cost.Values.Sum();
        }

        public double LeakageDensity
        {
            get
            {
                if (TotalLength == 0)
                    return 0;
                else
                {
                    return Math.Round(1000.0 * TotalCaseAmount / TotalLength , 4);
                }
            }
        }

        public double AverageCost
        {
            get
            {
                if (TotalLength == 0)
                    return 0;
                else
                {
                    return Math.Round(1000.0 * TotalCost / TotalLength , 2);
                }
            }
        }

        public string LengthHtml(string style)
        {
            List<string> values = new List<string>();
            foreach (var dia in RA024.DiameterFilters)
            {
                values.Add(Length[dia.Title].ToString());
            }
            return Html(style, values);
        }

        public string CaseAmountHtml(string style)
        {
            List<string> values = new List<string>();
            foreach (var dia in RA024.DiameterFilters)
            {
                values.Add(CaseAmount[dia.Title].ToString());
            }
            return Html(style, values);
        }

        public string CostHtml(string style)
        {
            List<string> values = new List<string>();
            foreach (var dia in RA024.DiameterFilters)
            {
                values.Add(Cost[dia.Title].ToString());
            }
            return Html(style, values);
        }


        //來源 Dictionary 可能是 int, double , 都轉成 string
        public static string Html(string style, List<string> values)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var value in values)
            {
                sb.AppendLine(@$"<table:table-cell table:style-name=""{style}"">
                            <text:p>{value}</text:p>
                        </table:table-cell>");
            }
            return sb.ToString();
        }
    }


    public abstract class  RA024BaseData
    {
        /// <summary>
        /// 管種
        /// </summary>
        public string PipeKind { get; set; }


        /// <summary>
        ///管徑
        /// </summary>
        public string PipeDiameter { get; set; }

        public int PipeDiameterNum
        {
            get
            {
                int result = 0;
                int.TryParse(PipeDiameter, out result);
                return result;
            }
        }

        /// <summary>
        /// 各項費用_總計
        /// </summary>
        public decimal? FinalCost_Total { get; set; }

        /// <summary>
        /// 長度
        /// </summary>
        public int Length { get; set; }
    }

    /// <summary>
    /// 財產系統匯入的管線
    /// </summary>
    public class RA024ImportPipe : RA024BaseData
    {
        
    }

    public class RA024FixForm : RA024BaseData
    {
        public Guid FormId { get; set; }


        
        

        /// <summary>
        /// 挖損追償案件
        /// </summary>
        public bool IsReimburseCase { get; set; }



    }

    /// <summary>
    /// 管徑 filter 以用迴圈產生資料
    /// </summary>
    public class RA024DiameterFilter
    {

        public string Title { get; set; }
        public Func<RA024BaseData, bool> func { get; set; }
    }

    /// <summary>
    /// 管種 filter 以用迴圈產生資料
    /// </summary>
    public class RA024KindFilter
    {

        public string Title { get; set; }
        public Func<RA024BaseData, bool> func { get; set; }
    }




}
