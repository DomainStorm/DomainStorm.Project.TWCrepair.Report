using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA018.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 合約-資源統計表
/// </summary>
public class RA018Service : IGetService<RA018, string>
{
    public Task<RA018> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA018> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA018 e => QueryRA018(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA018> QueryRA018(QueryRA018 condition) 
    {
        
        var result = new RA018
        {
            PrintDate = DateTime.Today,
            EngineeringName = "台中給水廠東工區管線設備修理工程",
            DepartmentName = "台中給水廠",
            BudgetDocContractResourceStatisticsItems = new List<TWCrepair.Shared.ViewModel.BudgetDocContractResourceStatisticsItem>
            {
                new ()
                {
                    Category = new TWCrepair.Shared.ViewModel.Word
                    {
                        Name = "人工類"
                    },
                    Code = "P00001",
                    Name = "技術工",
                    Unit = "工",
                    DayAmount = 3254.1289M,
                    NightAmount = 132.6059M,
                    DayUnitPrice = 2600,
                    NightUnitPrice = 3900,
                    Magnification = 1.5M,
                    DayPrice = 8460735.09M,
                    NightPrice = 517162.93M,
                }
            }
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA018[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA018[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA018[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
