using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA029.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-作業概要
/// </summary>
public class RA029Service : IGetService<RA029, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<YearPlanBase>> _getPlanBaseRepository;
    private readonly IMapper _mapper;

    public RA029Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<YearPlanBase>> getPlanBaseRepository,
        IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getPlanBaseRepository = getPlanBaseRepository;
        _mapper = mapper;
        
    }

    public Task<RA029> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA029> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA029 e => QueryRA029(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA029> QueryRA029(QueryRA029 condition)
    {
        var planReport = await condition.GetModel(_getRepository(), _getPlanBaseRepository());
        if (planReport != null)
        {
            planReport.YearPlanReportInstruments = planReport.YearPlanReportInstruments.OrderBy(x => x.Sort).ToList();
        }
        
        var result = _mapper.Map<RA029>(planReport);
        if(planReport != null && planReport.YearPlanBase != null && planReport.YearPlanBase.YearPlanWorkSpaces != null)
        {
            result.WorkSpaces = _mapper.Map<List<RA029WorkSpace>>(planReport.YearPlanBase.YearPlanWorkSpaces);
            foreach(var ws in result.WorkSpaces)
            {
                if(ws.OperationStartDate.HasValue && ws.OperationEndDate.HasValue)
                {
                    var beginMonth = ws.OperationStartDate.Value.Month;
                    var endMonth = ws.OperationEndDate.Value.Month;
                    for(int i = 0;i<12 ;i++) 
                    {
                        var month = i + 1;
                        ws.OperationMonthes[i] = month >= beginMonth && month <= endMonth;
                    }
                }
                
            }
        }
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA029[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA029[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA029[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
