using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA008.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Framework.Caching;
using LinqKit;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
    /// <summary>
    /// 流量分析-(檢前總表/檢後總表)的流量曲線圖(結合在 RA041裡的圖)
    /// </summary>
    public class DA008Service : IGetService<DA008, string>
    {
        private readonly GetRepository<IRepository<Models.WaterFlowCheckData>> _getFlowDataRepository;
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;

        public DA008Service(
            TokenProvider tokenProvider,
            ICache cache,
            GetRepository<IRepository<Models.WaterFlowCheckData>> getFlowDataRepository)
        {
            _tokenProvider = tokenProvider;
            _cache = cache;
            _getFlowDataRepository = getFlowDataRepository;
        }

        public Task<DA008> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA008> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA008 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class DA008Item
        {
            public DateTime Time { get; set; }
            public double? CH1Volumetric { get; set; }
        }

        public async Task<DA008> Query(QueryDA008 condition)
        {
            var result = new DA008();

            //1.取得所有地點的所有時間的流量資料
            var pb = PredicateBuilder.New<Repository.Models.WaterFlowCheckData>();
            //必填
            var exp = pb.Start(x =>
                x.WaterFlowCheck.DepartmentId == condition.DepartmentId
                && x.WaterFlowCheck.MeasureDate == condition.MeasureDate
                && x.WaterFlowCheck.BeforeOrAfterWordId == condition.BeforeOrAfterWordId
                && x.CH1Volumetric.HasValue);

            if (condition.SiteId.HasValue)
            {
                exp = exp.And(x => x.WaterFlowCheck.SiteId == condition.SiteId);
            }
            if (condition.WaterSupplySystemId.HasValue)
            {
                exp = exp.And(x => x.WaterFlowCheck.WaterSupplySystemId == condition.WaterSupplySystemId);
            }
            if (condition.WorkSpaceId.HasValue)
            {
                exp = exp.And(x => x.WaterFlowCheck.WorkSpaceId == condition.WorkSpaceId);
            }
            if (condition.SmallRegionId.HasValue)
            {
                exp = exp.And(x => x.WaterFlowCheck.SmallRegionId == condition.SmallRegionId);
            }
            var data = (await _getFlowDataRepository().GetListAsync<SimpleWaterFlowCheckData>(exp,
                x => new SimpleWaterFlowCheckData
                {
                    Time = x.Time,
                    CH1Volumetric = x.CH1Volumetric
                })).ToList();


            //2.以時間為 key 作加總
            var group = data.GroupBy(x => x.Time)
                .Select(g => new 
                { 
                    time = g.Key, 
                    CH1Volumetric = Math.Round(g.Sum(x => x.CH1Volumetric!.Value),3) 
                })
                .OrderBy(x => x.time);
                
            foreach (var eachDatra in data)
            {
                result.PlotlyJson.Data.First().X.Add(eachDatra.Time.ToString("HH:mm"));
                result.PlotlyJson.Data.First().Y.Add(eachDatra.CH1Volumetric.ToString()!);
            }
            return result;
        }

        public class SimpleWaterFlowCheckData
        {
            public DateTime Time { get; set; }
            public Double? CH1Volumetric { get; set; }
        }


        public Task<DA008[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA008[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA008[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
