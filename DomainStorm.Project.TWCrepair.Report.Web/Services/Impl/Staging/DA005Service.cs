using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA005.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Framework.Caching;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA001.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
    public class DA005Service : IGetService<DA005, string>
    {
        private readonly GetRepository<IRepository<Models.WaterPressureCheckData>> _getPressureDataRepository;
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;
        private readonly IGetService<RA001, string> _ra001Service;

        public DA005Service(
            TokenProvider tokenProvider,
            ICache cache,
            GetRepository<IRepository<Models.WaterPressureCheckData>> getPressureDataRepository,
            IGetService<RA001, string> ra001Service)
        {
            _tokenProvider = tokenProvider;
            _cache = cache;
            _getPressureDataRepository = getPressureDataRepository;
            _ra001Service = ra001Service;
        }

        public Task<DA005> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA005> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA005 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class DA005Item
        {
            public DateTime Time { get; set; }
            public double Pressure { get; set; }
        }

        public async Task<DA005> Query(QueryDA005 condition)
        {
            var result = new DA005();

            var ra001 = await _ra001Service.GetAsync<QueryRA001>(condition);

            var before = result.PlotlyJson.Data.Last();
            var after = result.PlotlyJson.Data.First();
            foreach (var item in ra001.Items)
            {
                before.X.Add(item.LocationNumber);
                before.Y.Add(item.BeforePressure.TotalWater.ToString()!);
                after.X.Add(item.LocationNumber);
                after.Y.Add(item.AfterPressure.TotalWater.ToString()!);
            }
            return result;
        }

        

        public Task<DA005[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA005[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA005[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
