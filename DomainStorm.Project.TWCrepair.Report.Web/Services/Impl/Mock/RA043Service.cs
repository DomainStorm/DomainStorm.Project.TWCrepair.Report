using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA043.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 毀損計算營業損失
/// </summary>
public class RA043Service : IGetService<RA043, string>
{
   

    public Task<RA043> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA043> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA043 e => QueryRA043(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA043> QueryRA043(QueryRA043 condition)
    {
        var result = new RA043();
        
       
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA043[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA043[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA043[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
