using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using FluentValidation;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA012.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 發包-進度表
/// </summary>
public class RA012Service : IGetService<RA012, string>
{
    private readonly GetRepository<IRepository<BudgetDocOutSource>> _getRepository;
    private readonly IMapper _mapper;

    public RA012Service(
        GetRepository<IRepository<BudgetDocOutSource>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA012> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA012> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA012 e => QueryRA012(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA012> QueryRA012(QueryRA012 condition) 
    {
        var budgetDocOutSource = await _getRepository().GetAsync(condition.Id);
        var result = _mapper.Map<RA012>(budgetDocOutSource);
        result.PrintDate = DateTime.Today;
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA012[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA012[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA012[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
