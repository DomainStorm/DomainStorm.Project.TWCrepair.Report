using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA063.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 預算書-XML
/// </summary>
public class RA063Service : IGetService<RA063, string>
{
    private readonly GetRepository<IRepository<BudgetDoc>> _getRepository;
    private readonly IMapper _mapper;
    private readonly IGetService<Department, string> _departmentService;
    private readonly GetRepository<IRepository<BudgetPCCEsItem>> _getPCCRepository;
    private readonly IGetService<BudgetDocResourceStatistics, Guid> _getStatisticService;

    public RA063Service(
        GetRepository<IRepository<BudgetDoc>> getRepository,
        IMapper mapper,
        IGetService<Department, string> departmentService,
        GetRepository<IRepository<BudgetPCCEsItem>> getPCCRepository,
        IGetService<BudgetDocResourceStatistics, Guid> getStatisticService)
    {
        _getRepository = getRepository;
        _mapper = mapper;
        _departmentService = departmentService;
        _getPCCRepository = getPCCRepository;
        _getStatisticService = getStatisticService;
    }

    public Task<RA063> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA063> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA063 e => QueryRA063(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA063> QueryRA063(QueryRA063 condition) 
    {
        var budgetDoc = await _getRepository().GetAsync(condition.Id);
        budgetDoc.BudgetDocUnitPrices = budgetDoc.BudgetDocUnitPrices
            .Where(x => x.DayAmount > 0 || x.NightAmount > 0)
            .OrderBy(x => x.Sort).ToList();
        var result = _mapper.Map<RA063>(budgetDoc);
        

        if(condition.XmlKind == "1")
        {
            result.DocumentType = "budget";
        }
        else if (condition.XmlKind == "2")
        {
            result.DocumentType = "request";
        }
        else if (condition.XmlKind == "3")
        {
            result.DocumentType = "submit";
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof (condition), condition, null);
        }

        var pccItems = (await _getPCCRepository().GetListAsync(x => true)).ToList();
        if(!pccItems.Any())
        {
            throw new Exception("查無工程會代碼轉換表,請先匯入");
        }


        //基本資料
        result.PrintDate = DateTime.Today;
        result.TotalPriceStr = GetMoneyStr(result.TotalPrice?.ToString() ?? "");
        var department = await _departmentService.GetAsync(budgetDoc.DepartmentId.ToString());
        result.OrgName = department.OrgName!;
        result.OrgCode = department.OrgCode!;
        result.Address = department.Address!;

        //移除 500 項目
        result.DetailItems!.RemoveAll(x => x.Code == "500");

        
        //將詳細表的日夜間數字轉成物件
        foreach (var item in result.DetailItems)
        {
            item.GenerateDayOrNightParts(
                pccItems, 
                budgetDoc.BudgetDocUnitPrices.FirstOrDefault(x => x.Code == item.Code),
                result);
        }

        //將 500 , 及 9XX 的項目轉成物件清單,xml 比較好處理 
        //901小計不轉, 它的xml 結構比較不一樣
        //500
        result.Detail9XXItems.Add(new RA063Detail9XXItem
        {
            Name = "職業安全衛生費",
            Amout = budgetDoc.Detail_SafetyAndHealthAmount,
            UnitPrice = budgetDoc.Detail_SafetyAndHealthUnitPrice,
            TotalPrice = budgetDoc.Detail_SafetyAndHealthPrice
        });
        //902
        result.Detail9XXItems.Add(new RA063Detail9XXItem
        {
            Name = "供給材料保管及廢料處理補助費",
            Amout = 1,
            UnitPrice = budgetDoc.Detail_MaterialCustodyPrice,
            TotalPrice = budgetDoc.Detail_MaterialCustodyPrice
        });
        //903
        result.Detail9XXItems.Add(new RA063Detail9XXItem
        {
            Name = "承商管理、工程保險修漏作業補助費",
            Amout = 1,
            UnitPrice = budgetDoc.Detail_InsuranceSubsidyForFixPrice,
            TotalPrice = budgetDoc.Detail_InsuranceSubsidyForFixPrice
        });
        //906
        result.Detail9XXItems.Add(new RA063Detail9XXItem
        {
            Name = "排放濁水作業費(依實際施作給付)",
            Amout = 1,
            UnitPrice = budgetDoc.Detail_TurbidWaterPrice,
            TotalPrice = budgetDoc.Detail_TurbidWaterPrice
        });
        //914
        result.Detail9XXItems.Add(new RA063Detail9XXItem
        {
            Name = "品管費",
            Amout = 1,
            UnitPrice = budgetDoc.Detail_QualityControlPrice,
            TotalPrice = budgetDoc.Detail_QualityControlPrice
        });
        //907
        result.Detail9XXItems.Add(new RA063Detail9XXItem
        {
            Name = "承商管理及工程保險補助費",
            Amout = 1,
            UnitPrice = budgetDoc.Detail_InsuranceSubsidyPrice,
            TotalPrice = budgetDoc.Detail_InsuranceSubsidyPrice
        });
        //908
        result.Detail9XXItems.Add(new RA063Detail9XXItem
        {
            Name = "利潤及什費",
            Amout = 1,
            UnitPrice = budgetDoc.Detail_ProfitPrice,
            TotalPrice = budgetDoc.Detail_ProfitPrice
        });

        //取得資源統計表
        var statistic = await _getStatisticService.GetAsync(condition.Id);
        //預算書的資源統計表改成顯示全部(含數量=0 者),但報表不需顯示,故排除之
        var statisticsItems = statistic.BudgetDocResourceStatisticsItems.Where(
            x => x.Category.Name != "職安類"  //只列印非職安類
            && (x.DayAmount > 0 || x.NightAmount > 0)).ToList();
        
        foreach(var statisticsItem in statisticsItems)
        {
            //日夜間要作成不同的兩筆資料
            if(statisticsItem.DayAmount > 0)
            {
                result.ResourceItems.Add(new RA063WorkItem(
                    statisticsItem,
                    "D",
                    pccItems
                    ));
            }
            if (statisticsItem.NightAmount > 0)
            {
                result.ResourceItems.Add(new RA063WorkItem(
                    statisticsItem,
                    "N",
                    pccItems
                    ));
            }

        }





        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA063[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA063[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA063[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }






    #region "將數字轉換成文字"
    public static string GetMoneyStr(string value)
    {
        string str = "";
        string str2 = value.ToString().Trim();
        int num = 0;
        while (str2.Length > 0)
        {
            switch (str2.Substring(str2.Length - 1, 1))
            {
                case "1":
                    str = "壹" + str;
                    break;

                case "2":
                    str = "貳" + str;
                    break;

                case "3":
                    str = "參" + str;
                    break;

                case "4":
                    str = "肆" + str;
                    break;

                case "5":
                    str = "伍" + str;
                    break;

                case "6":
                    str = "陸" + str;
                    break;

                case "7":
                    str = "柒" + str;
                    break;

                case "8":
                    str = "捌" + str;
                    break;

                case "9":
                    str = "玖" + str;
                    break;

                case "0":
                    if (num != 0)
                    {
                        str = "零" + str;
                    }
                    break;
            }
            switch (num)
            {
                case 0:
                    str = "拾" + str;
                    break;

                case 1:
                    str = "佰" + str;
                    break;

                case 2:
                    str = "仟" + str;
                    break;

                case 3:
                    str = "萬" + str;
                    break;

                case 4:
                    str = "拾" + str;
                    break;

                case 5:
                    str = "佰" + str;
                    break;

                case 6:
                    str = "仟" + str;
                    break;

                case 7:
                    str = "億" + str;
                    break;

                case 8:
                    str = "拾" + str;
                    break;

                case 9:
                    str = "佰" + str;
                    break;

                case 10:
                    str = "仟" + str;
                    break;

                case 11:
                    str = "兆" + str;
                    break;

                case 12:
                    str = "拾" + str;
                    break;

                case 13:
                    str = "佰" + str;
                    break;

                case 14:
                    str = "仟" + str;
                    break;
            }
            num++;
            str2 = str2.Substring(0, str2.Length - 1);
        }
        str = str.Substring(1);
        bool flag = true;
        while (flag)
        {
            if (((str.IndexOf("零拾") > -1) || (str.IndexOf("零佰") > -1)) || (((str.IndexOf("零仟") > -1) || (str.IndexOf("零萬") > -1)) || (str.IndexOf("零零") > -1)))
            {
                if (str.IndexOf("零拾") > -1)
                {
                    str = str.Replace("零拾", "零");
                }
                if (str.IndexOf("零佰") > -1)
                {
                    str = str.Replace("零佰", "零");
                }
                if (str.IndexOf("零仟") > -1)
                {
                    str = str.Replace("零仟", "零");
                }
                if (str.IndexOf("零萬") > -1)
                {
                    str = str.Replace("零萬", "萬");
                }
                if (str.IndexOf("零零") > -1)
                {
                    str = str.Replace("零零", "零");
                }
                if ((str.Length > 1) && (str.Substring(str.Length - 1, 1) == "零"))
                {
                    str = str.Substring(0, str.Length - 1);
                }
            }
            else
            {
                flag = false;
            }
        }
        if (str.Length == 0)
        {
            str = "零";
        }
        return (str + "元整");
    }
    #endregion
}