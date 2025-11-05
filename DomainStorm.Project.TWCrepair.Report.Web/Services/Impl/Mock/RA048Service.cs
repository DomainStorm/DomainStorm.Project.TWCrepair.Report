using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA048.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-目錄
/// </summary>
public class RA048Service : IGetService<RA048, string>
{
   

    public Task<RA048> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA048> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA048 e => QueryRA048(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private Task<RA048> QueryRA048(QueryRA048 condition)
    {
        var result = new RA048
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

    public Task<RA048[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA048[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA048[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
