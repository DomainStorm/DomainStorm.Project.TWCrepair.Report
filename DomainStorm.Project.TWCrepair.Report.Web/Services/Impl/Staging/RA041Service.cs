using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA041.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 流量分析-檢前總表/檢後總表
/// </summary>
public class RA041Service : IGetService<RA041, string>, IGetService<RA041MeasureDate, Guid>
{
    private readonly GetRepository<IRepository<Repository.Models.WaterFlowCheck>> _getRepository;
    private readonly GetRepository<IRepository<Repository.Models.Word>> _getWordRepository;

    private IMapper _mapper;

    public RA041Service(
        GetRepository<IRepository<Repository.Models.WaterFlowCheck>> getRepository,
        GetRepository<IRepository<Repository.Models.Word>> getWordRepository,
       IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getWordRepository = getWordRepository;
        _mapper = mapper;
    }

    public Task<RA041> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA041> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA041 e => QueryRA041(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA041> QueryRA041(QueryRA041 condition)
    {
        var beforOrAfterword = await _getWordRepository().GetAsync(condition.BeforeOrAfterWordId);


        var pb = PredicateBuilder.New<Repository.Models.WaterFlowCheck>();
        //必填
        var exp = pb.Start(x =>
            x.DepartmentId == condition.DepartmentId
            && x.MeasureDate == condition.MeasureDate
            && x.BeforeOrAfterWordId == condition.BeforeOrAfterWordId);

        if (condition.SiteId.HasValue)
        {
            exp = exp.And(x => x.SiteId == condition.SiteId);
        }
        if (condition.WaterSupplySystemId.HasValue)
        {
            exp = exp.And(x => x.WaterSupplySystemId == condition.WaterSupplySystemId);
        }
        if (condition.WorkSpaceId.HasValue)
        {
            exp = exp.And(x => x.WorkSpaceId == condition.WorkSpaceId);
        }
        if (condition.SmallRegionId.HasValue)
        {
            exp = exp.And(x => x.SmallRegionId == condition.SmallRegionId);
        }


        var result = new RA041
        {
            MeasuerDate = condition.MeasureDate, 
            BeforeOrAfter = beforOrAfterword.Name,
            SheetName = beforOrAfterword.Name.Contains("前") ?
            "檢前總表":
            "檢後總表"

        };
        

        var flowChecks = await _getRepository().GetListAsync(exp);
        foreach(var flowCheck in flowChecks)
        {
            var firstData = flowCheck.WaterFlowCheckData.OrderBy(x => x.Time).FirstOrDefault();
            var lastData = flowCheck.WaterFlowCheckData.OrderBy(x => x.Time).LastOrDefault();
            var minData = flowCheck.WaterFlowCheckData.OrderBy(x => x.CH1Volumetric).FirstOrDefault();

            var item = new RA041_Item
            {
                Location = flowCheck.Location,
            };

            if(firstData != null && lastData!= null)
            {
                if(firstData.CH1REVTotal > 0)    //負機流??
                {
                    item.StartValue =  Math.Round( firstData.CH1REVTotal.Value,2);
                    item.EndValue = Math.Round(lastData.CH1REVTotal!.Value, 2);
                }
                else
                {
                    item.StartValue = Math.Round(firstData.CH1FWDTotal!.Value , 2);
                    item.EndValue = Math.Round(lastData.CH1FWDTotal!.Value, 2);
                    item.Positive = true;
                }
            }

            if(minData != null)
            {
                item.MinTime = minData.Time;
                item.MinValue = Math.Round(minData.CH1Volumetric!.Value, 2);
            }

            result.Items.Add(item);
        }
        
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA041[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA041[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA041[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate> IGetService<RA041MeasureDate, Guid>.GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate> IGetService<RA041MeasureDate, Guid>.GetAsync<TQuery>(IQuery condition)
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate[]> IGetService<RA041MeasureDate, Guid>.GetListAsync()
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate[]> IGetService<RA041MeasureDate, Guid>.GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<RA041MeasureDate[]> IGetService<RA041MeasureDate, Guid>.GetListAsync<TQuery>(IQuery condition)
    {
        return condition switch
        {
            QueryRA041Date e => QueryRA041Date(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    async Task<RA041MeasureDate[]> QueryRA041Date(QueryRA041Date condition)
    {
        var pb = PredicateBuilder.New<Repository.Models.WaterFlowCheck>();
        //必填
        var exp = pb.Start(x =>
            x.DepartmentId == condition.DepartmentId
            && x.BeforeOrAfterWordId == condition.BeforeOrAfterWordId);

        if(condition.Year.HasValue)
        {
            exp = exp.And(x => x.MeasureDate.Year == condition.Year);
        }
        if (condition.SiteId.HasValue)
        {
            exp = exp.And(x => x.SiteId == condition.SiteId);
        }
        if (condition.WaterSupplySystemId.HasValue)
        {
            exp = exp.And(x => x.WaterSupplySystemId == condition.WaterSupplySystemId);
        }
        if (condition.WorkSpaceId.HasValue)
        {
            exp = exp.And(x => x.WorkSpaceId == condition.WorkSpaceId);
        }
        if (condition.SmallRegionId.HasValue)
        {
            exp = exp.And(x => x.SmallRegionId == condition.SmallRegionId);
        }


        var dates = (await _getRepository().GetListAsync<RA041MeasureDate>(
            exp,
            x => new RA041MeasureDate 
            { 
                MeasureDate = x.MeasureDate
            }))
          .Distinct().OrderBy(x => x).ToArray();
        return dates;
    }
}
