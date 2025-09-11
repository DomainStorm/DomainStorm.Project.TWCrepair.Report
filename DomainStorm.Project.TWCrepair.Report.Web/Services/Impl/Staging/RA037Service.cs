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
    private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> _getZoneRepository;
    private readonly GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> _getWorkSpaceRepository;
    private readonly GetRepository<IRepository<Repository.Models.DepartmentWorkSpaceItem>> _getWorkSpaceItemRepository;

    public RA037Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<Repository.Models.Word>> getWordRepository,
        GetRepository<IRepository<Repository.Models.Import.ImportPipe>> getImportPipeRepository,
        GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> getZoneRepository,
        GetRepository<IRepository<Repository.Models.DepartmentWorkSpace>> getWorkSpaceRepository,
        GetRepository<IRepository<Repository.Models.DepartmentWorkSpaceItem>> getWorkSpaceItemRepository
        )
    {
        _getRepository = getRepository;
        _getWordRepository = getWordRepository;
        _getImportPipeRepository = getImportPipeRepository;
        _getZoneRepository = getZoneRepository;
        _getWorkSpaceRepository = getWorkSpaceRepository;
        _getWorkSpaceItemRepository = getWorkSpaceItemRepository;


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
        var planReport = await _getRepository().GetAsync(condition.Id);
        var zoneRepository = _getZoneRepository();
        var workSpaceRepository = _getWorkSpaceRepository();
        var workSpaceItemRepository = _getWorkSpaceItemRepository();
        var result = new RA037
        {
            DepartmentName = planReport.DepartmentName,
            Year = planReport.Year - 1911,
        };
        for(int i = 0; i < result.LastYears.Length ; i++)
        {
            result.LastYears[i] = result.Year - result.LastYears.Length + i + 1;
        }


       


        if (planReport.YearPlanBase != null)
        {
            foreach(var ws in planReport.YearPlanBase.YearPlanWorkSpaces)
            {
                var ra037ws = new RA037WorkSapce
                {
                    DepartmentId = planReport.DepartmentId,
                    SiteId = ws.SiteId,
                    SiteName = ws.SiteName,
                    WaterSupplySystemId = ws.WaterSupplySystemId,
                    WaterSupplySystemName = ws.WaterSupplySystemName,
                    PlanPipeLength = ws.PlanPipeLength
                };
                result.WorkSapces.Add(ra037ws);

                //取得該系統的抄見率相關資料
                var zones = await zoneRepository.GetListAsync(x => x.Year == planReport.YearPlanBase.Year
                 && x.DepartmentId == planReport.YearPlanBase.DepartmentId
                 && x.SiteId == ws.SiteId
                 && x.WaterSupplySystemId == ws.WaterSupplySystemId);

                var items = zones.SelectMany(x => x.YearPlanSetAllZoneItems).Where(x => x.Month >=7 && x.Month <= 10).ToList();
                ra037ws.ReadAmount = (int)items.Where(x => x.DataType == YearPlanSetAllZoneDataType.Read).Sum(x => x.Amount);
                ra037ws.DistributionAmount = (int)items.Where(x => x.DataType == YearPlanSetAllZoneDataType.Distribution).Sum(x => x.Amount);

                ra037ws.Customer = (int)items.Where(x => x.DataType == YearPlanSetAllZoneDataType.Customer).Sum(x => x.Amount);
                ra037ws.CustomerFromSubSection = (int)items.Where(x => x.DataType == YearPlanSetAllZoneDataType.CustomerFromSubSection).Sum(x => x.Amount);

                //檢查該工作區是否有設定
                for(var i = 0; i < result.LastYears.Length; i++)
                {
                    var year = result.LastYears[i] + 1911;
                    var wsItems = await workSpaceItemRepository.GetListAsync(x =>
                    x.DepartmentWorkSpace.Year == year
                    && x.DepartmentWorkSpace.DepartmentId == planReport.YearPlanBase.DepartmentId
                    && x.SiteId == ra037ws.SiteId
                    && x.WaterSupplySystemId == ra037ws.WaterSupplySystemId);

                    if(wsItems.Any())
                    {
                        if(wsItems.Any(x => (x.Disabled && x.DisableTime!.Value.Year == year)
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
        
        var sumItem = new RA037WorkSapce
        {
            WaterSupplySystemName = "合計",
            PlanPipeLength = result.WorkSapces.Sum(x => x.PlanPipeLength),
            ReadAmount = result.WorkSapces.Sum(x => x.ReadAmount),
            DistributionAmount = result.WorkSapces.Sum(x => x.DistributionAmount),
            Customer = result.WorkSapces.Sum(x => x.Customer),
            CustomerFromSubSection = result.WorkSapces.Sum(x => x.CustomerFromSubSection),
        };

        result.WorkSapces.Add(sumItem);
        
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
