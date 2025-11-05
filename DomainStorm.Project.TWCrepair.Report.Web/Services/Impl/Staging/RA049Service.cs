using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA049.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-一.作業成果分析表
/// </summary>
public class RA049Service : IGetService<RA049, string>
{
    private readonly GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> _getRepository;
    private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanBase>> _getPlanRepository;
    private readonly GetRepository<IRepository<Repository.Models.CheckDailyReportDetail>> _getDailyReportRepository;
    private readonly GetRepository<IRepository<Repository.Models.WaterFlowCheck>> _getFlowCheckRepository;
    

    public RA049Service(
       GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> getRepository,
       GetRepository<IRepository<Repository.Models.YearPlan.YearPlanBase>> getPlanRepository,
       GetRepository<IRepository<Repository.Models.CheckDailyReportDetail>> getDailyReportRepository,
       GetRepository<IRepository<Repository.Models.WaterFlowCheck>> getFlowCheckRepository
       )
    {
        _getRepository = getRepository;
        _getPlanRepository = getPlanRepository;
        _getDailyReportRepository = getDailyReportRepository;
        _getFlowCheckRepository = getFlowCheckRepository;
    }

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

    private async Task<RA049> QueryRA049(QueryRA049 condition)
    {
        var dailyReportRepository = _getDailyReportRepository();
        var flowCheckRepository = _getFlowCheckRepository();
        var workspace =  await _getRepository().GetAsync(condition.WorkSpaceId);
        var result = new RA049
        {
            Year = workspace.Year,
            DepartmentName = workspace.DepartmentName,
            WorkSpaceName = workspace.WorkSpaceName
        };

        
        var plan = (await _getPlanRepository().GetListAsync(x => x.Year == workspace.Year && x.DepartmentId == condition.WorkSpaceId))
            .FirstOrDefault();
        if (plan != null)
        {
            
            var planWorkSpace = plan.YearPlanWorkSpaces.FirstOrDefault(x => x.WorkSpaceId == condition.WorkSpaceId);
            if(planWorkSpace != null)
            {
                result.OperationStartDate = planWorkSpace.OperationStartDate;
                result.OperationEndDate = planWorkSpace.OperationEndDate;
                result.Operators = plan.Operators;
                result.WorkDayTotal = ( await dailyReportRepository.GetListAsync(x => x.WorkSpaceId == condition.WorkSpaceId && x.CheckDailyReport.ReportDate.Year == workspace.Year))
                    .Sum(x => x.WorkDayTotal);


                //流量檢查取得同年度最小的點
                result.ThisYearMinFlowData = (await flowCheckRepository.GetListAsync<FlowData>(x => x.WorkSpaceId == condition.WorkSpaceId
                && x.MeasureDate.Year == workspace.Year && x.BeforeOrAfter.Name == "檢修前",
                x => new FlowData
                {
                    LocationNumber = x.LocationNumber,
                    MeasureDate = x.MeasureDate,
                    MinFlow = x.WaterFlowCheckData.Select(x => x.CH1Volumetric).Min()
                }))
                .OrderBy(x => x.MinFlow).FirstOrDefault();
                if(result.ThisYearMinFlowData != null)
                {
                    result.LastYearMinFlowData = (await flowCheckRepository.GetListAsync<FlowData>(x => x.WorkSpaceId == condition.WorkSpaceId
                        && x.MeasureDate.Year < workspace.Year && x.BeforeOrAfter.Name == "檢修後"
                        && x.LocationNumber == result.ThisYearMinFlowData.LocationNumber , //找前期的要找同一地點
                       x => new FlowData
                       {
                           LocationNumber = x.LocationNumber,
                           MeasureDate = x.MeasureDate,
                           MinFlow = x.WaterFlowCheckData.Select(x => x.CH1Volumetric).Min()
                       }))
                       .OrderBy(x => x.MinFlow).FirstOrDefault();
                }





            }
        }


       
        return result;
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
