using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA003.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Framework.Caching;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
    public class DA003Service : IGetService<DA003, string>
    {
        private readonly GetRepository<IRepository<Models.WaterPressureCheckData>> _getPressureDataRepository;
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;

        public DA003Service(
            TokenProvider tokenProvider,
            ICache cache,
            GetRepository<IRepository<Models.WaterPressureCheckData>> getPressureDataRepository)
        {
            _tokenProvider = tokenProvider;
            _cache = cache;
            _getPressureDataRepository = getPressureDataRepository;
        }

        public Task<DA003> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA003> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA003 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class DA003Item
        {
            public DateTime Time { get; set; }
            public double Pressure { get; set; }
        }

        public async Task<DA003> Query(QueryDA003 condition)
        {
            var result = new DA003();
            var data = (await _getPressureDataRepository().GetListAsync<DA003Item>(x => x.WaterPressureCheckId == condition.Id, x => new DA003Item { Time = x.Time, Pressure = x.Pressure1} ))
                .OrderBy(x => x.Time).ToList();
            foreach(var eachDatra in data)
            {
                result.PlotlyJson.Data.First().X.Add(eachDatra.Time.ToString("HH:mm"));
                result.PlotlyJson.Data.First().Y.Add(eachDatra.Pressure.ToString());
            }
            return result;
        }

        

        public Task<DA003[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA003[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA003[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
