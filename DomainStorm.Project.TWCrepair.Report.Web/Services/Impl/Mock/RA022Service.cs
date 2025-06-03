using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA022.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 修漏紀錄簿二
/// </summary>
public class RA022Service : IGetService<RA022, string>
{
    public Task<RA022> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA022> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA022 e => QueryRA022(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private Task<RA022> QueryRA022(QueryRA022 condition) 
    {
        
        var result = new RA022();
        result.Items = new List<RA022Item>
        {
            new RA022Item()
            {
                FixCaseNo = "C111303306",
                CaseAttribute = "漏水案件",
                EquipmentAttribute = "管線",
                PipeKind = "PVCP",
                PipeDiameter = "40", 
                FinalCost_Outsourcing = 0.0M,
                FinalCost_Material = 367,
                FinalCost_RoadRightProxy = 0,
                FinalCost_EmployeeSalary = 1028.0M,
                FinalCost_Total = 1395,
                Reason = "老化腐蝕",
                FixSituation = "裂縫",
                Situation = "滲入地下",
                DailyAmount = 42.75M,
                TotalAmount = 1.78M,
                LeakageEquipmentAttribute = "用戶外線及設備",
                Photos = 0,
                SuperVisorHour = new List<string>
                {
                    "老吳 4.0時"
                }
            }
        };
        return Task.FromResult(result);
    }

    
   
    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA022[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA022[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA022[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
