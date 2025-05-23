using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA020.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 漏水原因分析表
/// </summary>
public class RA020Service : IGetService<RA020, string>
{
    

    public Task<RA020> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA020> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA020 e => QueryRA020(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA020> QueryRA020(QueryRA020 condition) 
    {
        
        var result = new RA020();


        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA020[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA020[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA020[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
