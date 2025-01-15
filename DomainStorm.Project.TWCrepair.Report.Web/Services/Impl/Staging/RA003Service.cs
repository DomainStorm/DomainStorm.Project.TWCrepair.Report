using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA003.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

public class RA003Service : IGetService<RA003, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly IMapper _mapper;

    public RA003Service(
        GetRepository<IRepository<FixForm>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA003> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA003> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA003 e => QueryRA003(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA003> QueryRA003(QueryRA003 condition) 
    {
        var result = new RA003();
        result.PrintDate = DateTime.Today;

        var fixForm = await _getRepository().GetAsync(condition.Id);
        fixForm.Sorting();
        _mapper.Map(fixForm, result);
        
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA003[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA003[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA003[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
