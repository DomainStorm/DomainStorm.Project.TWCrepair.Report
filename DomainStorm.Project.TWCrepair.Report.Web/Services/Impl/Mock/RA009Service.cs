using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA009.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 預算書-詳細表
/// </summary>
public class RA009Service : IGetService<RA009, string>
{
    

    public Task<RA009> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA009> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA009 e => QueryRA009(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private Task<RA009> QueryRA009(QueryRA009 condition) 
    {

        var result = new RA009
        {
            PrintDate = DateTime.Now,
            BudgetDocDetail = new TWCrepair.Shared.ViewModel.BudgetDocDetail
            {
                EngineeringName = "台中給水廠東工區管線設備修理工程",
                DepartmentName = "台中給水廠",
                BudgetDocDetailItems = new List<TWCrepair.Shared.ViewModel.BudgetDocDetailItem>
                {
                    new ()
                    {
                        Code = "025",
                        Name = "表箱另件修理或換新",
                        Description ="13-25",
                        Unit = "處",
                        DayAmount = 1,
                        NightAmount = 1,
                        DayPrice = 602,
                        NightPrice = 722,
                        TotalPrice = 1324
                    }

                },
                Detail_SubTotal = 25486135,
                Detail_SafetyAndHealthAmount = 4.72M,
                Detail_SafetyAndHealthUnitPrice = 131286,
                Detail_SafetyAndHealthPrice = 619670,
                Detail_MaterialCustodyPrice = 30000,
                Detail_InsuranceSubsidyForFixPercentage = 1.7M,
                Detail_InsuranceSubsidyForFixPrice = 433264,
                Detail_TurbidWaterPrice = 254861,
                Detail_InsuranceSubsidyPrice = 2132795,
                Detail_QualityControlPrice = 509722,
                Detail_ProfitPrice = 2293752,
                Detail_Total= 31760199,
                Detail_Tax = 1588010,
                Detail_FinalTotal = 33348209

            }

        };
        return Task.FromResult( result);
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA009[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA009[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA009[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
