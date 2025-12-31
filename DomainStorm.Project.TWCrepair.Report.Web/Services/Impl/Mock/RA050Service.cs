using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA050.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-二.檢修漏成果計算統計表
/// </summary>
public class RA050Service : IGetService<RA050, string>
{

    public Task<RA050> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA050> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA050 e => QueryRA050(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private  Task<RA050> QueryRA050(QueryRA050 condition)
    {
        var result = new RA050
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

    public Task<RA050[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA050[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA050[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
