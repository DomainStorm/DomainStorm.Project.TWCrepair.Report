using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA027.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-目錄
/// </summary>
public class RA027Service : IGetService<RA027, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<YearPlanBase>> _getPlanBaseRepository;

    public RA027Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<YearPlanBase>> getPlanBaseRepository
        )
    {
        _getRepository = getRepository;
        _getPlanBaseRepository = getPlanBaseRepository;
    }

    public Task<RA027> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA027> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA027 e => QueryRA027(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA027> QueryRA027(QueryRA027 condition)
    {
        var result = new RA027();
        var plan = await condition.GetModel(_getRepository(), _getPlanBaseRepository());
        if(plan != null) 
        {
            result.Year = plan.Year;
            result.DepartmentName = plan.DepartmentName;
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA027[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA027[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA027[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
