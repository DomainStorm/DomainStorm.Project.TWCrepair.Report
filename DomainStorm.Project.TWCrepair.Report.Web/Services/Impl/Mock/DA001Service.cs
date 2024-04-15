using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA001.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;


namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
    public class DA001Service : IGetService<DA001, string>
    {
       

        public Task<DA001> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA001> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA001 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<DA001> Query(QueryDA001 condition)
        {
            var result = new DA001();


            //取得前7個工作天的報表資料
            var date = DateTime.Today;
            while (result.dates.Count < 7)
            {
                date = date.AddDays(-1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    result.dates.Add(date);
                }
            }
            result.dates = result.dates.Reverse<DateTime>().ToList();

            
            

            for (var i = -1; i < result.dates.Count; i++)
            {
                if (i == -1)
                {
                    result.PlotlyJson.Data.First().Header.Values.Add(new List<string> { "單位\\日期" });
                }
                else
                {
                    result.PlotlyJson.Data.First().Header.Values.Add(new List<string> { result.dates[i].ToString("yyyy/MM/dd") });
                }

                List<string> cellValues;
                if (i == -1)
                {
                    cellValues = new List<string>
                    {
                        "板新系統",
                        "板橋區",
                        "新莊區"
                    };
                }
                else
                {
                    cellValues = new List<string>
                    {
                        "0",
                        "0",
                        "0"
                    };
                }
                result.PlotlyJson.Data.First().Cells.Values.Add(cellValues);
            }
            return result;
        }



        public Task<DA001[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA001[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA001[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
