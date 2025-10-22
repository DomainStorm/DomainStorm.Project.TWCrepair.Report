using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA017.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 合約-單價分析表
/// </summary>
public class RA017Service : IGetService<RA017, string>
{
    private readonly GetRepository<IRepository<BudgetDocContract>> _getRepository;
    private readonly IMapper _mapper;

    public RA017Service(
        GetRepository<IRepository<BudgetDocContract>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

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
        
        var budgetDocContract = await _getRepository().GetAsync(condition.Id);
        budgetDocContract.BudgetDocContractUnitPrices = budgetDocContract.BudgetDocContractUnitPrices
            .Where(x => x.DayAmount > 0 || x.NightAmount > 0)
            .OrderBy(x => x.Code).ToList();
        foreach(var up in budgetDocContract.BudgetDocContractUnitPrices)
        {
            up.BudgetDocContractUnitPriceMembers = up.BudgetDocContractUnitPriceMembers.OrderBy(x => x.Sort).ToList();
            if (up.UnitAmount == 0) 
                up.UnitAmount = 1;
        }
        var result = _mapper.Map<RA017>(budgetDocContract);
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
