using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA015.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 發包-資源統計表
/// </summary>
public class RA015Service : IGetService<RA015, string>
{
    public Task<RA015> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA015> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA015 e => QueryRA015(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA015> QueryRA015(QueryRA015 condition) 
    {
        
        var result = new RA015
        {
            PrintDate = DateTime.Today,
            EngineeringName = "台中給水廠東工區管線設備修理工程",
            DepartmentName = "台中給水廠",
            BudgetDocOutSourceResourceStatisticsItems = new List<TWCrepair.Shared.ViewModel.BudgetDocOutSourceResourceStatisticsItem>
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

    public Task<RA015[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA015[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA015[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
