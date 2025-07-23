using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA033.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-三、檢漏作業各系統費用分析表
/// </summary>
public class RA033Service : IGetService<RA033, string>
{
   

    public Task<RA033> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA033> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA033 e => QueryRA033(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA033> QueryRA033(QueryRA033 condition)
    {
        
        var result = new RA033
        {
            DepartmentName = "四區處",
            Year = 114,
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA033[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA033[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA033[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
