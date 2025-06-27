using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA006.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Framework.Caching;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
    public class DA006Service : IGetService<DA006, string>
    {
        private readonly GetRepository<IRepository<Models.WaterPressureCheckData>> _getPressureDataRepository;
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;

        public DA006Service(
            TokenProvider tokenProvider,
            ICache cache,
            GetRepository<IRepository<Models.WaterPressureCheckData>> getPressureDataRepository)
        {
            _tokenProvider = tokenProvider;
            _cache = cache;
            _getPressureDataRepository = getPressureDataRepository;
        }

        public Task<DA006> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA006> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA006 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class DA006Item
        {
            public DateTime Time { get; set; }
            public double Pressure { get; set; }
        }

        public async Task<DA006> Query(QueryDA006 condition)
        {
            var result = new DA006();
            var data = (await _getPressureDataRepository().GetListAsync<DA006Item>(x => x.WaterPressureCheck.Location == condition.Location
            && x.WaterPressureCheck.MeasureDate == condition.MeasureDate && x.WaterPressureCheck.BeforeOrAfterWordId == condition.BeforeOrAfterWordId
            , x => new DA006Item { Time = x.Time, Pressure = x.Pressure1} ))
                .OrderBy(x => x.Time).ToList();
            foreach(var eachDatra in data)
            {
                result.PlotlyJson.Data.First().X.Add(eachDatra.Time.ToString("HH:mm"));
                result.PlotlyJson.Data.First().Y.Add(eachDatra.Pressure.ToString());
            }
            return result;
        }

        

        public Task<DA006[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA006[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA006[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
