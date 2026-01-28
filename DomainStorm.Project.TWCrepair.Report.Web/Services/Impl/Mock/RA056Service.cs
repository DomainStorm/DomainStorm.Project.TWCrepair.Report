using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA056.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-七.系統暨成本費用工作報表
/// </summary>
public class RA056Service : IGetService<RA056, string>
{
   

    public Task<RA056> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA056> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA056 e => QueryRA056(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA056> QueryRA056(QueryRA056 condition)
    {
		RA056 result = new RA056();
		return result;
    }


	public class UserMonthSalary
	{
		public Guid ReportTeamMemberUserId { get; set; }
		public Guid WaterSupplySystemId { get; set; }
		public int Month { get; set; }
		public decimal WorkDayTotal { get; set; }
		public int Salary { get; set; } = 0;

	}
    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA056[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA056[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA056[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
