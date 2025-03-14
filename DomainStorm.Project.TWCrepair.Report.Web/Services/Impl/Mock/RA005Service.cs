using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA005.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

public class RA005Service : IGetService<RA005, string>
{
    

    public Task<RA005> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA005> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA005 e => QueryRA005(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA005> QueryRA005(QueryRA005 condition) 
    {
        var result = new RA005()
        {
            FixCaseNo = "1130001",
            Location = "新北市板橋區三民路1號",
            Contractor="中華工程",
            StartTime = "113年1月1日09時30分"
        };
        
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA005[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA005[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA005[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
