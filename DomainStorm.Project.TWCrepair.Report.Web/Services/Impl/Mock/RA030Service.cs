using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA030.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-計劃經費
/// </summary>
public class RA030Service : IGetService<RA030, string>
{
    

    public Task<RA030> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA030> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA030 e => QueryRA030(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA030> QueryRA030(QueryRA030 condition)
    {
        var result = new RA030
        {
            Conclusion = "配合資訊電腦化,積極建立以系統及分區日配水量臨界值之設定為導向的機動檢漏作業，俾能即時檢出並修復，使無效的漏水量減至最少。同時在正確的資料提供下（尤以配水量為最）來研析，藉此資訊來探討並提出必要的改善措施，期能切中時弊，完成各系統之營運管理評核，藉由作業架構之逐步改進，提升檢漏作業在整體營運管理上之層次。",
            Funding = "1,000",
            Benefit = "2,000",
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA030[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA030[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA030[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
