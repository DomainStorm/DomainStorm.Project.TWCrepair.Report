using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
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

        public void GenerateDayOrNightParts(List<BudgetPCCEsItem> pccItems, Repository.Models.Budget.BudgetDocUnitPrice unitPrice )
        {
            var pccItem = pccItems.FirstOrDefault(x => x.Code == Code);
            if(DayAmount > 0 )
            {
                var part = new RA063DetailItemDayOrNightPart(
                    this,
                    this.DayAmount,
                    pccItem!,
                    unitPrice,
                    "D"
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
                    "N"
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

        public Guid BudgetDocUnitPriceId { get; set; }

        public RA063DetailItemDayOrNightPart(
            RA063DetailItem item , 
            decimal amount ,
            BudgetPCCEsItem pccItem, 
            Repository.Models.Budget.BudgetDocUnitPrice unitPrice,  
            string DayOrNight)
        {
            BudgetDocUnitPriceId = unitPrice.Id;
            Code = $"025520{item.Code}{DayOrNight}";
            Name = item.Name;
            Unit = item.Unit ;
            Amount = amount;


            //若有定義工程會的代碼,轉換成工程會的
            if (pccItem != null)
            {
                Unit = pccItem.Unit;
                if (DayOrNight == "D")
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
            if(DayOrNight == "D")
            {
                var members = unitPrice.BudgetDocUnitPriceMembers.Where(x => x.DayAmount > 0);
                foreach(var member in members)
                {
                    if(member.ResourceWorkMaterial != null)
                    {
                        //var outputMember = new RA063UnitPriceMember(member.ResourceWorkMaterial);
                    }
                    else
                    {

                    }
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

        public List<RA063UnitPriceMember> UnitPriceMembers { get; set; } = new List<RA063UnitPriceMember>();

    }


    public class RA063UnitPriceMember
    {
        public decimal Amout { get; set; }
        public string Code { get; set; }
        
        public RA063UnitPriceMember()
        {

        }

        public RA063UnitPriceMember(Repository.Models.Budget.ResourceWorkMaterial material, decimal amount)
        {
            Amout = amount;

            //string refItemCodeZ = drdataZ[zz]["resNo"].ToString().Substring(0, 1);

            //if (drdataZ[zz]["resNo"].ToString().Substring(0, 1) == "O")
            //    refItemCodeZ = "M";  //將其它歸納至材料類

            //if (drdataZ[zz]["resNo"].ToString().Substring(0, 1) == "P")
            //    refItemCodeZ = "L";  //將其它歸納至材料類

            //if (drdataZ[zz]["resNo"].ToString().Substring(0, 1) == "A")
            //    refItemCodeZ = "W";  //將其它歸納至耗損類

            //refItemCodeZ += "02252"; //歸屬公共管線之保護大類 + 資源編號
            //refItemCodeZ += drdataZ[zz]["resNo"].ToString().TrimEnd() + "D";
        }
    }


}

    
