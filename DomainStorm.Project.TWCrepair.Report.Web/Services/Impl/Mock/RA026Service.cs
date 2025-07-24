using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA026.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-封面
/// </summary>
public class RA026Service : IGetService<RA026, string>
{
   

    public Task<RA026> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA026> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA026 e => QueryRA026(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private Task<RA026> QueryRA026(QueryRA026 condition) 
    {
        var result = new RA026
        {
            Year = 2025,
            DepartmentName = "台水四區處"
        };
        return Task.FromResult(result);
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA026[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA026[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA026[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
