using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.Budget;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA011.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 預算書-單價分析表
/// </summary>
public class RA011Service : IGetService<RA011, string>
{
    private readonly GetRepository<IRepository<BudgetDoc>> _getRepository;
    private readonly IMapper _mapper;

    public RA011Service(
        GetRepository<IRepository<BudgetDoc>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
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
        
        var result = new RA011
        {
            PrintDate = DateTime.Today,
            EngineeringName = "台中給水廠東工區管線設備修理工程",
            DepartmentName = "台中給水廠",
            BudgetDocResourceStatisticsItems = new List<TWCrepair.Shared.ViewModel.BudgetDocResourceStatisticsItem>
            {
                new ()
                {
                    Category = new TWCrepair.Shared.ViewModel.Word
                    {
                        Name = "人工類"
                    },
                    Code = "P00001",
                    Name = "技術工",
                    Unit = "工",
                    DayAmount = 3254.1289M,
                    NightAmount = 132.6059M,
                    DayUnitPrice = 2600,
                    NightUnitPrice = 3900,
                    Magnification = 1.5M,
                    DayPrice = 8460735.09M,
                    NightPrice = 517162.93M,
                }
            }
        };
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
