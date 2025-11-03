using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA048.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-目錄
/// </summary>
public class RA048Service : IGetService<RA048, string>
{
    private readonly GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> _getRepository;
    
    public RA048Service(
       GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> getRepository
       )
    {
        _getRepository = getRepository;
    }

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

    private async Task<RA048> QueryRA048(QueryRA048 condition)
    {
        var workspace =  await _getRepository().GetAsync(condition.WorkSpaceId);
        var result = new RA048
        {
            Year = workspace.Year,
            DepartmentName = workspace.DepartmentName,
            WorkSpaceName = workspace.WorkSpaceName
        };
        return result;
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
