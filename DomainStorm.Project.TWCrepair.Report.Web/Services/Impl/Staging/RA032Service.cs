using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA032.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-附表二、檢漏工作計劃表
/// </summary>
public class RA032Service : IGetService<RA032, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<YearPlanBase>> _getPlanBaseRepository;
    private readonly GetRepository<IRepository<YearPlanSetAllZone>> _getZoneRepository;
    private IMapper _mapper;

    public RA032Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<YearPlanSetAllZone>> getZoneRepository,
        GetRepository<IRepository<YearPlanBase>> getPlanBaseRepository,
        IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getZoneRepository = getZoneRepository;
        _getPlanBaseRepository = getPlanBaseRepository;
        _mapper = mapper;
    }

    public Task<RA032> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA032> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA032 e => QueryRA032(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA032> QueryRA032(QueryRA032 condition)
    {
        var result = new RA032();
        var planReport = await condition.GetModel(_getRepository(), _getPlanBaseRepository());
        if (planReport != null)
        {
            result.DepartmentName = planReport.DepartmentName;
            result.Year = planReport.Year - 1911;

            if (planReport.YearPlanBase != null)
            {
                result.CurrentPeople = planReport.YearPlanBase.CurrentPeople;
               
                if (planReport.YearPlanBase.YearPlanWorkSpaces != null)
                {
                    planReport.YearPlanBase.AppendSumItem();
                    result.Items = _mapper.Map<List<RA032Item>>(planReport.YearPlanBase.YearPlanWorkSpaces);
                    //合計列要置頂, 和CheckWeb 不一樣
                    if (result.Items.Any())
                    {
                        result.SumItem = result.Items.Last();
                        result.Items.Remove(result.SumItem);
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

    public Task<RA032[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA032[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA032[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
