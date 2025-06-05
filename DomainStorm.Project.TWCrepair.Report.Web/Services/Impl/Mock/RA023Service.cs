using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA023.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 管線修理統計表
/// </summary>
public class RA023Service : IGetService<RA023, string>
{
    

    public Task<RA023> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA023> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA023 e => QueryRA023(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private  Task<RA023> QueryRA023(QueryRA023 condition) 
    {

        var result = new RA023
        {
            DepartmentName = "十二區處",
            SiteName = "板橋服務所",
            DateRange = "從2025/1/1至2025/3/31"

        };
        var fixForms = new List<RA023FixForm> { };
        result.GenerateData(fixForms);
        return Task.FromResult(result);
    }


    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA023[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA023[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA023[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
