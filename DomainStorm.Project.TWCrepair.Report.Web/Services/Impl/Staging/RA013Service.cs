using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA013.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 發包-詳細表(估價單)
/// </summary>
public class RA013Service : IGetService<RA013, string>
{
    private readonly GetRepository<IRepository<BudgetDoc>> _getRepository;
    private readonly IMapper _mapper;

    public RA013Service(
        GetRepository<IRepository<BudgetDoc>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA013> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA013> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA013 e => QueryRA013(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA013> QueryRA013(QueryRA013 condition) 
    {
        
        var budgetDoc = await _getRepository().GetAsync(condition.Id);
        budgetDoc.BudgetDocUnitPrices = budgetDoc.BudgetDocUnitPrices
            .Where(x => int.Parse(x.Code) <= 495 || (int.Parse(x.Code) >= 501 && int.Parse(x.Code) <= 899))  //應和 FixWeb 詳細表一致
            .Where(x => x.DayAmount > 0 || x.NightAmount > 0)
            .OrderBy(x => x.Code).ToList();
        var result = new RA013
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

    public Task<RA013[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA013[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA013[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
