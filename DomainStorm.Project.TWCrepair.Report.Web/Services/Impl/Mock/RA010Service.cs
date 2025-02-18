using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA010.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 預算書-單價分析表
/// </summary>
public class RA010Service : IGetService<RA010, string>
{
    private readonly GetRepository<IRepository<BudgetDoc>> _getRepository;
    private readonly IMapper _mapper;

    public RA010Service(
        GetRepository<IRepository<BudgetDoc>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA010> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA010> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA010 e => QueryRA010(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA010> QueryRA010(QueryRA010 condition) 
    {
        
        var result = new RA010
        {
            PrintDate = DateTime.Today,
            EngineeringName = "台中給水廠東工區管線設備修理工程",
            DepartmentName = "台中給水廠",
            BudgetDocUnitPrices = new List<TWCrepair.Shared.ViewModel.BudgetDocUnitPrice>
            {
                new TWCrepair.Shared.ViewModel.BudgetDocUnitPrice()
                {
                    Code = "026",
                    Name = "消防栓盒升降或換新",
                    isCombine = true,
                    Unit ="處",
                    UnitAmount = 1M,
                    BudgetDocUnitPriceMembers = new List<TWCrepair.Shared.ViewModel.BudgetDocUnitPriceMember>
                    {
                        new TWCrepair.Shared.ViewModel.BudgetDocUnitPriceMember()
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

    public Task<RA010[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA010[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA010[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
