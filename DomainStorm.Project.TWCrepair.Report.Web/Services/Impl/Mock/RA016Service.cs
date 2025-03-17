using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA016.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 合約-詳細表
/// </summary>
public class RA016Service : IGetService<RA016, string>
{
    

    public Task<RA016> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA016> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA016 e => QueryRA016(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private Task<RA016> QueryRA016(QueryRA016 condition) 
    {

        var result = new RA016
        {
            PrintDate = DateTime.Now,
            BudgetDocContractDetail = new TWCrepair.Shared.ViewModel.BudgetDocContractDetail
            {
                EngineeringName = "台中給水廠東工區管線設備修理工程",
                DepartmentName = "台中給水廠",
                 BudgetDocContractDetailItems = new List<TWCrepair.Shared.ViewModel.BudgetDocContractDetailItem>
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

    public Task<RA016[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA016[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA016[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
