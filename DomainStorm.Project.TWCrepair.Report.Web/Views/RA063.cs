using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using Nest;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 預算書-XML
    /// </summary>
    public class RA063 : ReportDataModel
    {

        public string DocumentType { get; set; }    

        /// <summary>
        /// 區處的機關名稱
        /// </summary>
        public string? OrgName { get; set; }

        /// <summary>
        /// 區處的機關代碼
        /// </summary>
        public string? OrgCode { get; set; }

        /// <summary>
        /// 區處的地址(縣市別)
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 預算書編號
        /// </summary>
        public string? SerialNumber { get; set; }

        /// <summary>
        /// 工程編號
        /// </summary>
        public string? EngineeringNumber { get; set; }

        /// <summary>
        /// 工程名稱
        /// </summary>
        public string EngineeringName { get; set; }

        /// <summary>
        /// 工程費用
        /// </summary>
        public string? EngineeringPrice { get; set; }

        /// <summary>
        /// 材料費用
        /// </summary>
        public string? MaterialPrice { get; set; }

        /// <summary>
        /// 小計
        /// </summary>
        public string? SubTotalPrice { get; set; }

        /// <summary>
        /// 營業稅
        /// </summary>
        public string? Tax { get; set; }

        /// <summary>
        /// 總計
        /// </summary>
        public string? TotalPrice { get; set; }


        /// <summary>
        /// 總計(中文大寫)
        /// </summary>
        public string? TotalPriceStr { get; set; }

       
        
        public List<RA063DetailItem> DetailItems { get; set; }


        /// <summary>
        /// 901-詳細表小計的小計
        /// </summary>
        public int Detail_SubTotal { get; set; }

        /// <summary>
        /// 911-詳細表共計
        /// </summary>
        public int Detail_Total { get; set; }

        /// <summary>
        /// 912-詳細表小計的營業稅
        /// </summary>
        public int Detail_Tax { get; set; }

        /// <summary>
        /// 913-詳細表總計(含稅)
        /// </summary>
        public int Detail_FinalTotal { get; set; }


        /// <summary>
        /// 將 500 及 9XX 的項目整理成物件清單
        /// </summary>
        public List<RA063Detail9XXItem> Detail9XXItems { get; set; } = new List<RA063Detail9XXItem>();

        public List<RA063WorkItem> ResourceItems { get; set; } = new List<RA063WorkItem>();

        //尾項用, 舊系統的尾項會有代碼 ,例如 A00048 ,xml 會產生<WorkItem itemCode="W02252A00048D">  但轉檔時未轉入, 且新資料也沒有
        //在此給每個項目動態編碼用
        public int TailIndex = 10001;  


    }

    /// <summary>
    /// 相當於預算書的詳細表
    /// </summary>
    public class RA063DetailItem : BudgetDocDetailItem
    {
        public decimal UnitAmount { get; set; }
        public List<RA063DetailItemDayOrNightPart> DayOrNightParts { get; set; } = new List<RA063DetailItemDayOrNightPart>();

        public void GenerateDayOrNightParts(
            List<BudgetPCCEsItem> pccItems, 
            Repository.Models.Budget.BudgetDocUnitPrice unitPrice,
            RA063 ra063)
        {
            var pccItem = pccItems.FirstOrDefault(x => x.Code == Code);
            if(DayAmount > 0 )
            {
                var part = new RA063DetailItemDayOrNightPart(
                    this,
                    this.DayAmount,
                    pccItem!,
                    unitPrice,
                    "D",
                    pccItems,
                    ra063
                    );
                DayOrNightParts.Add(part);
            }
            if (NightAmount > 0)
            {
                var part = new RA063DetailItemDayOrNightPart(
                    this,
                    this.NightAmount,
                    pccItem!,
                    unitPrice,
                    "N",
                    pccItems,
                    ra063
                    );
                DayOrNightParts.Add(part);
            }
        }
    }


    /// <summary>
    /// 詳細表的日夜間數量轉成物件
    /// </summary>
    public class RA063DetailItemDayOrNightPart
    {
        public string? Code { get; set;  }
        public string? Name { get; set; }
        public string? Unit { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public decimal Quantity { get; set ; } 


        public int UnitPrice { get; set; }
        public decimal TotalPrice
        {
            get => Quantity * UnitPrice;
        }

        public List<RA063WorkItem> UnitPriceMembers { get; set; } = new List<RA063WorkItem>();


        public Guid BudgetDocUnitPriceId { get; set; }

        public RA063DetailItemDayOrNightPart(
            RA063DetailItem item , 
            decimal amount ,
            BudgetPCCEsItem pccItem, 
            Repository.Models.Budget.BudgetDocUnitPrice unitPrice,  
            string dayOrNight,
            List<BudgetPCCEsItem> pccItems,
            RA063 ra063)
        {
            BudgetDocUnitPriceId = unitPrice.Id;
            Code = $"022520{item.Code}{dayOrNight}";
            Name = $"{item.Name}, {(dayOrNight == "D" ? "(日間)" : "(夜間)")}";
            Unit = item.Unit ;
            Quantity = amount;
            
            //若有定義工程會的代碼,轉換成工程會的
            if (pccItem != null)// && !string.IsNullOrEmpty(pccItem.DayCode))    //若工程會代碼空白,不用它的( 以 DayCode 判斷就好)
            {
				if(!string.IsNullOrEmpty(pccItem.Unit))
				{
					Unit = pccItem.Unit;
				}
                
                if (dayOrNight == "D")
                {
                    Code = pccItem.DayCode;  //從 xml 來看, code 可能換成工程會空白的
                    if(!string.IsNullOrEmpty(pccItem.DayName))   //但若 name 空白, 維持用上面的 name
                    {
                        Name = pccItem.DayName;
                    }
                    
                }
                else
                {
                    Code = pccItem.NightCode; //從 xml 來看, code 可能換成工程會空白的
                    if (!string.IsNullOrEmpty(pccItem.NightName))   //但若 name 空白, 維持用上面的 name
                    {
                        Name = pccItem.NightName;
                    }
                }
            }
            

            if ((unitPrice.TailDayPrice > 0 || unitPrice.TailNightPrice > 0) && unitPrice.Tail == null)
            {
                throw new Exception($"單價分析表{unitPrice.Code}查無尾項詞庫");   //舊資料尾項亂碼,對應不到詞庫
            }


            //處理成員
            if(dayOrNight == "D")
            {
                UnitPrice = (int)Math.Round( (unitPrice.TotalDayPrice / unitPrice.UnitAmount), 0);
                var members = unitPrice.BudgetDocUnitPriceMembers
                    //.Where(x => x.DayAmount > 0)  從 xml 看來, 數量為 0 的也要產出
                    .OrderBy(x => x.Sort);
                foreach(var member in members)
                {
                    UnitPriceMembers.Add(new RA063WorkItem(
                        member, 
                        dayOrNight, 
                        pccItems,
                        ra063)); 
                }
                //處理尾項
                //if(unitPrice.TailDayPrice > 0)
                //{

                    UnitPriceMembers.Add(new RA063WorkItem(
                        unitPrice.Tail!,
                        "D",
                        unitPrice.TailPersentage,
                        unitPrice.TailDayPrice,
                        pccItems, 
                        ra063));
                //}
            }
            else
            {
                UnitPrice = (int)Math.Round((unitPrice.TotalNightPrice / unitPrice.UnitAmount), 0);
                var members = unitPrice.BudgetDocUnitPriceMembers
                    //.Where(x => x.NightAmount > 0) 從 xml 看來, 數量為 0 的也要產出
                    .OrderBy(x => x.Sort);
                foreach (var member in members)
                {
                    UnitPriceMembers.Add(new RA063WorkItem(
                        member, 
                        dayOrNight, 
                        pccItems, 
                        ra063));
                }
                //處理尾項
                //if (unitPrice.TailNightPrice > 0)
                //{
                    UnitPriceMembers.Add(new RA063WorkItem(
                        unitPrice.Tail!,
                        "N",
                        unitPrice.TailPersentage,
                        unitPrice.TailNightPrice,
                        pccItems, 
                        ra063));
                //}
            }
        }
    }


    /// <summary>
    /// 將 500 及 9XX 的項目整理成物件
    /// </summary>
    public class RA063Detail9XXItem
    {
        public string Name { get; set;  }
        public decimal Amout { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
    }


    /// <summary>
    /// (單價分析表,單價分析表成員,資源統計表)共用的物件
    /// </summary>
    public class RA063WorkItem
    {
        public string ItemCode { get; set; }

        public string ItemKind { get; set; }

        /// <summary>
        /// 單位數量
        /// </summary>
        //若是組合單價時要帶出參照到的項目的 UnitAmount
        //若是對應到資源統計表,要看層級, 例如 001 組合單價有個成員  是 047 的一般單價分析表, 裡面有 小工 的資源統計項目
        // 001 的 047 childNode 裡的小工, UnitAmount = 0
        //但 047 本身的小工的 UnitAmount =  1
        public decimal AnalysisOutputQuantity { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        /// <summary>
        /// 日夜數量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 日夜單價
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 日夜總價
        /// </summary>
        public decimal Amount { get; set; }

        public string Remark { get; set; }

        public decimal Percent { get; set; } = 0;



        public List<RA063WorkItem> UnitPriceMembers { get; set; } = new List<RA063WorkItem>();

        /// <summary>
        /// 單價分析表的尾項用
        /// </summary>
        public RA063WorkItem(

            Repository.Models.Word word,
            string dayOrNight,
            decimal? percent,
            decimal? amount,
            List<BudgetPCCEsItem> pccItems,
            RA063 ra063
            )
        {
            ItemKind = "variableSumPercentage";
            AnalysisOutputQuantity = 0;
            Quantity = 1;   //數量固定都是 1
            Price = Amount = amount ??  0;  //單價= 總價
            Unit = "式";


           

            //這邊只能 hard code 處理
            if (word.Name == "其他安全衛生設施及作業費")
            {
                ItemCode = "W01271F0004";
                Description = "職業安全作業費";
                Percent = percent?? 0;
            }
            else if(word.Name == "單價整數調整")
            {
                ItemCode = "W0127100004";
                Description = "計量與計價";
            }
            else if(percent.HasValue)
            {
                Description = $"工具損耗，約以上項目之{ percent.Value.ToString("0.0") }%";
                Percent = percent.Value ;
                var pccitem = pccItems.FirstOrDefault(x => x.DayName == Description);
                if(pccitem == null)
                {
                    ra063.TailIndex++;
                    Description = $"{word.Name}, { percent?? 0}, {(dayOrNight == "D" ? "(日間)" : "(夜間)")}";
                    ItemCode = $"W02252A{ra063.TailIndex}{dayOrNight}";    
                }
                else
                {
                    ItemCode = pccitem.DayCode!;   //一對用 day   (Code 一樣)
                }
                    
            }
            else
            {
                throw new Exception($"尾項 [{word.Name}] 無法對應到工程會代碼");
            }

            if(ItemCode == "W0127120004" || ItemCode == "W0127110004")
            {
                ItemKind = "variablePrice";
            }
        }
        


        public RA063WorkItem(
            Repository.Models.Budget.BudgetDocUnitPriceMember member , 
            string dayOrNight,
            List<BudgetPCCEsItem> pccItems,
            RA063 ra063,
            int level = 1
            ) 
        {
            if(dayOrNight == "D")
            {
                Quantity = member.DayAmount;
                Price = member.DayUnitPrice;
                Amount = member.DayPrice;
            }
            else
            {
                Quantity = member.NightAmount;
                Price = member.NightUnitPrice;
                Amount = member.NightPrice;
            }

            Remark = member.Notes;

            if(member.ResourceWorkMaterial != null)
            {
                LoadData(
                    member.ResourceWorkMaterial,
                    dayOrNight,
                    level,
                    pccItems
                    );
            }
            else
            {
                LoadData(
                    member.BudgetDocUnitPrice,
                    dayOrNight,
                    level,
                    pccItems,
                    ra063
                    );
            }
        }

        /// <summary>
        /// 資源統計表物件用
        /// </summary>
        public RA063WorkItem(
            BudgetDocResourceStatisticsItem resource,
            string dayOrNight,
            List<BudgetPCCEsItem> pccItems
            )
        {
            if(dayOrNight == "D")
            {
                Quantity = resource.DayAmount;
                Price = resource.DayUnitPrice;
                Amount = resource.DayPrice;
            }
            else
            {
                Quantity = resource.NightAmount;
                Price = resource.NightUnitPrice;
                Amount = resource.NightPrice;
            }
            
            LoadData(
                resource.Code!,
                resource.Unit,
                resource.Name,
                resource.Description,
                dayOrNight,
                -1,
                pccItems
                );
        }



        public void LoadData(
            Repository.Models.Budget.BudgetDocUnitPrice unitPrice,
            string dayOrNight,
            int level,
            List<BudgetPCCEsItem> pccItems,
            RA063 ra063)
        {

			Unit = unitPrice.Unit;
			Description = $"{unitPrice.Name}, {(dayOrNight == "D" ? "(日間)" : "(夜間)")}";
			ItemCode = $"022520{unitPrice.Code}{dayOrNight}";


			var pccItem = pccItems.FirstOrDefault(x => x.Code == unitPrice.Code);
            if (pccItem != null)
            {
                if (!string.IsNullOrEmpty(pccItem.Unit))
                {
                    Unit = pccItem.Unit!;
                }
                
                if (dayOrNight == "D")
                {
                    ItemCode = pccItem.DayCode!;  //從 xml 來看, code 可能換成工程會空白的
                    if (!string.IsNullOrEmpty(pccItem.DayName))   //但若 name 空白, 維持用原本的 Description
                    {
                        Description = pccItem.DayName;
                    }

                }
                else
                {
                    ItemCode = pccItem.NightCode!;  //從 xml 來看, code 可能換成工程會空白的
                    if (!string.IsNullOrEmpty(pccItem.NightName))   //但若 name 空白, 維持用原本的 Description
                    {
                        Description = pccItem.NightName;
                    }
                }
            }
            else
            {

            }

             ItemKind = "analysis";
            AnalysisOutputQuantity = unitPrice.UnitAmount;

            //以 C7113043 為例, 001 的單價分析表有 042 人工挖方  , 047 殘土處理,   但 047 不在詳細表裡 (數量為0) 
            //所以042-的 workitem 還有 child , 047 只載入第一層
            bool loadChild = ra063.DetailItems.Any(x => x.Code == unitPrice.Code);
            

            if (loadChild)
            {
                foreach (var member in unitPrice.BudgetDocUnitPriceMembers.OrderBy(x => x.Sort))
                {
                    var childMember = new RA063WorkItem(
                        member,
                        dayOrNight,
                        pccItems,
                        ra063,
                        level + 1
                        );

                    UnitPriceMembers.Add(childMember);

                }

                if ((unitPrice.TailDayPrice > 0 || unitPrice.TailNightPrice > 0) && unitPrice.Tail == null)
                {
                    throw new Exception($"單價分析表{unitPrice.Code}查無尾項詞庫");   //舊資料尾項亂碼,對應不到詞庫
                }

                //處理尾項
                if (dayOrNight == "D" ) //&& unitPrice.TailDayPrice > 0)
                {

                    UnitPriceMembers.Add(new RA063WorkItem(
                           unitPrice.Tail!,
                           dayOrNight,
                           unitPrice.TailPersentage,
                           unitPrice.TailDayPrice,
                           pccItems,
                           ra063));
                }
                else if (dayOrNight == "N") // && unitPrice.TailNightPrice > 0)
                {

                    UnitPriceMembers.Add(new RA063WorkItem(
                           unitPrice.Tail!,
                           dayOrNight,
                           unitPrice.TailPersentage,
                           unitPrice.TailNightPrice,
                           pccItems,
                           ra063));
                }
            }
        }


        public void LoadData(  
            Repository.Models.Budget.ResourceWorkMaterial material, 
            string dayOrNight, 
            int level, 
            List<BudgetPCCEsItem> pccItems)
        {
            LoadData(
                material.Code,
                material.Unit,
                material.Name,
                material.Description!,
                dayOrNight,
                level,
                pccItems
                );
        }

        public void LoadData(
            string code,
            string unit,
            string name,
            string notes,
            string dayOrNight,
            int level,
            List<BudgetPCCEsItem> pccItems)
        {
            var pccItem = pccItems.FirstOrDefault(x => x.Code == code);
            if (pccItem != null)
            {

                Unit = pccItem.Unit!;
                if (dayOrNight == "D")
                {
                    ItemCode = pccItem.DayCode!;
                    Description = pccItem.DayName!;
                }
                else
                {
                    ItemCode = pccItem.NightCode!;
                    Description = pccItem.NightName!;
                }
            }
            else
            {
                Unit = unit!;
                Description = $"{name}, {notes}, {(dayOrNight == "D" ? "(日間)" : "(夜間)")}";
                ItemCode = code;
                var prfix = ItemCode.Substring(0, 1);
                if (prfix == "O")
                    prfix = "M";  //將其它歸納至材料類
                else if (prfix == "P")
                    prfix = "L";  //將其它歸納至材料類
                else if (prfix == "A")
                    prfix = "W";  //將其它歸納至耗損類
                ItemCode = $"{prfix}02252{ItemCode}{dayOrNight}";
            }

            ItemKind = "general";
            if (level == 1)
            {
                AnalysisOutputQuantity = 1;
            }
            //level = 2 表示組合單價的單價分析表,參照到一般單價的單價分析表,然後它的成員有資源統計表 ;
            //level = -1 資源統計表不需此屬性
            else
            {
                AnalysisOutputQuantity = 0;
            }
        }

    }


}

    
