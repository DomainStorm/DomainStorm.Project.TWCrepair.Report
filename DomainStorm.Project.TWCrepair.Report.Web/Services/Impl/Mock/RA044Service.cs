using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA044.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 區處執行管控表
/// </summary>
public class RA044Service : IGetService<RA044, string>
{
    

    public Task<RA044> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA044> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA044 e => QueryRA044(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA044> QueryRA044(QueryRA044 condition)
    {
        var result = new RA044();

        

        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA044[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA044[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA044[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
