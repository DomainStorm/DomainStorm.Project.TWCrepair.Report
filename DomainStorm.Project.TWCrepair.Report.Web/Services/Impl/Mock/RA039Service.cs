using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA039.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-附表十、儀器需求統計
/// </summary>
public class RA039Service : IGetService<RA039, string>
{
   

    public Task<RA039> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA039> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA039 e => QueryRA039(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA039> QueryRA039(QueryRA039 condition)
    {
        var result = new RA039
        {
            DepartmentName = "四區處",
            OnSitePeople = 7
        };
            return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA039[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA039[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA039[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
