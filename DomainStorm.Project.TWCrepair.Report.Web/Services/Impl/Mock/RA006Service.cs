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
        var result = new RA006();
        result.PrintDate = DateTime.Today;

        
        

        
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
