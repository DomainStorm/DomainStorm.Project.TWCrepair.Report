using System.Net.WebSockets;
using System.Text;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 漏水原因分析表
    public class RA020 : ReportDataModel
    {

        #region 定義各種詞庫的項目

        /// <summary>
        /// 設備屬性
        /// </summary>
        public static readonly string[] equipmentAttributes =
        {
        "管線","附屬設備","表箱另件", "其他"
        };


        /// <summary>
        /// 管種
        /// </summary>
        public static readonly string[] pipeKinds = new string[]
        {
        "DIP","CIP","SP","SSP","PSCP","PCCP","FRP","PVCP","PVCP/PE","ABSP","HDPEP","HIWP","其他","殘存管","接合管鞍","RCP","GIP","STEEL","WSP","LP"
        };

        /// <summary>
        /// 附屬設備
        /// </summary>
        public static readonly string[] accessoryEquipments =
        {
        "制水閥","地上消栓","地下消栓","排氣閥","其他"
        };

        /// <summary>
        /// 表箱另件
        /// </summary>
        public static readonly string[] boxAnnexs =
        {
        "止水栓","管套節","其他"
        };


        //以下用於縱軸各欄, 要照報表的順序
        /// <summary>
        /// 漏水原因
        /// </summary>
        public static readonly string[] leakageReasons =
        {
        "老化腐蝕","荷重振動","水錘","地盤下陷","施工不良","回填不良","材質不良","工程施工","其他"
        };


        /// <summary>
        /// 修理狀況
        /// </summary>
        public static readonly string[] fixSituations =
        {
        "折斷","空洞","裂縫","脫接","橡皮墊","管鞍","其他"
        };

        #endregion
        /// <summary>
        /// 區處名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 廠所名稱
        /// </summary>
        public string SiteName { get; set; }


        public string DateRange { get; set; }

        public int AllEquipmentDataLeakageReasonTotal
        {
            get => EquipmentDataDic["合計"].LeakageReasonTotal;
        }
        public int AllEquipmentDataFixSituationTotal
        {
            get => EquipmentDataDic["合計"].FixSituationTotal;
        }





        /// <summary>
        /// 所有設備的各項統計數據 key 為設備的兩層資訊合起來(例如  "管線-DIP" , "管線-CIP" , "附屬設備-制水閥")
        /// </summary>
        public Dictionary<string, EquipmentData> EquipmentDataDic { get; set; } = new Dictionary<string, EquipmentData>();


        public string GetEquipmentDataKey(string level1, string level2)
        {
            if (!string.IsNullOrEmpty(level2))
                return $"{level1}-{level2}";
            else
                return level1;
        }

        public void AddEquipmentData(string level1Equip, string level2Equip , EquipmentData data)
        {
            string key = GetEquipmentDataKey(level1Equip, level2Equip);
            data.Parent = this;
            EquipmentDataDic.Add(key, data);
        }

        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <returns></returns>
        public static double Percentage(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(100.0 * numerator / denominator, 2, MidpointRounding.AwayFromZero);
            }
        }


        public string EquipmentDataHtml(string level1Equip, string level2Equip)
        {
            string key = GetEquipmentDataKey(level1Equip, level2Equip);
            EquipmentData data;
            if (EquipmentDataDic.ContainsKey(key))
            {
                data = EquipmentDataDic[key];
            }
            else
            {
                data = new EquipmentData(this);
            }
            return data.Html();
        }
    }


    /// <summary>
    /// 設備的各項統計數據
    /// </summary>
    public class EquipmentData
    {

        /// <summary>
        /// 各種漏水原因統計
        /// </summary>
        public int[] LeakageReason { get; set; } = new int[RA020.leakageReasons.Length];

        /// <summary>
        /// 漏水原因合計
        /// </summary>
        public int LeakageReasonTotal { get => LeakageReason.Sum();}

        /// <summary>
        /// 漏水原因合計的百分比
        /// </summary>
        public double LeakageReasonPercentage { get => RA020.Percentage(LeakageReasonTotal, Parent.AllEquipmentDataLeakageReasonTotal); }

        /// <summary>
        /// 各種修理狀況統計
        /// </summary>
        public int[] FixSituation { get; set; } = new int[RA020.fixSituations.Length];

        /// <summary>
        /// 修理狀況合計
        /// </summary>
        public int FixSituationTotal { get => FixSituation.Sum(); }

        /// <summary>
        /// 修理狀況合計的百分比
        /// </summary>
        public double FixSituationPercentage { get => RA020.Percentage(FixSituationTotal, Parent.AllEquipmentDataFixSituationTotal); }

        public RA020 Parent { get; set; }

        public EquipmentData(RA020 rA020)
        {
            Parent = rA020;
        }

        public EquipmentData()
        {
            
        }

        /// <summary>
        /// 產生合計列用
        /// </summary>
        public EquipmentData(IReadOnlyCollection<EquipmentData> datas)
        {
            for(int i = 0; i < LeakageReason.Length; i++ )
            {
                this.LeakageReason[i] = datas.Sum(x => x.LeakageReason[i]);
            }
            for (int i = 0; i < FixSituation.Length; i++)
            {
                this.FixSituation[i] = datas.Sum(x => x.FixSituation[i]);
            }
        }


        public string Html()
        {
            StringBuilder sb = new StringBuilder();

            //漏水原因及合計
            for (var i = 0; i <= LeakageReason.Length; i++)
            {
                var value = i == LeakageReason.Length ? LeakageReasonTotal : LeakageReason[i];
                sb.AppendLine(@$"<table:table-cell table:style-name=""ce28"" office:value-type=""string"" calcext:value-type=""string"">
                        <text:p>{value}</text:p>
                    </table:table-cell>");
            }

            //漏水原因百分比 (配合範本用不同的 style, 所以分開寫)
            sb.AppendLine($@"<table:table-cell table:style-name=""ce41"" office:value-type=""string"" calcext:value-type=""string"">
                        <text:p>{LeakageReasonPercentage}</text:p>
                    </table:table-cell>");


            //修理狀況及合計
            for (var i = 0; i <= FixSituation.Length; i++)
            {
                var value = i == FixSituation.Length ? FixSituationTotal : FixSituation[i];
                var style = i == FixSituation.Length ? "ce41" : "ce28";   //最後一欄有粗框
                sb.AppendLine(@$"<table:table-cell table:style-name=""{style}"" office:value-type=""string"" calcext:value-type=""string"">
                        <text:p>{value}</text:p>
                    </table:table-cell>");
            }

           

            return sb.ToString();
        }



    }


}


