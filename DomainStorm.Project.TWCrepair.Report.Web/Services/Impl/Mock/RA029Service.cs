using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA029.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-作業概要
/// </summary>
public class RA029Service : IGetService<RA029, string>
{
    

    public Task<RA029> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA029> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA029 e => QueryRA029(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA029> QueryRA029(QueryRA029 condition)
    {

        var result = new RA029();
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA029[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA029[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA029[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
