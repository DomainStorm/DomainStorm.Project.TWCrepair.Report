using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA031.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-附表一 抄見率暨戶日配水量明細表
/// </summary>
public class RA031Service : IGetService<RA031, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> _getZoneRepository;
    private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanBase>> _getPlanBaseRepository;
    private IMapper _mapper;
    

    public RA031Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> getZoneRepository,
         GetRepository<IRepository<Repository.Models.YearPlan.YearPlanBase>> getPlanBaseRepository,
        IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getZoneRepository = getZoneRepository;
        _getPlanBaseRepository = getPlanBaseRepository;
        _mapper = mapper;
    }

    public Task<RA031> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA031> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA031 e => QueryRA031(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA031> QueryRA031(QueryRA031 condition)
    {
        var result = new RA031();
        var planReport = await condition.GetModel(_getRepository(), _getPlanBaseRepository());

        if (planReport != null)
        {
            result.DepartmentName = planReport.DepartmentName;
            result.Year = planReport.Year - 1911;

            if (planReport.YearPlanBase != null)
            {
                //附表一:要限系統及大區, 排除機動支援 , 和 CheckWeb 的 YearPlanSetAllZoneService 有點不一樣, 不使用 service

                //設定抄見率和年度計畫目前沒有關聯,要重查
                var zones = await _getZoneRepository().GetListAsync(x => x.DepartmentId == planReport.YearPlanBase.DepartmentId
                && x.Year == planReport.YearPlanBase.Year);

                foreach (var zone in zones)
                {
                    //if (zone.WaterSupplySystemName.Contains("機動")) continue;  

                    var item = _mapper.Map<YearPlanStatistics>(zone);
                    item.LoadMonthlyData(zone, 7, 10);
                    result.Items.Add(item);
                }

                //加一個總計資料
                result.Items.Add(YearPlanStatistics.GenerateSumAllSites(result.Items));

                //總計資料置頂
                result.Items = result.Items.OrderByDescending(x => x.SortOrder).ToList();
            }
        }

        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA031[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA031[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA031[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
