using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA009.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 預算書-詳細表
/// </summary>
public class RA009Service : IGetService<RA009, string>
{
    private readonly GetRepository<IRepository<BudgetDoc>> _getRepository;
    private readonly IMapper _mapper;

    public RA009Service(
        GetRepository<IRepository<BudgetDoc>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA009> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA009> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA009 e => QueryRA009(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA009> QueryRA009(QueryRA009 condition) 
    {
        
        var budgetDoc = await _getRepository().GetAsync(condition.Id);
        budgetDoc.BudgetDocUnitPrices = budgetDoc.BudgetDocUnitPrices.OrderBy(x => x.Code).ToList();
        var result = new RA009
        {
            PrintDate = DateTime.Today,
            BudgetDocDetail = _mapper.Map<BudgetDocDetail>(budgetDoc)
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA009[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA009[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA009[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
