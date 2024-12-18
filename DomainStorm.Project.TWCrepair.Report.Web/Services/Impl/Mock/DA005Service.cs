using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA005.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
    public class DA005Service : IGetService<DA005, string>
    {
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

        private class MockData
        {
            public string LocationNumber { get; set; }
            public double before { get; set; }
            public double after { get; set; }
        }


        public Task<DA005> Query(QueryDA005 condition)
        {
            var result = new DA005();
            
            var before = result.PlotlyJson.Data.Last();
            var after = result.PlotlyJson.Data.First();
            var items = new MockData[]
            {
                new ()
                {
                    LocationNumber = "進1",
                    before = 20,
                    after = 25
                },
                new ()
                {
                    LocationNumber = "G5",
                    before = 18,
                    after = 23
                },
                new ()
                {
                    LocationNumber = "G4",
                    before = 17,
                    after = 19
                },
                new ()
                {
                    LocationNumber = "G3",
                    before = 15,
                    after = 17
                },
                new()
                {
                    LocationNumber = "G2",
                    before = 16,
                    after = 18
                },
                new()
                {
                    LocationNumber = "G1",
                    before = 14,
                    after = 19
                },
                new()
                {
                    LocationNumber = "G6",
                    before = 12,
                    after = 19
                }
            };

            foreach(var item in items)
            {
                before.X.Add(item.LocationNumber);
                before.Y.Add(item.before.ToString());
                after.X.Add(item.LocationNumber);
                after.Y.Add(item.after.ToString());
            }
            return Task.FromResult(result);
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
