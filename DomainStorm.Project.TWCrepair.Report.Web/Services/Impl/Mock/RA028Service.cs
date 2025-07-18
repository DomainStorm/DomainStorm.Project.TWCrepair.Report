using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA028.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-工作計畫
/// </summary>
public class RA028Service : IGetService<RA028, string>
{
    public Task<RA028> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA028> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA028 e => QueryRA028(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private  Task<RA028> QueryRA028(QueryRA028 condition)
    {
        var result = new RA028
        {
            YearPlanReportInstruments = new List<RA028Instrument>()
            {
            }
        };
        return Task.FromResult(result);
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA028[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA028[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA028[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
