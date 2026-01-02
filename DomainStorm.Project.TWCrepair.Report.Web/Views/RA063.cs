using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        /// 預算書所屬區處名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 預算書所屬廠所名稱
        /// </summary>
        public string SiteName { get; set; }

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
        /// 工程地點
        /// </summary>
        public string? EngineeringLocation { get; set; }

        /// <summary>
        /// 施工方法
        /// </summary>
        public string? EngineeringMethod { get; set; }

        /// <summary>
        /// 工程概要
        /// </summary>
        public string? EngineeringSummary { get; set; }

        /// <summary>
        /// 會計科目
        /// </summary>
        public string? AccountingAccount { get; set; }

        /// <summary>
        /// 預計開工日期
        /// </summary>
        public string? PlanStartDate { get; set; }

        /// <summary>
        /// 預計完工日期
        /// </summary>
        public string? PlanEndDate { get; set; }


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

        
        /// <summary>
        /// 設計圖張數
        /// </summary>
        public string? DesignDrawingAmount { get; set; }

        /// <summary>
        /// 說明書頁數
        /// </summary>
        public string? ManualAmount { get; set; }

        /// <summary>
        /// 計算書頁數
        /// </summary>
        public string? CalculationBookAmount { get; set; }

        /// <summary>
        /// 詳細表頁數
        /// </summary>
        public string? DetailTableAmount { get; set; }

        /// <summary>
        /// 單價分析表頁數
        /// </summary>
        public string? UnitPriceAmount { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string? Notes { get; set; }


        
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
            Repository.Models.Budget.BudgetDocUnitPrice unitPrice )
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
                    pccItems
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
                    pccItems
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

        public decimal Amount { get; set ; } 
        public int UnitPrice { get; set; }
        public decimal TotalPrice
        {
            get => Amount * UnitPrice;
        }

        public List<RA063UnitPriceMember> UnitPriceMembers { get; set; } = new List<RA063UnitPriceMember>();


        public Guid BudgetDocUnitPriceId { get; set; }

        public RA063DetailItemDayOrNightPart(
            RA063DetailItem item , 
            decimal amount ,
            BudgetPCCEsItem pccItem, 
            Repository.Models.Budget.BudgetDocUnitPrice unitPrice,  
            string dayOrNight,
            List<BudgetPCCEsItem> pccItems)
        {
            BudgetDocUnitPriceId = unitPrice.Id;
            Code = $"025520{item.Code}{dayOrNight}";
            Name = item.Name;
            Unit = item.Unit ;
            Amount = amount;


            //若有定義工程會的代碼,轉換成工程會的
            if (pccItem != null)
            {
                Unit = pccItem.Unit;
                if (dayOrNight == "D")
                {
                    Code = pccItem.DayCode;
                    Name = pccItem.DayName;
                }
                else
                {
                    Code = pccItem.NightCode;
                    Name = pccItem.NightName;
                }
            }

            //處理成員
            if(dayOrNight == "D")
            {
                var members = unitPrice.BudgetDocUnitPriceMembers.Where(x => x.DayAmount > 0);
                foreach(var member in members)
                {
                    UnitPriceMembers.Add(new RA063UnitPriceMember(member, dayOrNight, pccItems)); 
                }
            }
            else
            {
                var members = unitPrice.BudgetDocUnitPriceMembers.Where(x => x.NightAmount > 0);
                foreach (var member in members)
                {
                    UnitPriceMembers.Add(new RA063UnitPriceMember(member, dayOrNight, pccItems));
                }

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


    public class RA063UnitPriceMember
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



        public List<RA063UnitPriceMember> UnitPriceMembers { get; set; } = new List<RA063UnitPriceMember>();
        
       

        public RA063UnitPriceMember(
            Repository.Models.Budget.BudgetDocUnitPriceMember member , 
            string dayOrNight,
            List<BudgetPCCEsItem> pccItems,
            int level = 1) 
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
                    pccItems
                    );
            }
        }

      

        public void LoadData(
            Repository.Models.Budget.BudgetDocUnitPrice unitPrice,
            string dayOrNight,
            int level,
            List<BudgetPCCEsItem> pccItems)
        {
            var pccItem = pccItems.FirstOrDefault(x => x.Code == unitPrice.Code);
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

            ItemKind = "analysis";
            AnalysisOutputQuantity = unitPrice.UnitAmount;
            foreach (var member in unitPrice.BudgetDocUnitPriceMembers)
            {
                var childMember = new RA063UnitPriceMember(
                    member,
                    dayOrNight,
                    pccItems,
                    level + 1
                    );

                UnitPriceMembers.Add( childMember );

            }
        }


        public void LoadData(  
            Repository.Models.Budget.ResourceWorkMaterial material, 
            string dayOrNight, 
            int level, 
            List<BudgetPCCEsItem> pccItems)
        {
            var pccItem = pccItems.FirstOrDefault(x => x.Code == material.Code);
            if (pccItem != null)
            {
                Unit = pccItem.Unit!;
                if(dayOrNight =="D")
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
                Unit = material.Unit!;
                Description = material.Name;
                ItemCode = material.Code;
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
            if(level == 1)
            {
                AnalysisOutputQuantity = 1;
            }
            else //level = 2 表示組合單價的單價分析表,參照到一般單價的單價分析表,然後它的成員有資源統計表
            {
                AnalysisOutputQuantity = 0;
            }
        }
    }


}

    
