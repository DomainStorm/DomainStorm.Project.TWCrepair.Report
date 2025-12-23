using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nest;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static DomainStorm.Framework.BlazorComponent.ViewModel.Table;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 管線修理統計表
    public class RA023 : ReportDataModel
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

        /// <summary>
        /// 分組統計結果
        /// </summary>
        public ICollection<RA023ItemGroup> Groups { get; set; } = new List<RA023ItemGroup>();

        /// <summary>
        /// 最後的總計
        /// </summary>
        public RA023Item TotalItem { get; set; }


        /// <summary>
        /// 線總長度(M)
        /// </summary>
        public double PipeLength { get; set; }


        /// <summary>
        /// 總用戶數
        /// </summary>
        public int CustomerAmount { get; set; }

        /// <summary>
        /// 管線每公里漏水件數
        /// </summary>
        public double CaseAmountPerKm
        {
            get
            {
                if (PipeLength == 0)
                    return 0;
                else
                    return Math.Round((double)TotalItem.TotalCase / PipeLength / 1000, 6, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// 表箱另件修理總件數
        /// </summary>
        private int BoxCaseAmount { get; set; }

        /// <summary>
        /// 用戶表箱每萬戶修漏件數
        /// </summary>
        public double BoxAmountPerCustomer
        {
            get
            {
                if (CustomerAmount == 0)
                    return 0;
                else
                    return Math.Round((double)BoxCaseAmount / CustomerAmount / 10000, 6, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// 抑制漏水流量(CMD)
        /// </summary>
        public double TotalLeakageWater { get; set; }




        private List<RA023FilterGroup> FilterGroups;


        public RA023()
        {
            FilterGroups = new List<RA023FilterGroup>
            {
                new RA023FilterGroup
                {
                    Kind = "管線",
                    Sum = true,
                    Filters = new List<RA023Filter>
                    {
                        new RA023Filter
                        {
                             Title = "50mm以下管種及用戶外線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum <= 50)
                        },
                        new RA023Filter
                        {
                             Title = "65~80mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >= 65 && x.PipeDiameterNum <= 90)
                        },
                        new RA023Filter
                        {
                             Title = "100mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >=100 && x.PipeDiameterNum <= 110)
                        },
                        new RA023Filter
                        {
                             Title = "125~150mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >=125 && x.PipeDiameterNum <= 180)
                        },
                        new RA023Filter
                        {
                             Title = "200mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >= 200 && x.PipeDiameterNum <= 225)
                        },
                        new RA023Filter
                        {
                             Title = "250~300mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >= 250 && x.PipeDiameterNum <= 300)
                        },
                        new RA023Filter
                        {
                             Title = "350~400mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >= 350 && x.PipeDiameterNum <= 400)
                        },
                        new RA023Filter
                        {
                             Title = "450~500mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >= 450 && x.PipeDiameterNum <= 500)
                        },
                        new RA023Filter
                        {
                             Title = "600mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum == 600)
                        },
                        new RA023Filter
                        {
                             Title = "700mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum == 700)
                        },
                        new RA023Filter
                        {
                             Title = "800mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum == 800)
                        },
                        new RA023Filter
                        {
                             Title = "900mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum == 900)
                        },
                        new RA023Filter
                        {
                             Title = "1000mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum == 1000)
                        },
                        new RA023Filter
                        {
                             Title = "1100~1200mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >= 1100 && x.PipeDiameterNum <= 1200)
                        },
                        new RA023Filter
                        {
                             Title = "1350~1500mm管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >= 1350 && x.PipeDiameterNum <= 1500)
                        },
                        new RA023Filter
                        {
                             Title = "1750mm以上管線修理",
                             func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "管線" && x.PipeDiameterNum >= 1750)
                        },
                    }
                },
                new RA023FilterGroup
                {
                    Kind = "附屬設備1",
                    Sum = true,
                    Filters = new List<RA023Filter>
                    {
                        new RA023Filter
                        {
                            Title = "制水閥修理",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "附屬設備" && x.AccessoryEquipment == "制水閥")
                        },
                        new RA023Filter
                        {
                            Title = "地上式消防栓修理",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "附屬設備" && x.AccessoryEquipment == "地上式消防栓")
                        },
                        new RA023Filter
                        {
                            Title = "地下式消防栓修理",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "附屬設備" && x.AccessoryEquipment == "地下式消防栓")
                        },
                        new RA023Filter
                        {
                            Title = "排氣閥修理",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "附屬設備" && x.AccessoryEquipment == "排氣閥修理")
                        },
                        new RA023Filter
                        {
                            Title = "其他閥類修理",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "附屬設備" && x.AccessoryEquipment == "其他閥類修理")
                        }
                    }
                },
                new RA023FilterGroup
                {
                    Kind = "附屬設備盒箱蓋",
                    Sum = true,
                    Filters = new List<RA023Filter>
                    {
                        new RA023Filter
                        {
                            Title = "制水閥盒修理或提升",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "附屬設備" && x.AccessoryEquipmentCover == "制水閥盒")
                        },
                        new RA023Filter
                        {
                            Title = "消防栓箱修理或提升",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "附屬設備" && x.AccessoryEquipmentCover == "消防栓箱")
                        },
                        new RA023Filter
                        {
                            Title = "窨井蓋修理或提升",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "附屬設備" && x.AccessoryEquipmentCover == "窨井蓋")
                        },
                        new RA023Filter
                        {
                            Title = "其他盒蓋修理或提升",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "附屬設備" && x.AccessoryEquipmentCover == "其他")
                        }
                    }
                },
                new RA023FilterGroup
                {
                    Kind = "綜合",
                    Sum = false,
                    Filters = new List<RA023Filter>
                    {
                        new RA023Filter
                        {
                            Title = "接合管鞍修理",
                            func = new Func<RA023FixForm, bool> (x => x.PipeKind == "接合管鞍" && x.CaseAttribute == "漏水案件")
                        },
                        new RA023Filter
                        {
                            Title = "用戶表箱另件修理",
                            func = new Func<RA023FixForm, bool> (x => x.EquipmentAttribute == "表箱另件" && x.CaseAttribute == "漏水案件")
                        },
                        new RA023Filter
                        {
                            Title = "無水及水壓異常處理",
                            func = new Func<RA023FixForm, bool> (x =>  x.CaseAttribute == "非漏水案件" && x.CaseAttributeNotLeackage == "無水處理")
                        },
                        new RA023Filter
                        {
                            Title = "水質混濁處理",
                            func = new Func<RA023FixForm, bool> (x =>  x.CaseAttribute == "非漏水案件" && x.CaseAttributeNotLeackage == "水質混濁")
                        },
                        new RA023Filter
                        {
                            Title = "配合遷移管線或設備",
                            func = new Func<RA023FixForm, bool> (x =>  x.CaseAttribute == "非漏水案件" && x.CaseAttributeNotLeackage == "配合遷移")
                        },
                        new RA023Filter
                        {
                            Title = "其他",
                            func = new Func<RA023FixForm, bool> (x =>  x.CaseAttribute == "非漏水案件" && x.CaseAttributeNotLeackage == "其他")
                        }
                    }
                }
            };
        }

        /// <summary>
        /// 產生報表資料
        /// </summary>
        public void GenerateData(ICollection<RA023FixForm> allForms)
        {
            BoxCaseAmount = allForms.Count(x => x.EquipmentAttribute == "表箱另件");
            TotalLeakageWater = allForms.Where(x => x.CaseAttribute == "漏水案件").Sum(x => (double)(x.DailyAmount ?? 0));

            foreach (var group in FilterGroups)
            {
                var resultGroup = new RA023ItemGroup();
                foreach(var filter in group.Filters)
                {
                    var resultItem = filter.GenerateItem(allForms);
                    resultGroup.Items.Add(resultItem);
                }
                if(group.Sum)
                {
                    resultGroup.Sum();
                }
                Groups.Add(resultGroup);
            }

            //拿每個群組原始的 items 作加總, 不要用該群組的小計(從範本看, 有的群組沒有小計)
            TotalItem = RA023Item.Sum(Groups.SelectMany(x => x.Items).ToArray(), "總計");
        }
    }

    public class RA023Item
    {
        public string Title { get; set; }

        //區分"管線", "附屬設備", "表箱另件"
        public string Kind { get; set; }
        /// <summary>
        /// 最小管徑(只用在"管線" 的資料)
        /// </summary>
        public int MinPipeDiameter { get; set; }

        /// <summary>
        /// 最大管徑(只用在"管線" 的資料)
        /// </summary>
        public int MaxPipeDiameter { get;set; }

        /// <summary>
        /// 總計數
        /// </summary>
        public int TotalCase { get; set; }


        /// <summary>
        /// 自修件數
        /// </summary>
        public int FixUnit_Self { get; set; }

        /// <summary>
        /// 委外件數
        /// </summary>
        public int FixUnit_OutSourcing { get; set; }


        /// <summary>
        /// 保固件數
        /// </summary>
        public int FixUnit_Warranty { get; set; }

        /// <summary>
        /// 其他件數
        /// </summary>
        public int FixUnit_Other { get; set; }


        /// <summary>
        /// 各項費用_委外施工費
        /// </summary>
        public decimal FinalCost_Outsourcing { get; set; }
        /// <summary>
        /// 各項費用_材料費
        /// </summary>
        public decimal FinalCost_Material { get; set; }
        /// <summary>
        /// 各項費用_路權代修費
        /// </summary>
        public decimal FinalCost_RoadRightProxy { get; set; }
        /// <summary>
        /// todo :各項費用_員工工資 (資料來源?)
        /// </summary>
        public decimal FinalCost_EmployeeSalary { get; set; }

        /// <summary>
        /// 各項費用_其他(保留空白)
        /// </summary>
        public decimal FinalCost_Other { get; set; }

        /// <summary>
        /// 各項費用_總計
        /// </summary>
        public decimal FinalCost_Total { get; set; }


        /// <summary>
        /// 各項費用_平均
        /// </summary>
        public decimal? FinalCost_Average 
        { 
            get
            {
                if (TotalCase == 0 )
                    return 0.00M;
                else
                {
                    return Math.Round(FinalCost_Total / TotalCase, 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// 漏水案件件數
        /// </summary>
        public int Leakage_Case { get; set; }

        /// <summary>
        /// 漏水案件修理時間(小時)
        /// </summary>
        public double Leakage_FixHours { get; set; }

        /// <summary>
        /// 漏水案件平均修理時間(小時)
        /// </summary>
        public double Leakage_FixAverageHours
        {
            get
            {
                if (Leakage_Case == 0)
                    return 0.00;
                else
                {
                    return Math.Round(Leakage_FixHours / Leakage_Case, 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// 漏水案件處理時間(小時)
        /// </summary>
        public double Leakage_ProcessHours { get; set; }

        /// <summary>
        /// 漏水案件處理時間(小時)
        /// </summary>
        public double Leakage_ProcessAverageHours
        {
            get
            {
                if (Leakage_Case == 0)
                    return 0.00;
                else
                {
                    return Math.Round(Leakage_ProcessHours / Leakage_Case, 2, MidpointRounding.AwayFromZero);
                }
            }
        }


        /// <summary>
        /// 非漏水案件件數
        /// </summary>
        public int NotLeakage_Case { get; set; }

        /// <summary>
        /// 非漏水案件修理時間(小時)
        /// </summary>
        public double NotLeakage_FixHours { get; set; }

        /// <summary>
        /// 非漏水案件平均修理時間(小時)
        /// </summary>
        public double NotLeakage_FixAverageHours
        {
            get
            {
                if (NotLeakage_Case == 0)
                    return 0.00;
                else
                {
                    return Math.Round(NotLeakage_FixHours / NotLeakage_Case, 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// 非漏水案件處理時間(小時)
        /// </summary>
        public double NotLeakage_ProcessHours { get; set; }

        /// <summary>
        /// 非漏水案件處理時間(小時)
        /// </summary>
        public double NotLeakage_ProcessAverageHours
        {
            get
            {
                if (NotLeakage_Case == 0)
                    return 0.00;
                else
                {
                    return Math.Round(NotLeakage_FixHours / NotLeakage_Case, 2, MidpointRounding.AwayFromZero);
                }
            }
        }


        public static RA023Item Sum(ICollection<RA023Item> items, string title)
        {
            var sumItem = new RA023Item();
            sumItem.Title = title;
            sumItem.TotalCase = items.Sum(x => x.TotalCase);
            sumItem.FixUnit_Self = items.Sum(x => x.FixUnit_Self);
            sumItem.FixUnit_OutSourcing = items.Sum(x => x.FixUnit_OutSourcing);
            sumItem.FixUnit_Warranty = items.Sum(x => x.FixUnit_Warranty);
            sumItem.FixUnit_Other = items.Sum(x => x.FixUnit_Other);
            sumItem.FinalCost_Outsourcing = items.Sum(x => x.FinalCost_Outsourcing);
            sumItem.FinalCost_Material = items.Sum(x => x.FinalCost_Material);
            sumItem.FinalCost_RoadRightProxy = items.Sum(x => x.FinalCost_RoadRightProxy);
            sumItem.FinalCost_EmployeeSalary = items.Sum(x => x.FinalCost_EmployeeSalary);
            sumItem.FinalCost_Total = items.Sum(x => x.FinalCost_Total);
            sumItem.Leakage_Case = items.Sum(x => x.Leakage_Case);
            sumItem.Leakage_FixHours = items.Sum(x => x.Leakage_FixHours);
            sumItem.Leakage_ProcessHours = items.Sum(x => x.Leakage_ProcessHours);
            sumItem.NotLeakage_Case = items.Sum(x => x.NotLeakage_Case);
            sumItem.NotLeakage_FixHours = items.Sum(x => x.NotLeakage_FixHours);
            sumItem.NotLeakage_ProcessHours = items.Sum(x => x.NotLeakage_ProcessHours);
            return sumItem;
        }


        public string Html(string rowStyle, string titlStyle, string normalStyle, string rightBoldStyle)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@$"
                <table:table-row table:style-name=""{rowStyle}"">
                            <table:table-cell table:style-name=""{titlStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{Title}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{TotalCase}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FixUnit_Self}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FixUnit_OutSourcing}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FixUnit_Warranty}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{rightBoldStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FixUnit_Other}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FinalCost_Outsourcing}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FinalCost_Material}</text:p>
                            </table:table-cell>
                            <table:table-cell  table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FinalCost_RoadRightProxy}</text:p>
                            </table:table-cell>
                            <table:table-cell  table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FinalCost_EmployeeSalary}</text:p>
                            </table:table-cell>
                            <table:table-cell  table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FinalCost_Other}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FinalCost_Total}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{rightBoldStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{FinalCost_Average}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{Leakage_Case}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{Leakage_FixHours}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{Leakage_FixAverageHours}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{Leakage_ProcessHours}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{rightBoldStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{Leakage_ProcessAverageHours}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{NotLeakage_Case}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{NotLeakage_FixHours}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{NotLeakage_FixAverageHours}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{normalStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{NotLeakage_ProcessHours}</text:p>
                            </table:table-cell>
                            <table:table-cell table:style-name=""{rightBoldStyle}"" office:value-type=""string"" calcext:value-type=""string"">
                                <text:p>{NotLeakage_ProcessAverageHours}</text:p>
                            </table:table-cell>
                            <table:table-cell table:number-columns-repeated=""16361"" />
                        </table:table-row>
            ");
            return sb.ToString();
        }

    }

    public class RA023ItemGroup
    {
        public string Kind { get; set; }
        public List<RA023Item> Items { get; set; } = new List<RA023Item>();

        public RA023Item SumItem { get; set; }

        public void Sum()
        {
            SumItem = RA023Item.Sum(Items, "小計");
        }
    }

    public class RA023FixForm
    {
        public Guid FormId { get; set; }


        /// <summary>
        /// 案件屬性 
        /// </summary>
        public string CaseAttribute { get; set; }

        // <summary>
        /// 案件屬性-非漏水子選項
        /// </summary>
        public string CaseAttributeNotLeackage { get; set; }

        /// <summary>
        /// 設備
        /// </summary>
        public string EquipmentAttribute { get; set; }


        /// <summary>
        /// 設備屬性其他子選項
        /// </summary>
        public string EquipmentAttributeOther { get; set; }


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
        /// 附屬設備
        /// </summary>
        public string AccessoryEquipment { get; set; }



        /// <summary>
        /// 附屬設備盒箱蓋
        /// </summary>
        public string AccessoryEquipmentCover { get; set; }



        /// <summary>
        /// 表箱另件
        /// </summary>
        public string BoxAnnex { get; set; }



        /// <summary>
        /// 維修單位
        /// </summary>
        public string FixUnit { get; set; }



        /// <summary>
        /// 各項費用_委外施工費
        /// </summary>
        public decimal? FinalCost_Outsourcing { get; set; }
        /// <summary>
        /// 各項費用_材料費
        /// </summary>
        public decimal? FinalCost_Material { get; set; }
        /// <summary>
        /// 各項費用_路權代修費
        /// </summary>
        public decimal? FinalCost_RoadRightProxy { get; set; }
        /// <summary>
        /// todo :各項費用_員工工資 (資料來源?)
        /// </summary>
        public decimal? FinalCost_EmployeeSalary { get; set; }

        /// <summary>
        /// 各項費用_其他(保留空白)
        /// </summary>
        public decimal? FinalCost_Other { get; set; }

        /// <summary>
        /// 各項費用_總計
        /// </summary>
        public decimal? FinalCost_Total { get; set; }

        /// <summary>
        /// 日漏水量
        /// </summary>
        public decimal? DailyAmount { get; set; }

        /// <summary>
        /// 總漏水量
        /// </summary>
        public decimal? TotalAmount { get; set; }



        /// <summary>
        /// 派工時間
        /// </summary>
        public DateTime? DispatchTime { get; set; }


        /// <summary>
        /// 開工時間
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 修復時間
        /// </summary>
        public DateTime? FixTime { get; set; }
    }

    /// <summary>
    /// 定義 filter 以用迴圈產生資料
    /// </summary>
    public class RA023Filter
    {

        public string Title { get; set; }
        public Func<RA023FixForm, bool> func { get; set; }

        public RA023Item GenerateItem(ICollection<RA023FixForm> allForms)
        {
            var forms = allForms.Where(func);

            RA023Item item = new RA023Item
            {
                Title = Title,
            };
            item.TotalCase = forms.Count();
            item.FixUnit_Self = forms.Count(x => x.FixUnit == "自修");
            item.FixUnit_OutSourcing = forms.Count(x => x.FixUnit == "委外");
            item.FixUnit_Warranty = forms.Count(x => x.FixUnit == "保固修理");
            item.FixUnit_Other = forms.Count(x => x.FixUnit == "其他");

            item.FinalCost_Outsourcing = forms.Sum(x => x.FinalCost_Outsourcing ?? 0);
            item.FinalCost_Material = forms.Sum(x => x.FinalCost_Material ?? 0);
            item.FinalCost_RoadRightProxy = forms.Sum(x => x.FinalCost_RoadRightProxy ?? 0);
            item.FinalCost_EmployeeSalary = forms.Sum(x => x.FinalCost_EmployeeSalary ?? 0);
            item.FinalCost_Other = forms.Sum(x => x.FinalCost_Other ?? 0);
            item.FinalCost_Total = forms.Sum(x => x.FinalCost_Total ?? 0);

            item.Leakage_Case = forms.Count(x => x.CaseAttribute == "漏水案件");
            item.Leakage_FixHours = forms.Where(x => x.CaseAttribute == "漏水案件" && x.StartTime.HasValue && x.FixTime.HasValue).Sum(x => (x.FixTime!.Value - x.StartTime!.Value).TotalHours);
            item.Leakage_ProcessHours = forms.Where(x => x.CaseAttribute == "漏水案件" && x.DispatchTime.HasValue && x.FixTime.HasValue).Sum(x => (x.FixTime!.Value - x.DispatchTime!.Value).TotalHours);

            item.NotLeakage_Case = forms.Count(x => x.CaseAttribute == "非漏水案件");
            item.NotLeakage_FixHours = forms.Where(x => x.CaseAttribute == "非漏水案件" && x.StartTime.HasValue && x.FixTime.HasValue).Sum(x => (x.FixTime!.Value - x.StartTime!.Value).TotalHours);
            item.NotLeakage_ProcessHours = forms.Where(x => x.CaseAttribute == "非漏水案件" && x.DispatchTime.HasValue && x.FixTime.HasValue).Sum(x => (x.FixTime!.Value - x.DispatchTime!.Value).TotalHours);

            return item;
        }
    }

    public class RA023FilterGroup
    {
        public string Kind { get; set; }
        public List<RA023Filter> Filters { get; set; }

        /// <summary>
        /// 是否產生小計
        /// </summary>
        public bool Sum { get; set; }
    }

    
}
