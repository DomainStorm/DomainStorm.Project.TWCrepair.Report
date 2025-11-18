using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA030.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-計劃經費
/// </summary>
public class RA030Service : IGetService<RA030, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<YearPlanBase>> _getPlanBaseRepository;


    public RA030Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
         GetRepository<IRepository<YearPlanBase>> getPlanBaseRepository
        )
    {
        _getRepository = getRepository;
        _getPlanBaseRepository = getPlanBaseRepository;
    }

    public Task<RA030> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA030> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA030 e => QueryRA030(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA030> QueryRA030(QueryRA030 condition)
    {
        var result = new RA030();
        var planReport = await condition.GetModel(_getRepository(),_getPlanBaseRepository());
        if(planReport != null)
        {
            planReport.YearPlanReportInstruments = planReport.YearPlanReportInstruments.OrderBy(x => x.Sort).ToList();
            result.Conclusion = planReport.Conclusion;
            result.Funding = $"{planReport.Funding:n0}";
            result.Benefit = $"{planReport.Benefit:n0}";
        }
        
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA030[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA030[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA030[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
