using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA024.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 管線漏水密度及修理費用
/// </summary>
public class RA024Service : IGetService<RA024, string>
{
   

    public Task<RA024> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA024> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA024 e => QueryRA024(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA024> QueryRA024(QueryRA024 condition) 
    {
        var result = new RA024
        {
            DepartmentName = "",
            SiteName = "",
            DateRange = "從 2025/1/1 到 2025/3/31"
        };
        var fixForms = new List<RA024FixForm>();
        var importPipes = new List<RA024ImportPipe>();
        result.GenerateData(fixForms, importPipes);

        
        return result;
    }

    public class YearPlanItemData
    {
        public double Amount { get; set; }

        public YearPlanSetAllZoneDataType DataType { get; set; }
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA024[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA024[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA024[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
