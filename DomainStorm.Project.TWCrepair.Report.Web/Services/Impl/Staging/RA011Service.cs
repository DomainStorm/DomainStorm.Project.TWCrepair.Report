using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA011.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 預算書-資源統計表
/// </summary>
public class RA011Service : IGetService<RA011, string>
{
    private readonly GetRepository<IRepository<BudgetDoc>> _getRepository;
    private readonly IGetService<BudgetDocResourceStatistics, Guid> _getStatisticService;
    private readonly IMapper _mapper;

    public RA011Service(
        GetRepository<IRepository<BudgetDoc>> getRepository,
        IGetService<BudgetDocResourceStatistics, Guid> getStatisticService,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _getStatisticService = getStatisticService;
        _mapper = mapper;
    }

    public Task<RA011> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA011> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA011 e => QueryRA011(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA011> QueryRA011(QueryRA011 condition) 
    {
        
        var budgetDoc = await _getRepository().GetAsync(condition.Id);
        var result = _mapper.Map<RA011>(budgetDoc);
        var statistic = await _getStatisticService.GetAsync(condition.Id);
        result.BudgetDocResourceStatisticsItems = statistic.BudgetDocResourceStatisticsItems;

        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA011[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA011[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA011[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
