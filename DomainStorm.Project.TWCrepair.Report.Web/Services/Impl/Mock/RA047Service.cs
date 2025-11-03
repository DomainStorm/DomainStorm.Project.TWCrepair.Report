using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA047.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-封面
/// </summary>
public class RA047Service : IGetService<RA047, string>
{
   

    public Task<RA047> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA047> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA047 e => QueryRA047(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private Task<RA047> QueryRA047(QueryRA047 condition)
    {
        var result = new RA047
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

    public Task<RA047[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA047[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA047[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
