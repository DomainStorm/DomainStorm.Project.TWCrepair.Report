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
    private readonly GetRepository<IRepository<BudgetDoc>> _getRepository;
    private readonly IGetService<BudgetDocResourceStatistics, Guid> _getStatisticService;
    private readonly IMapper _mapper;

    public RA015Service(
        GetRepository<IRepository<BudgetDoc>> getRepository,
        IGetService<BudgetDocResourceStatistics, Guid> getStatisticService,
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
        
        var budgetDoc = await _getRepository().GetAsync(condition.Id);
        var result = _mapper.Map<RA015>(budgetDoc);
        var statistic = await _getStatisticService.GetAsync(condition.Id);
        //預算書的資源統計表改成顯示全部(含數量=0 者),但報表不需顯示,故排除之
        result.BudgetDocResourceStatisticsItems = statistic.BudgetDocResourceStatisticsItems.Where(
            x => x.Category.Name != "職安類"  //只列印非職安類
            && (x.DayAmount > 0 || x.NightAmount > 0)).ToList();
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
