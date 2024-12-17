using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA004.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Framework.Caching;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA001.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
    public class DA004Service : IGetService<DA004, string>
    {
        private readonly GetRepository<IRepository<Models.WaterPressureCheckData>> _getPressureDataRepository;
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;
        private readonly IGetService<RA001, string> _ra001Service;

        public DA004Service(
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

        public Task<DA004> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA004> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA004 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class DA004Item
        {
            public DateTime Time { get; set; }
            public double Pressure { get; set; }
        }

        public async Task<DA004> Query(QueryDA004 condition)
        {
            var result = new DA004();

            var ra001 = await _ra001Service.GetAsync<QueryRA001>(condition);

            var before = result.PlotlyJson.Data.Last();
            before.X.Add(ra001!.HighestAnalyze!.BeforePressure!.HighestPressure.ToString()!);
            before.X.Add(ra001!.AverageAnalyze!.BeforePressure!.HighestPressure.ToString()!);
            before.X.Add(ra001!.AverageAnalyze!.BeforePressure!.AveragePressure.ToString()!);
            before.X.Add(ra001!.AverageAnalyze!.BeforePressure!.LowestPressure.ToString()!);
            before.X.Add(ra001!.LowestAnalyze!.BeforePressure!.LowestPressure.ToString()!);
            before.Text = before.X;

            var after = result.PlotlyJson.Data.First();
            after.X.Add(ra001!.HighestAnalyze!.AfterPressure!.HighestPressure.ToString()!);
            after.X.Add(ra001!.AverageAnalyze!.AfterPressure!.HighestPressure.ToString()!);
            after.X.Add(ra001!.AverageAnalyze!.AfterPressure!.AveragePressure.ToString()!);
            after.X.Add(ra001!.AverageAnalyze!.AfterPressure!.LowestPressure.ToString()!);
            after.X.Add(ra001!.LowestAnalyze!.AfterPressure!.LowestPressure.ToString()!);
            after.Text = after.X;

            return result;
        }

        

        public Task<DA004[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA004[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA004[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
