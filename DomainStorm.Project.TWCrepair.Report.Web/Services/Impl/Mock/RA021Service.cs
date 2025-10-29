using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA021.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 修漏紀錄簿
/// </summary>
public class RA021Service : IGetService<RA021, string>
{
   

    public Task<RA021> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA021> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA021 e => QueryRA021(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private Task<RA021> QueryRA021(QueryRA021 condition) 
    {
        
        var result = new RA021();
        result.Items = new List<RA021Item>
        {
            new RA021Item()
            {
                FixCaseNo = "C111303306",
                AcceptanceTime = DateTime.Now.AddDays(-30),
                Location = "新北市板橋區漢生東路193巷81號對面",
                Source = "檢漏單位",
                ReporterMobile = "0912345678",
                FixDescription = "地下漏水(備註:)",
                PipeDiameter = "40",
                FixTime = DateTime.Now,
                FixDeadline = DateTime.Now,
                FixUnit = "委外",
                CaseEmergency = "緊急",
                WorkTime = "日間",
                ChargeAmount = 1000,
                DispatchNotes = "無"
            }
        };
        return Task.FromResult(result);
    }

    
   
    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA021[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA021[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA021[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
