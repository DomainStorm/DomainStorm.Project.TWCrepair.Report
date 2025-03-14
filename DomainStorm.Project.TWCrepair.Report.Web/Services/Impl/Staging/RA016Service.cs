using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA016.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 合約-詳細表
/// </summary>
public class RA016Service : IGetService<RA016, string>
{
    private readonly GetRepository<IRepository<BudgetDocContract>> _getRepository;
    private readonly IMapper _mapper;

    public RA016Service(
        GetRepository<IRepository<BudgetDocContract>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA016> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA016> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA016 e => QueryRA016(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA016> QueryRA016(QueryRA016 condition) 
    {
        
        var budgetDocContract = await _getRepository().GetAsync(condition.Id);
        budgetDocContract.BudgetDocContractUnitPrices = budgetDocContract.BudgetDocContractUnitPrices.OrderBy(x => x.Code).ToList();
        var result = new RA016
        {
            PrintDate = DateTime.Today,
            BudgetDocContractDetail = _mapper.Map<BudgetDocContractDetail>(budgetDocContract)
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA016[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA016[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA016[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
