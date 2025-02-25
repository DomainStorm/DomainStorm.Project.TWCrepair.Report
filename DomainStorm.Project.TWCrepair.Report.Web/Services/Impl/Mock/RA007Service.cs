using System.Globalization;
using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA007.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 預算書-詳細總表
/// </summary>
public class RA007Service : IGetService<RA007, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly IMapper _mapper;

    public RA007Service(
        GetRepository<IRepository<FixForm>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA007> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA007> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA007 e => QueryRA007(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA007> QueryRA007(QueryRA007 condition) 
    {
        var result = new RA007
        {
            PrintDate = DateTime.Now,
            DepartmentName = "台中給水廠",
            EngineeringName = "台中給水廠東工區管線設備修理工程",
            EngineeringPrice = 31760199.00M,
            MaterialPrice = (3000000.00M).ToString(CultureInfo.InvariantCulture),
            SubTotalPrice = 34760199.00M,
            Tax = 1738010.00M,
            TotalPrice= 36498209.00M
        };
        return result;

    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA007[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA007[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA007[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
