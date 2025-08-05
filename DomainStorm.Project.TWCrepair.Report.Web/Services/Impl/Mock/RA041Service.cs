using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA041.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 流量分析-檢前總表/檢後總表
/// </summary>
public class RA041Service : IGetService<RA041, string>, IGetService<RA041MeasureDate, Guid>
{
   

    public Task<RA041> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA041> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA041 e => QueryRA041(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA041> QueryRA041(QueryRA041 condition)
    {
        var result = new RA041();
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA041[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA041[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA041[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate> IGetService<RA041MeasureDate, Guid>.GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate> IGetService<RA041MeasureDate, Guid>.GetAsync<TQuery>(IQuery condition)
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate[]> IGetService<RA041MeasureDate, Guid>.GetListAsync()
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate[]> IGetService<RA041MeasureDate, Guid>.GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate[]> IGetService<RA041MeasureDate, Guid>.GetListAsync<TQuery>(IQuery condition)
    {
        var result = new RA041MeasureDate[]
        {
            new RA041MeasureDate
            {
                MeasureDate = DateTime.Today.AddDays(-1)
            },
            new RA041MeasureDate
            {
                MeasureDate = DateTime.Today
            }
        };
        return Task.FromResult(result);
    }
}
