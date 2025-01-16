using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using FluentValidation;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA003.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

public class RA003Service : IGetService<RA003, string>
{
    private readonly GetRepository<IRepository<WaterPressureCheck>> _getRepository;

    public RA003Service(GetRepository<IRepository<WaterPressureCheck>> getRepository)
    {
        _getRepository = getRepository;
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
