using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA014.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 發包-單價分析表
/// </summary>
public class RA014Service : IGetService<RA014, string>
{
    private readonly GetRepository<IRepository<BudgetDoc>> _getRepository;
    private readonly IMapper _mapper;

    public RA014Service(
        GetRepository<IRepository<BudgetDoc>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA014> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA014> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA014 e => QueryRA014(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA014> QueryRA014(QueryRA014 condition) 
    {
        
        var budgetDoc = await _getRepository().GetAsync(condition.Id);
        budgetDoc.BudgetDocUnitPrices = budgetDoc.BudgetDocUnitPrices
            .Where(x => x.DayAmount > 0 || x.NightAmount > 0)
            .OrderBy(x => x.Code).ToList();
        foreach(var up in budgetDoc.BudgetDocUnitPrices)
        {
            up.BudgetDocUnitPriceMembers = up.BudgetDocUnitPriceMembers.OrderBy(x => x.Sort).ToList();
            if (up.UnitAmount == 0) 
                up.UnitAmount = 1;
        }
        var result = _mapper.Map<RA014>(budgetDoc);
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA014[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA014[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA014[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
