using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA017.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 合約-單價分析表
/// </summary>
public class RA017Service : IGetService<RA017, string>
{
    public Task<RA017> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA017> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA017 e => QueryRA017(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA017> QueryRA017(QueryRA017 condition) 
    {
        
        var result = new RA017
        {
            PrintDate = DateTime.Today,
            EngineeringName = "台中給水廠東工區管線設備修理工程",
            DepartmentName = "台中給水廠",
            BudgetDocContractUnitPrices = new List<TWCrepair.Shared.ViewModel.BudgetDocContractUnitPrice>
            {
                new TWCrepair.Shared.ViewModel.BudgetDocContractUnitPrice()
                {
                    Code = "026",
                    Name = "消防栓盒升降或換新",
                    isCombine = true,
                    Unit ="處",
                    UnitAmount = 1M,
                    BudgetDocContractUnitPriceMembers = new List<TWCrepair.Shared.ViewModel.BudgetDocContractUnitPriceMember>
                    {
                        new TWCrepair.Shared.ViewModel.BudgetDocContractUnitPriceMember()
                        {
                            Name = "拆除柏油路面",
                            Description = "10cm以下",
                            Unit="㎡",
                            DayAmount=0.300M,
                            NightAmount = 0.300M,
                            DayUnitPrice = 820,
                            NightUnitPrice = 900,
                            DayPrice = 246M,
                            NightPrice = 270,
                            Notes=""

                        }

                    },
                    Tail = new TWCrepair.Shared.ViewModel.Word
                    {
                        Name = "工具損耗及另料"
                    },
                    TailPersentage = 2M,
                    TailDayPrice = 5,
                    TailNightPrice = 5,
                    TotalDayPrice = 251,
                    TotalNightPrice = 275
                }
            }
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA017[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA017[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA017[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
