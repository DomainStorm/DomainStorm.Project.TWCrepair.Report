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

        public static readonly string[] distributeEquipments =
        {
            "送配水管線及設備","用戶外線及設備","其他"
        };

        public static readonly string[] situations =
        {
            "漏出地面","滲入地下","其他漏水"
        };


        public static readonly string[] sources =
        {
            "客服系統","抄表後送","其他民眾","檢漏單位","員工報修","其它機關","委外檢漏案件"
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


        /// <summary>
        /// 依漏送配水及用水設備作統計(key :為設備的兩層資訊合起來 , 例如 "送配水管線及設備-管線" ,  "送配水管線及設備-附屬設備")
        /// </summary>
        public Dictionary<string, DistributeEquipmentData> DistributeEquipmentDataDic { get; set; } = new Dictionary<string, DistributeEquipmentData>();


        /// <summary>
        /// 依漏水情形作統計(key : 漏水情形名稱)
        /// </summary>
        public Dictionary<string, SituationData> SituationDataDic { get; set; } = new Dictionary<string, SituationData>();


        /// <summary>
        /// 依來源作統計(key : 來源名稱)
        /// </summary>
        public Dictionary<string, SourceData> SourceDataDic { get; set; } = new Dictionary<string, SourceData>();


        public string GetDataKey(string level1, string level2)
        {
            if (!string.IsNullOrEmpty(level2))
                return $"{level1}-{level2}";
            else
                return level1;
        }

        public void AddEquipmentData(string level1Equip, string level2Equip , EquipmentData data)
        {
            string key = GetDataKey(level1Equip, level2Equip);
            data.Parent = this;
            EquipmentDataDic.Add(key, data);
        }

        public void AddDistributeEquipmentData(string level1Equip, string level2Equip, DistributeEquipmentData data)
        {
            string key = GetDataKey(level1Equip, level2Equip);
            data.Parent = this;
            DistributeEquipmentDataDic.Add(key, data);
        }


        public void AddSituationData(string level1Equip, string level2Equip, SituationData data)
        {
            string key = GetDataKey(level1Equip, level2Equip);
            data.Parent = this;
            SituationDataDic.Add(key, data);
        }


        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <returns></returns>
        public static double Percentage(double numerator, double denominator)
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level1"></param>
        /// <param name="level2"></param>
        /// <param name="bottomBold">底線是否要加粗體</param>
        /// <returns></returns>
        public string EquipmentDataHtml(string level1, string level2, bool bottomBold = false)
        {
            string key = GetDataKey(level1, level2);
            EquipmentData data;
            if (EquipmentDataDic.ContainsKey(key))
            {
                data = EquipmentDataDic[key];
            }
            else
            {
                data = new EquipmentData(this);
            }
            return data.Html(bottomBold);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level1"></param>
        /// <param name="level2"></param>
        /// <param name="bottomBold">底線是否要加粗體</param>
        /// <returns></returns>
        public string DistributeEquipmentDataHtml(string level1, string level2, bool bottomBold = false, int rowSpan = 1)
        {
            string key = GetDataKey(level1, level2);
            DistributeEquipmentData data;
            if (DistributeEquipmentDataDic.ContainsKey(key))
            {
                data = DistributeEquipmentDataDic[key];
            }
            else
            {
                data = new DistributeEquipmentData(this);
            }
            return data.Html(bottomBold, rowSpan);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level1"></param>
        /// <param name="bottomBold">底線是否要加粗體</param>
        /// <returns></returns>
        public string SituationDataHtml(string level1,  bool bottomBold = false, int rowSpan = 1)
        {
            SituationData data;
            if (SituationDataDic.ContainsKey(level1))
            {
                data = SituationDataDic[level1];
            }
            else
            {
                data = new SituationData(this);
            }
            return data.Html(bottomBold, rowSpan);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level1"></param>
        /// <param name="level2"></param>
        /// <param name="bottomBold">底線是否要加粗體</param>
        /// <returns></returns>
        public string SourceDataHtml(string level1,bool bottomBold = false)
        {
            SourceData data;
            if (SourceDataDic.ContainsKey(level1))
            {
                data = SourceDataDic[level1];
            }
            else
            {
                data = new SourceData();
            }
            return data.Html(bottomBold);
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


        /// <summary>
        /// 日漏水量
        /// </summary>
        public decimal DailyAmount { get; set; }

        /// <summary>
        /// 總漏水量
        /// </summary>
        public decimal TotalAmount { get; set; }

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
            this.DailyAmount = datas.Sum(x => x.DailyAmount);
            this.TotalAmount = datas.Sum(x => x.TotalAmount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bottomBold">底線是否要加粗體</param>
        /// <returns></returns>
        public string Html(bool bottomBold = false)
        {
            StringBuilder sb = new StringBuilder();
            var style1 = bottomBold ? 
                "ce133" :   //底線加粗
                "ce132";    //底線normal
            var style2 = bottomBold ? 
                "ce146" : //底線加粗,右邊加粗
                "ce145";  //底線正常,右邊加粗

            //漏水原因及合計
            for (var i = 0; i <= LeakageReason.Length; i++)
            {
                var value = i == LeakageReason.Length ? LeakageReasonTotal : LeakageReason[i];
                sb.AppendLine(@$"<table:table-cell table:style-name=""{style1}"" office:value-type=""string"" calcext:value-type=""string"">
                        <text:p>{value}</text:p>
                    </table:table-cell>");
            }

            //漏水原因百分比 (配合範本用不同的 style, 所以分開寫)
            sb.AppendLine($@"<table:table-cell table:style-name=""{style2}"" office:value-type=""string"" calcext:value-type=""string"">
                        <text:p>{LeakageReasonPercentage}</text:p>
                    </table:table-cell>");


            //修理狀況及合計
            for (var i = 0; i <= FixSituation.Length; i++)
            {
                var value = i == FixSituation.Length ? FixSituationTotal : FixSituation[i];
                var style = i == FixSituation.Length ? style2 : style1;
                sb.AppendLine(@$"<table:table-cell table:style-name=""{style}"" office:value-type=""string"" calcext:value-type=""string"">
                        <text:p>{value}</text:p>
                    </table:table-cell>");
            }

            //日漏水量
            sb.AppendLine(@$"<table:table-cell table:style-name=""{style1}"" office:value-type=""string"" calcext:value-type=""string"">
                        <text:p>{DailyAmount}</text:p>
                    </table:table-cell>");


            //總漏水量
            sb.AppendLine(@$"<table:table-cell table:style-name=""{style2}"" office:value-type=""string"" calcext:value-type=""string"">
                        <text:p>{TotalAmount}</text:p>
                    </table:table-cell>");

            return sb.ToString();
        }
    }



    /// <summary>
    /// 送配水及用水設備
    /// </summary>
    public class DistributeEquipmentData
    {
        /// <summary>
        /// 件數
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 件數百分比
        /// </summary>
        public double CountPercentage { get => RA020.Percentage(Count, Parent.DistributeEquipmentDataDic["合計"].Count); }

        /// <summary>
        /// 漏水流量
        /// </summary>
        public decimal LeakageAmount { get; set; } = 0;

        /// <summary>
        /// 漏水流量百分比
        /// </summary>
        public double LeakageAmountPercentage
        {
            get => RA020.Percentage(
            decimal.ToDouble(LeakageAmount),
            decimal.ToDouble(Parent.DistributeEquipmentDataDic["合計"].LeakageAmount));
        }



        public RA020 Parent { get; set; }

        public DistributeEquipmentData(RA020 rA020)
        {
            Parent = rA020;
        }

        public DistributeEquipmentData()
        {

        }

        /// <summary>
        /// 產生合計列用
        /// </summary>
        public DistributeEquipmentData(IReadOnlyCollection<DistributeEquipmentData> datas)
        {
            this.Count = datas.Sum(x => x.Count);
            this.LeakageAmount = datas.Sum(x => x.LeakageAmount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bottomBold">底線是否要加粗體</param>
        /// <returns></returns>
        public string Html(bool bottomBold, int rowSpan)
        {
            StringBuilder sb = new StringBuilder();
            var style1 = bottomBold ?
                "ce133" :   //底線加粗
                "ce135";    //底線normal
            var style2 = bottomBold ?
                "ce146" : //底線加粗,右邊加粗
                "ce141";  //底線正常,右邊加粗


            sb.AppendLine(@$"<table:table-cell table:style-name=""{style1}"" office:value-type=""string"" calcext:value-type=""string"" table:number-columns-spanned=""2"" table:number-rows-spanned=""{rowSpan}"">
                          <text:p>{Count}</text:p>
                         </table:table-cell>
                         <table:covered-table-cell table:style-name=""{style1}"" />");
            sb.AppendLine(@$"<table:table-cell table:style-name=""{style1}"" office:value-type=""string"" table:number-rows-spanned=""{rowSpan}"" calcext:value-type=""string"">
                              <text:p>{CountPercentage}%</text:p>
                             </table:table-cell>");
            sb.AppendLine(@$"<table:table-cell table:style-name=""{style1}"" office:value-type=""string"" calcext:value-type=""string"" table:number-columns-spanned=""2"" table:number-rows-spanned=""{rowSpan}"">
                          <text:p>{LeakageAmount}</text:p>
                         </table:table-cell>
                         <table:covered-table-cell table:style-name=""{style1}""/>");
            sb.AppendLine(@$"<table:table-cell table:style-name=""{style2}"" office:value-type=""string"" table:number-rows-spanned=""{rowSpan}"" calcext:value-type=""string"">
                          <text:p>{LeakageAmountPercentage}%</text:p>
                         </table:table-cell>");
            return sb.ToString();
        }
    }


    /// <summary>
    /// 依來源作統計的數據
    /// </summary>
    public class SourceData
    {
        /// <summary>
        /// 件數
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 漏水流量
        /// </summary>
        public decimal LeakageAmount { get; set; } = 0;

        public SourceData()
        {

        }

        /// <summary>
        /// 產生合計列用
        /// </summary>
        public SourceData(IReadOnlyCollection<SourceData> datas)
        {
            this.Count = datas.Sum(x => x.Count);
            this.LeakageAmount = datas.Sum(x =>x.LeakageAmount);    
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bottomBold">底線是否要加粗體</param>
        /// <returns></returns>
        public string Html(bool bottomBold)
        {
            StringBuilder sb = new StringBuilder();
            var style1 = bottomBold ?
                "ce137" :   //底線加粗
                "ce136";    //底線normal
          

            sb.AppendLine(@$"<table:table-cell table:style-name=""{style1}"" office:value-type=""string"" calcext:value-type=""string"" table:number-columns-spanned=""2"" table:number-rows-spanned=""1"">
                      <text:p>{Count}</text:p>
                     </table:table-cell>
                     <table:covered-table-cell table:style-name=""{style1}""/>
                     <table:table-cell table:style-name=""{style1}"" office:value-type=""string"" calcext:value-type=""string"" table:number-columns-spanned=""2"" table:number-rows-spanned=""1"">
                     <text:p>{LeakageAmount}</text:p>
                     </table:table-cell>");

            return sb.ToString();
        }
    }


    /// <summary>
    /// 依漏水情形作統計的數據
    /// </summary>
    public class SituationData
    {
        /// <summary>
        /// 件數
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 件數百分比
        /// </summary>
        public double CountPercentage { get => RA020.Percentage(Count, Parent.SituationDataDic["合計"].Count); }

        /// <summary>
        /// 漏水流量
        /// </summary>
        public decimal LeakageAmount { get; set; } = 0;

        /// <summary>
        /// 漏水流量百分比
        /// </summary>
        public double LeakageAmountPercentage { get => RA020.Percentage( 
            decimal.ToDouble(LeakageAmount),  
            decimal.ToDouble( Parent.SituationDataDic["合計"].LeakageAmount)); }



        public RA020 Parent { get; set; }

        public SituationData(RA020 rA020)
        {
            Parent = rA020;
        }

        public SituationData()
        {

        }

        /// <summary>
        /// 產生合計列用
        /// </summary>
        public SituationData(IReadOnlyCollection<SituationData> datas)
        {
            this.Count = datas.Sum(x => x.Count);
            this.LeakageAmount = datas.Sum(x => x.LeakageAmount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bottomBold">底線是否要加粗體</param>
        /// <returns></returns>
        public string Html(bool bottomBold , int rowSpan)
        {
            StringBuilder sb = new StringBuilder();
            var style1 = bottomBold ?
                "ce133" :   //底線加粗
                "ce132";    //底線normal
            var style2 = bottomBold ?
                "ce146" : //底線加粗,右邊加粗
                "ce145";  //底線正常,右邊加粗
         

            sb.AppendLine(@$"<table:table-cell table:style-name=""{style1}"" office:value-type=""string"" calcext:value-type=""string"" table:number-columns-spanned=""1"" table:number-rows-spanned=""{rowSpan}"">
                        <text:p>{Count}</text:p>
                    </table:table-cell>");
            sb.AppendLine(@$"<table:table-cell table:style-name=""{style1}"" office:value-type=""string"" calcext:value-type=""string"" table:number-columns-spanned=""1"" table:number-rows-spanned=""{rowSpan}"">
                        <text:p>{CountPercentage}%</text:p>
                    </table:table-cell>");
            sb.AppendLine(@$"<table:table-cell table:style-name=""{style1}"" office:value-type=""string"" calcext:value-type=""string"" table:number-columns-spanned=""1"" table:number-rows-spanned=""{rowSpan}"">
                        <text:p>{LeakageAmount}</text:p>
                    </table:table-cell>");
            sb.AppendLine(@$"<table:table-cell table:style-name=""{style2}"" office:value-type=""string"" calcext:value-type=""string"" table:number-columns-spanned=""1"" table:number-rows-spanned=""{rowSpan}"">
                        <text:p>{LeakageAmountPercentage}%</text:p>
                    </table:table-cell>");

          

            return sb.ToString();
        }
    }

}


