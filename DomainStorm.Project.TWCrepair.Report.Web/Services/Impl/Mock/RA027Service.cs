using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA027.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-目錄
/// </summary>
public class RA027Service : IGetService<RA027, string>
{
   

    public Task<RA027> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA027> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA027 e => QueryRA027(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private Task<RA027> QueryRA027(QueryRA027 condition) 
    {
        var result = new RA027
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

    public Task<RA027[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA027[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA027[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
