using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA034.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-附表四、檢漏作業計劃差旅費分析表
/// </summary>
public class RA034Service : IGetService<RA034, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> _getZoneRepository;
    private IMapper _mapper;

    public RA034Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> getZoneRepository,
        IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getZoneRepository = getZoneRepository;
        _mapper = mapper;
    }

    public Task<RA034> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA034> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA034 e => QueryRA034(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA034> QueryRA034(QueryRA034 condition)
    {
        var planReport = await _getRepository().GetAsync(condition.Id);

        var result = new RA034
        {
            DepartmentName = planReport.DepartmentName,
            Year = planReport.Year - 1911,
        };

       
        

        if (planReport.YearPlanBase != null)
        {
            result.CurrentPeople1 = planReport.YearPlanBase.CurrentPeople1;
            result.CurrentPeople2 = planReport.YearPlanBase.CurrentPeople2;
            result.CurrentPeople3 = planReport.YearPlanBase.CurrentPeople3;

            

        }
        return result;



       
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA034[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA034[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA034[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
