using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA037.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-附表七、各系統大區NRW
/// </summary>
public class RA037Service : IGetService<RA037, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<Repository.Models.Word>> _getWordRepository;
    private readonly GetRepository<IRepository<Repository.Models.Import.ImportPipe>> _getImportPipeRepository;
    private readonly GetRepository<IRepository<YearPlanSetAllZone>> _getZoneRepository;
    private readonly GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> _getWorkSpaceRepository;
    private readonly GetRepository<IRepository<Repository.Models.DepartmentWorkSpaceItem>> _getWorkSpaceItemRepository;
    private readonly GetRepository<IRepository<YearPlanBase>> _getPlanBaseRepository;

    public RA037Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<Repository.Models.Word>> getWordRepository,
        GetRepository<IRepository<Repository.Models.Import.ImportPipe>> getImportPipeRepository,
        GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> getZoneRepository,
        GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> getWorkSpaceRepository,
        GetRepository<IRepository<Repository.Models.DepartmentWorkSpaceItem>> getWorkSpaceItemRepository,
        GetRepository<IRepository<YearPlanBase>> getPlanBaseRepository
        )
    {
        _getRepository = getRepository;
        _getWordRepository = getWordRepository;
        _getImportPipeRepository = getImportPipeRepository;
        _getZoneRepository = getZoneRepository;
        _getWorkSpaceRepository = getWorkSpaceRepository;
        _getWorkSpaceItemRepository = getWorkSpaceItemRepository;
        _getPlanBaseRepository = getPlanBaseRepository;

    }

    public Task<RA037> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA037> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA037 e => QueryRA037(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA037> QueryRA037(QueryRA037 condition)
    {
        var result = new RA037();
        var planReport = await condition.GetModel(_getRepository(), _getPlanBaseRepository());
        var zoneRepository = _getZoneRepository();
        var workSpaceRepository = _getWorkSpaceRepository();
        var workSpaceItemRepository = _getWorkSpaceItemRepository();
        if (planReport != null)
        {
            result.DepartmentName = planReport.DepartmentName;
            result.Year = planReport.Year - 1911;
            
            for (int i = 0; i < result.LastYears.Length; i++)
            {
                result.LastYears[i] = result.Year - result.LastYears.Length + i + 1;
            }

            if (planReport.YearPlanBase != null)
            {
                //取得年度設定抄見率的所有廠所-系統
                var zones = await zoneRepository.GetListAsync(x => x.Year == planReport.YearPlanBase.Year
                 && x.DepartmentId == planReport.YearPlanBase.DepartmentId);




                foreach (var zone in zones)
                {
                    var ra037ws = new RA037Item
                    {
                        DepartmentId = planReport.DepartmentId,
                        SiteId = zone.SiteId,
                        SiteName = zone.SiteName,
                        WaterSupplySystemId = zone.WaterSupplySystemId,
                        WaterSupplySystemName = zone.WaterSupplySystemName,
                        PlanPipeLength = (int)(zone.DistributionPipe ?? 0)
                    };
                    result.Items.Add(ra037ws);


                    var items = zone.YearPlanSetAllZoneItems.Where(x => x.Month >= 7 && x.Month <= 10).ToList();
                    ra037ws.ReadAmount = (int)items.Where(x => x.DataType == YearPlanSetAllZoneDataType.Read).Sum(x => x.Amount);
                    ra037ws.DistributionAmount = (int)items.Where(x => x.DataType == YearPlanSetAllZoneDataType.Distribution).Sum(x => x.Amount);

                    ra037ws.Customer = (int)items.Where(x => x.DataType == YearPlanSetAllZoneDataType.Customer).Sum(x => x.Amount);
                    ra037ws.CustomerFromSubSection = (int)items.Where(x => x.DataType == YearPlanSetAllZoneDataType.CustomerFromSubSection).Sum(x => x.Amount);

                    //檢查該工作區是否有設定
                    for (var i = 0; i < result.LastYears.Length; i++)
                    {
                        var year = result.LastYears[i] + 1911;
                        var wsItems = await workSpaceItemRepository.GetListAsync(x =>
                        x.DepartmentWorkSpace.Year == year
                        && x.DepartmentWorkSpace.DepartmentId == planReport.YearPlanBase.DepartmentId
                        && x.SiteId == ra037ws.SiteId
                        && x.WaterSupplySystemId == ra037ws.WaterSupplySystemId);

                        if (wsItems.Any())
                        {
                            if (wsItems.Any(x => (x.Disabled && x.DisableTime!.Value.Year == year)
                                || (x.DepartmentWorkSpace.Disabled && x.DepartmentWorkSpace.DisableTime!.Value.Year == year)))
                            {
                                ra037ws.LastYearsHasData[i] = "◎";   //年度中間停用
                            }
                            else
                            {
                                ra037ws.LastYearsHasData[i] = "○";
                            }
                        }
                    }
                }
            }

            var sumItem = new RA037Item
            {
                WaterSupplySystemName = "合計",
                PlanPipeLength = result.Items.Sum(x => x.PlanPipeLength),
                ReadAmount = result.Items.Sum(x => x.ReadAmount),
                DistributionAmount = result.Items.Sum(x => x.DistributionAmount),
                Customer = result.Items.Sum(x => x.Customer),
                CustomerFromSubSection = result.Items.Sum(x => x.CustomerFromSubSection),
            };

            result.Items.Add(sumItem);
        }
        return result;
    }

    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA037[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA037[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA037[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
