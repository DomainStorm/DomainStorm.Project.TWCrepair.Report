using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA049.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-一.作業成果分析表
/// </summary>
public class RA049Service : IGetService<RA049, string>
{
    
    public Task<RA049> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA049> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA049 e => QueryRA049(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private  Task<RA049> QueryRA049(QueryRA049 condition)
    {
       
        var result = new RA049
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

    public Task<RA049[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA049[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA049[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
