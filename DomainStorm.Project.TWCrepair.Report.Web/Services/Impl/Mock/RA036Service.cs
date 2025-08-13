using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using System.IO.Pipelines;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA036.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-附表六、各系統管徑、管長暨附屬設備統計表
/// </summary>
public class RA036Service : IGetService<RA036, string>
{
   
    public Task<RA036> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA036> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA036 e => QueryRA036(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA036> QueryRA036(QueryRA036 condition)
    {
        
        var result = new RA036
        {
            DepartmentName = "第四區處",
            Year = 114,
        };

       

        return result;
    }

   

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA036[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA036[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA036[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
