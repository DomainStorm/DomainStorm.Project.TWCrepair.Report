using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA001.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

public class RA001Service : IGetService<RA001, string>, IGetService<DateTime, Guid>
{
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
    private Task<RA001> QueryRA001(QueryRA001 condition)
    {
        var result = new RA001();
        result.PrintDate = DateTime.Today;
        result.BeforeDate = DateTime.Parse("2024/12/1");
        result.AfterDate = DateTime.Parse("2024/12/1");
        result.WaterSupplySystemName = "供水系統一";
        result.Items.Add(new RA001_Item
        {
            Location = "1.地點1",
            BeforePressure = new RA001_Pressure
            {
                HighestPressure = 2.2,
                AveragePressure = 1.2,
                LowestPressure = 0.2
            },
            AfterPressure = new RA001_Pressure
            {
                HighestPressure = 2.1,
                AveragePressure = 1.1,
                LowestPressure = 0.1
            }
        });
        result.Items.Add(new RA001_Item
        {
            Location = "1.地點2",
            BeforePressure = new RA001_Pressure
            {
                HighestPressure = 2.3,
                AveragePressure = 1.3,
                LowestPressure = 0.3
            },
            AfterPressure = new RA001_Pressure
            {
                HighestPressure = 2.4,
                AveragePressure = 1.4,
                LowestPressure = 0.4
            }
        });

        //分析結果
        result.HighestAnalyze.BeforePressure = RA001_Pressure.GetHighestPressure(result.Items.Where(x => x.BeforePressure != null).Select(x => x.BeforePressure).ToArray());
        result.HighestAnalyze.AfterPressure = RA001_Pressure.GetHighestPressure(result.Items.Where(x => x.AfterPressure != null).Select(x => x.AfterPressure).ToArray());
        result.LowestAnalyze.BeforePressure = RA001_Pressure.GetLowestPressure(result.Items.Where(x => x.BeforePressure != null).Select(x => x.BeforePressure).ToArray());
        result.LowestAnalyze.AfterPressure = RA001_Pressure.GetLowestPressure(result.Items.Where(x => x.AfterPressure != null).Select(x => x.AfterPressure).ToArray());
        result.AverageAnalyze.BeforePressure = RA001_Pressure.GetAveragePressure(result.Items.Where(x => x.BeforePressure != null).Select(x => x.BeforePressure).ToArray());
        result.AverageAnalyze.AfterPressure = RA001_Pressure.GetAveragePressure(result.Items.Where(x => x.AfterPressure != null).Select(x => x.AfterPressure).ToArray());

        return Task.FromResult(result);
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

    Task<DateTime[]> QueryRA001Date(QueryRA001Date condition)
    {
        return Task.FromResult(new DateTime[]
        {
            DateTime.Today.AddDays(-2),
            DateTime.Today.AddDays(-1),
            DateTime.Today,
        });
    }
}
