using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using System.IO.Pipelines;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA037.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-附表七、各系統大區NRW
/// </summary>
public class RA037Service : IGetService<RA037, string>
{
   

    public Task<RA037> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA037> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA037 e => QueryRA037(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private Task<RA037> QueryRA037(QueryRA037 condition)
    {
        var result = new RA037
        {
            DepartmentName = "四區處",
            Year = DateTime.Now.Year - 1911,
        };
        for(int i = 0; i < result.LastYears.Length ; i++)
        {
            result.LastYears[i] = result.Year - result.LastYears.Length + i + 1;
        }

        return  Task.FromResult(result);
    }

   

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA037[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA037[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA037[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
