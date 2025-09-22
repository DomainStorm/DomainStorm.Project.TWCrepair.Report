using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA046.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;
    

/// <summary>
/// 工作日報表-請假天數檢核
/// </summary>
public class RA046Service : IGetService<RA046, string>
{
    

    public Task<RA046> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA046> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA046 e => QueryRA046(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA046> QueryRA046(QueryRA046 condition)
    {
        var result = new RA046();
        var startDate = new DateTime(condition.Year, condition.Month, 1);
        var endDate = startDate.AddMonths(1);
        var startDateStr = startDate.ToString("yyyy/MM/dd");
        var endDateStr = endDate.ToString("yyyy/MM/dd");

        result.TeamMemers = new List<RA046_TeamMemer>
        {
            new RA046_TeamMemer
            {
                Name ="張三"
            },
            new RA046_TeamMemer
            {
                Name ="李四1"
            }
        };


        for(var tempDate = startDate; tempDate < endDate; tempDate = tempDate.AddDays(1))
        {
            var item = new RA046_Item
            {
                Date = tempDate,
                isHoliday = tempDate.DayOfWeek == DayOfWeek.Saturday ||
                            tempDate.DayOfWeek == DayOfWeek.Sunday 
                            
            };

            foreach(var member in result.TeamMemers)
            {
                decimal days = 0M;
                item.TotalDays.Add(days);
            }
        }

        return result;
    }

    public class SimpleCheckDailyReport
    {
        public Guid PostId { get; set; }

        public string UserName { get; set; }

        public decimal? DayOfLeave { get; set; }
        public decimal? DayOfWork { get; set;  }

        public DateTime ReportDate { get; set; }
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA046[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA046[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA046[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
