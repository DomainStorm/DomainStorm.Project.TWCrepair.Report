using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA019.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 漏水情形管制月報表
/// </summary>
public class RA019Service : IGetService<RA019, string>
{
    public Task<RA019> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA019> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA019 e => QueryRA019(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA019> QueryRA019(QueryRA019 condition) 
    {
        var result = new RA019
        {
            DepartmentName = "台水十二區處",
            DateRange = "從2025-04-01至2025-04-30",
            Items = new List<RA019Item>
            {
                new ()
                {
                    SiteName = "土城服務所",
                    UnDispatch = 7,
                    UrgentCase = new RA019_CaseNumber(),
                    UnUrgentCase = new RA019_CaseNumber
                    {
                        Dispatch = 94,
                        FinishNotOverDue = 94
                    }
                },
                new ()
                {
                    SiteName = "板橋服務所",
                    UnDispatch = 20,
                    UrgentCase = new RA019_CaseNumber(),
                    UnUrgentCase = new RA019_CaseNumber
                    {
                        Dispatch = 272,
                        FinishNotOverDue = 10,
                        FinishOverDue1 = 20,
                        FinishOverDue3 = 30,
                        FinishOverDueOver3 = 40
                    }
                }
            }
            
        };
        result.Sum();
        return result;
    }

    
    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA019[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA019[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA019[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
