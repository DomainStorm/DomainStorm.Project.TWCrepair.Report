using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA006.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 預算書-封面
/// </summary>
public class RA006Service : IGetService<RA006, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly IMapper _mapper;

    public RA006Service(
        GetRepository<IRepository<FixForm>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA006> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA006> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA006 e => QueryRA006(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA006> QueryRA006(QueryRA006 condition) 
    {
        var result = new RA006
        {
            PrintDate = DateTime.Now,
            DepartmentName = "台中給水廠",
            EngineeringName = "台中給水廠東工區管線設備修理工程",
            EngineeringPrice = "31760199.00M",
            MaterialPrice = "3000000.00M",
            SubTotalPrice = "34760199.00M",
            Tax = "1738010.00M",
            TotalPrice = "36498209.00M",
            EngineeringNumber = "W4-114-0401-504",
            EngineeringLocation = "廠所供水轄區",
            EngineeringMethod = "單價發包",
            EngineeringSummary = "台中給水廠東工區管線設備修理工程",
            PlanStartDate = "2025/01/01",
            PlanEndDate ="2025/12/31",
            DetailTableAmount = "7",
            UnitPriceAmount = "43"
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA006[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA006[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA006[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
