using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA051.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-三.檢修漏成果計算資料表
/// </summary>
public class RA051Service : IGetService<RA051, string>
{

    public Task<RA051> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA051> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA051 e => QueryRA051(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private  Task<RA051> QueryRA051(QueryRA051 condition)
    {
        var result = new RA051
        {
            Year = 2022,
            DepartmentName = "台水四區處",
            WorkSpaceName = "東勢系統"
        };


        return Task.FromResult(result);
    }

    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA051[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA051[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA051[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
