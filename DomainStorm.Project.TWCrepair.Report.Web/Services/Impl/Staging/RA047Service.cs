using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA047.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-封面
/// </summary>
public class RA047Service : IGetService<RA047, string>
{
    private readonly GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> _getRepository;
    
    public RA047Service(
       GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> getRepository
       )
    {
        _getRepository = getRepository;
    }

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

    private async Task<RA047> QueryRA047(QueryRA047 condition)
    {
        var workspace =  await _getRepository().GetAsync(condition.WorkSpaceId);
        var result = new RA047
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
