using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA052.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-四.最小流量率(修正值)比較表
/// </summary>
public class RA052Service : IGetService<RA052, string>
{

    public Task<RA052> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA052> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA052 e => QueryRA052(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private  Task<RA052> QueryRA052(QueryRA052 condition)
    {
        var result = new RA052
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

    public Task<RA052[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA052[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA052[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
