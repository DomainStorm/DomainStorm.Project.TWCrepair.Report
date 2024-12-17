using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA001.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

public class RA001Service : IGetService<RA001, string>, IGetService<DateTime, Guid>
{
    private readonly GetRepository<IRepository<WaterPressureCheck>> _getRepository;

    public RA001Service(GetRepository<IRepository<WaterPressureCheck>> getRepository)
    {
        _getRepository = getRepository;
    }

    public Task<RA001> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA001> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA001 e => QueryRA001(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA001> QueryRA001(QueryRA001 condition) 
    {
        var result = new RA001();
        result.PrintDate = DateTime.Today;
        result.BeforeDate = condition.BeforeDate;
        result.AfterDate = condition.AfterDate;

        
        var pb = PredicateBuilder.New<WaterPressureCheck>();
        //必填
        var exp = pb.Start(x =>
            x.DepartmentId == condition.DepartmentId
            && x.WaterSupplySystemId == condition.WaterSupplySystemId
            && x.WorkSpaceId == condition.WorkSpaceId
            && (x.MeasureDate == condition.BeforeDate || x.MeasureDate == condition.AfterDate));

        if (condition.SmallRegionId.HasValue)
        {
            exp = pb.And(x => x.SmallRegionId == condition.SmallRegionId);
        }
        if (condition.SiteId.HasValue)
        {
            exp = pb.And(x => x.SiteId == condition.SiteId);
        }

        var models = await _getRepository().GetListAsync(exp);

        if (!models.Any())
        {
            return result;
        }
        result.WaterSupplySystemName = models.First().WaterSupplySystemName;
        //合併各單位的前後資料
        foreach(var model in models)
        {
            if ((model.BeforeOrAfter.Name.Contains("前") && model.MeasureDate == condition.BeforeDate)
               || (model.BeforeOrAfter.Name.Contains("後") && model.MeasureDate == condition.AfterDate))
            {
                var tempLocation = model.LocationNumber + "." + model.Location;
                var locationItem = result.Items.FirstOrDefault(x => x.Location == tempLocation);
                if (locationItem == null)
                {
                    locationItem = new RA001_Item
                    {
                        Location = tempLocation,
                        LocationNumber = model.LocationNumber
                    };
                    result.Items.Add(locationItem);
                }
                if (model.BeforeOrAfter.Name.Contains("前") && model.MeasureDate == condition.BeforeDate)
                {
                    locationItem.BeforePressure = new RA001_Pressure(model);
                }
                else if (model.BeforeOrAfter.Name.Contains("後") && model.MeasureDate == condition.AfterDate)
                {
                    locationItem.AfterPressure = new RA001_Pressure(model);
                }
            }
        }

        //分析結果
        result.HighestAnalyze.BeforePressure = RA001_Pressure.GetHighestPressure(result.Items.Where(x => x.BeforePressure != null).Select(x => x.BeforePressure).ToArray());
        result.HighestAnalyze.AfterPressure = RA001_Pressure.GetHighestPressure(result.Items.Where(x => x.AfterPressure != null).Select(x => x.AfterPressure).ToArray());
        result.LowestAnalyze.BeforePressure = RA001_Pressure.GetLowestPressure(result.Items.Where(x => x.BeforePressure != null).Select(x => x.BeforePressure).ToArray());
        result.LowestAnalyze.AfterPressure = RA001_Pressure.GetLowestPressure(result.Items.Where(x => x.AfterPressure != null).Select(x => x.AfterPressure).ToArray());
        result.AverageAnalyze.BeforePressure = RA001_Pressure.GetAveragePressure(result.Items.Where(x => x.BeforePressure != null).Select(x => x.BeforePressure).ToArray());
        result.AverageAnalyze.AfterPressure = RA001_Pressure.GetAveragePressure(result.Items.Where(x => x.AfterPressure != null).Select(x => x.AfterPressure).ToArray());


        //容錯用,cshtml 比較好處理
        foreach(var item in result.Items)
        {
            if (item.BeforePressure == null)
                item.BeforePressure = new RA001_Pressure();
            if(item.AfterPressure == null)
                item.AfterPressure = new RA001_Pressure();
        }


        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA001[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA001[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA001[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<DateTime> IGetService<DateTime, Guid>.GetAsync<TQuery>(IQuery condition)
    {
        throw new NotImplementedException();
    }

    Task<DateTime[]> IGetService<DateTime, Guid>.GetListAsync()
    {
        throw new NotImplementedException();
    }

    Task<DateTime[]> IGetService<DateTime, Guid>.GetListAsync<TQuery>(IQuery condition)
    {
        return condition switch
        {
            QueryRA001Date e => QueryRA001Date(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    async Task<DateTime[]> QueryRA001Date(QueryRA001Date condition)
    {
        var pb = PredicateBuilder.New<WaterPressureCheck>();

        //必填
        var exp = pb.Start(x =>
            x.MeasureDate.Year == condition.Year 
            && x.DepartmentId == condition.DepartmentId
            && x.WaterSupplySystemId == condition.WaterSupplySystemId
            && x.BeforeOrAfterWordId == condition.BeforeOrAfterWordId
            && x.WorkSpaceId == condition.WorkSpaceId);

        if(condition.SmallRegionId.HasValue)
        {
            exp = pb.And(x => x.SmallRegionId == condition.SmallRegionId);
        }
        if (condition.SiteId.HasValue)
        {
            exp = pb.And(x => x.SiteId == condition.SiteId);
        }

        var dates = (await _getRepository().GetListAsync<DateTime>(
            exp,
            x => x.MeasureDate))
          .Distinct().OrderBy(x => x).ToArray();
        return dates;
    }
}
