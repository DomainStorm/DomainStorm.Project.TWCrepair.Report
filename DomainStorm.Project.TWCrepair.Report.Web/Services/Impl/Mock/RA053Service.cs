using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA053.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-五.作業前後水壓比較表
/// </summary>
public class RA053Service : IGetService<RA053, string>
{

    public Task<RA053> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA053> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA053 e => QueryRA053(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private  Task<RA053> QueryRA053(QueryRA053 condition)
    {
        var result = new RA053
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

    public Task<RA053[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA053[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA053[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
