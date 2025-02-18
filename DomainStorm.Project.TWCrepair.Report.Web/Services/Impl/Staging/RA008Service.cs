using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using FluentValidation;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA008.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 預算書-進度表
/// </summary>
public class RA008Service : IGetService<RA008, string>
{
    private readonly GetRepository<IRepository<BudgetDoc>> _getRepository;
    private readonly IMapper _mapper;

    public RA008Service(
        GetRepository<IRepository<BudgetDoc>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA008> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA008> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA008 e => QueryRA008(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA008> QueryRA008(QueryRA008 condition) 
    {
        var budgetDoc = await _getRepository().GetAsync(condition.Id);
        var result = _mapper.Map<RA008>(budgetDoc);
        result.PrintDate = DateTime.Today;
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA008[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA008[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA008[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
