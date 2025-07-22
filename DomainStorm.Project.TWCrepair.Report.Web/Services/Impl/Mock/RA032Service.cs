using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA032.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-附表二、檢漏工作計劃表
/// </summary>
public class RA032Service : IGetService<RA032, string>
{
   
    public Task<RA032> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA032> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA032 e => QueryRA032(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA032> QueryRA032(QueryRA032 condition)
    {
        
        var result = new RA032
        {
            DepartmentName = "四區處",
            Year = 114
        };

        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA032[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA032[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA032[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
