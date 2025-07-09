using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA007.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Framework.Caching;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
    /// <summary>
    /// 當日流量曲線圖
    /// </summary>
    public class DA007Service : IGetService<DA007, string>
    {
        private readonly GetRepository<IRepository<Models.WaterFlowCheckData>> _getFlowDataRepository;
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;

        public DA007Service(
            TokenProvider tokenProvider,
            ICache cache,
            GetRepository<IRepository<Models.WaterFlowCheckData>> getFlowDataRepository)
        {
            _tokenProvider = tokenProvider;
            _cache = cache;
            _getFlowDataRepository = getFlowDataRepository;
        }

        public Task<DA007> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA007> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA007 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class DA007Item
        {
            public DateTime Time { get; set; }
            public double? CH1Volumetric { get; set; }
        }

        public async Task<DA007> Query(QueryDA007 condition)
        {
            var result = new DA007();
            var data = (await _getFlowDataRepository().GetListAsync<DA007Item>(x => x.WaterFlowCheck.Location == condition.Location
            && x.WaterFlowCheck.MeasureDate == condition.MeasureDate && x.WaterFlowCheck.BeforeOrAfterWordId == condition.BeforeOrAfterWordId
            , x => new DA007Item { Time = x.Time, CH1Volumetric = x.CH1Volumetric } ))
                .OrderBy(x => x.Time).ToList();
            foreach(var eachDatra in data)
            {
                result.PlotlyJson.Data.First().X.Add(eachDatra.Time.ToString("HH:mm"));
                result.PlotlyJson.Data.First().Y.Add(eachDatra.CH1Volumetric.ToString()!);
            }
            return result;
        }

        

        public Task<DA007[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA007[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA007[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
