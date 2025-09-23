using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA046.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 工作日報表-請假天數檢核
/// </summary>
public class RA046Service : IGetService<RA046, string>
{
    private readonly GetRepository<IRepository<Repository.Models.CheckDailyReport>> _getRepository;
    private readonly GetRepository<IRepository<Repository.Models.Import.ImportHoliday>> _getHolidayRepository;

    private IMapper _mapper;

    public RA046Service(
       GetRepository<IRepository<Repository.Models.CheckDailyReport>> getRepository,
       GetRepository<IRepository<Repository.Models.Import.ImportHoliday>> getHolidayRepository,
       IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getHolidayRepository = getHolidayRepository;
        _mapper = mapper;
    }

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

        var holidays = await _getHolidayRepository().GetListAsync(x => 
        x.if_global == 1
        && x.if_holiday == 1
        && x.set_calendar_type ==3
        && x.date_mark.CompareTo(startDateStr) >= 0 && x.date_mark.CompareTo(endDateStr) < 0);

        var reports = await _getRepository().GetListAsync<SimpleCheckDailyReport>(x =>
        x.ReportDepartmentId == condition.DepartmentId && x.ReportDate >= startDate && x.ReportDate < endDate ,
        x => new SimpleCheckDailyReport {
            PostId = x.ReportTeamMemberPostId,
            UserName = x.ReportTeamMemberUserName,
            ReportDate = x.ReportDate,
            DayOfLeave = x.DayOfLeave,
            //DayOfWork = x.DayOfWork
        });

        //隊員先取得distinct
        result.TeamMemers = reports.Select(x => new RA046_TeamMemer
        {
            PostId = x.PostId,
            Name = x.UserName
        }).DistinctBy(x => x.PostId).ToList();



        for(var tempDate = startDate; tempDate < endDate; tempDate = tempDate.AddDays(1))
        {
            var item = new RA046_Item
            {
                Date = tempDate,
                isHoliday = tempDate.DayOfWeek == DayOfWeek.Saturday ||
                            tempDate.DayOfWeek == DayOfWeek.Sunday ||
                            holidays.Any(x => x.date_mark == tempDate.ToString("yyyy/MM/dd"))
            };
            result.Items.Add(item);

            foreach(var member in result.TeamMemers)
            {
                decimal days = 0M;
                var report = reports.Where(x => x.PostId == member.PostId && x.ReportDate == tempDate);
                if(report.Any())
                {
                    days = (report.First().DayOfLeave ?? 0);
                }
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
        //public decimal? DayOfWork { get; set;  }

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
