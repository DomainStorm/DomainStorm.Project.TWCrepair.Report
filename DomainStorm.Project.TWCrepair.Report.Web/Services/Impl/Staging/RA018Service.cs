using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA018.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 合約-資源統計表
/// </summary>
public class RA018Service : IGetService<RA018, string>
{
    private readonly GetRepository<IRepository<BudgetDocContract>> _getRepository;
    private readonly IGetService<BudgetDocContractResourceStatistics, Guid> _getStatisticService;
    private readonly IMapper _mapper;

    public RA018Service(
        GetRepository<IRepository<BudgetDocContract>> getRepository,
        IGetService<BudgetDocContractResourceStatistics, Guid> getStatisticService,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _getStatisticService = getStatisticService;
        _mapper = mapper;
    }

    public Task<RA018> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA018> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA018 e => QueryRA018(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA018> QueryRA018(QueryRA018 condition) 
    {
        
        var budgetDocContract = await _getRepository().GetAsync(condition.Id);
        var result = _mapper.Map<RA018>(budgetDocContract);
        var statistic = await _getStatisticService.GetAsync(condition.Id);
        result.BudgetDocContractResourceStatisticsItems = statistic.BudgetDocContractResourceStatisticsItems;

        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA018[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA018[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA018[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
