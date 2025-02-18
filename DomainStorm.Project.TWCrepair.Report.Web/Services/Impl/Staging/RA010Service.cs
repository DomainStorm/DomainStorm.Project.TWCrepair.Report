using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA010.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

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
        
        var budgetDoc = await _getRepository().GetAsync(condition.Id);
        budgetDoc.BudgetDocUnitPrices = budgetDoc.BudgetDocUnitPrices.OrderBy(x => x.Code).ToList();
        foreach(var up in budgetDoc.BudgetDocUnitPrices)
        {
            up.BudgetDocUnitPriceMembers = up.BudgetDocUnitPriceMembers.OrderBy(x => x.Sort).ToList();
            if (up.UnitAmount == 0) 
                up.UnitAmount = 1;
        }
        var result = _mapper.Map<RA010>(budgetDoc);
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
