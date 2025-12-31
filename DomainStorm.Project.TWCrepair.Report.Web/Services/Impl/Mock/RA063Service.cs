using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA063.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 預算書-XML
/// </summary>
public class RA063Service : IGetService<RA063, string>
{
    
    public Task<RA063> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA063> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA063 e => QueryRA063(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private  Task<RA063> QueryRA063(QueryRA063 condition) 
    {
        var result = new RA063();

        return Task.FromResult(result);
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA063[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA063[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA063[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

}