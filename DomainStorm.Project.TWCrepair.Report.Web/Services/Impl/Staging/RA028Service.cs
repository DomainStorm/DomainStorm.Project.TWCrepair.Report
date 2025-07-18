using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA028.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-工作計畫
/// </summary>
public class RA028Service : IGetService<RA028, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly IMapper _mapper;

    public RA028Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        IMapper mapper
        )
    {
        _getRepository = getRepository;
        _mapper = mapper;
        
    }

    public Task<RA028> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA028> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA028 e => QueryRA028(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA028> QueryRA028(QueryRA028 condition)
    {
        var planReport = await _getRepository().GetAsync(condition.Id);
        planReport.YearPlanReportInstruments = planReport.YearPlanReportInstruments.OrderBy(x => x.Sort).ToList();
        var result = _mapper.Map<RA028>(planReport);
        if(planReport.YearPlanBase != null && planReport.YearPlanBase.YearPlanWorkSpaces != null)
        {
            var min = planReport.YearPlanBase.YearPlanWorkSpaces.Where(x => x.LeakageLowerTarget.HasValue).Min(x => x.LeakageLowerTarget!.Value);
            var max = planReport.YearPlanBase.YearPlanWorkSpaces.Where(x => x.LeakageLowerTarget.HasValue).Max(x => x.LeakageLowerTarget!.Value);
            result.WorkSpace_LeakageLowerTarget = $"{min}%～{max}%";

            result.WorkSpace_CheckOutCMD = planReport.YearPlanBase.YearPlanWorkSpaces.Where(x => x.CheckOutCMD.HasValue).Sum(x => x.CheckOutCMD!.Value);
            result.WorkSpace_PipeLength = planReport.YearPlanBase.YearPlanWorkSpaces.Where(x => x.PlanPipeLength.HasValue).Sum(x => x.PlanPipeLength!.Value);
            result.WorkSpace_CheckOutAmountDistributionPipe = planReport.YearPlanBase.YearPlanWorkSpaces.Where(x => x.CheckOutAmountDistributionPipe.HasValue).Sum(x => x.CheckOutAmountDistributionPipe!.Value);
            result.WorkSpace_CheckOutAmountOutdoorPipe = planReport.YearPlanBase.YearPlanWorkSpaces.Where(x => x.CheckOutAmountOutdoorPipe.HasValue).Sum(x => x.CheckOutAmountOutdoorPipe!.Value);
        }
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA028[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA028[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA028[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
