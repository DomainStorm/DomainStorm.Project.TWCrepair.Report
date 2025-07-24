using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA031.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-附表一 抄見率暨戶日配水量明細表
/// </summary>
public class RA031Service : IGetService<RA031, string>
{
    
    public Task<RA031> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA031> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA031 e => QueryRA031(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA031> QueryRA031(QueryRA031 condition)
    {
       

        var result = new RA031();

        

        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA031[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA031[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA031[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
