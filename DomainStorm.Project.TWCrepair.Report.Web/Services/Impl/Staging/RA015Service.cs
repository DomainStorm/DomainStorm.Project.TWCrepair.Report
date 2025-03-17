using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA015.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 發包-資源統計表
/// </summary>
public class RA015Service : IGetService<RA015, string>
{
    private readonly GetRepository<IRepository<BudgetDocOutSource>> _getRepository;
    private readonly IGetService<BudgetDocOutSourceResourceStatistics, Guid> _getStatisticService;
    private readonly IMapper _mapper;

    public RA015Service(
        GetRepository<IRepository<BudgetDocOutSource>> getRepository,
        IGetService<BudgetDocOutSourceResourceStatistics, Guid> getStatisticService,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _getStatisticService = getStatisticService;
        _mapper = mapper;
    }

    public Task<RA015> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA015> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA015 e => QueryRA015(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA015> QueryRA015(QueryRA015 condition) 
    {
        
        var budgetDocOutSource = await _getRepository().GetAsync(condition.Id);
        var result = _mapper.Map<RA015>(budgetDocOutSource);
        var statistic = await _getStatisticService.GetAsync(condition.Id);
        result.BudgetDocOutSourceResourceStatisticsItems = statistic.BudgetDocOutSourceResourceStatisticsItems;

        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA015[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA015[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA015[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
